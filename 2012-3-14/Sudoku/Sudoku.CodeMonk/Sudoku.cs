using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Sudoku.Common;

namespace Sudoku.CodeMonk
{
    [Serializable]
    public class Sudoku : ISudoku
    {
        public event FinishHandler OnInitializeFinish;
        public event FinishHandler OnLoadFinish;

        #region 序列化
        private static byte[] ToByte(byte[,] b)
        {
            byte[] ret = new byte[81];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret[i * 9 + j] = b[i, j];
                }
            }
            return ret;
        }
        private static byte[,] GetByte(byte[] value)
        {
            byte[,] ret = new byte[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ret[i, j] = value[i * 9 + j];
                }
            }
            return ret;
        }

        [XmlElement("Data")]
        public byte[] Data
        {
            get { return ToByte(data); }
            set { data = GetByte(value); }
        }

        [XmlElement("Answer")]
        public byte[] Answer
        {
            get { return ToByte(answer); }
            set { answer = GetByte(value); }
        }

        [XmlElement("OriginalArray")]
        public byte[] OriginalArray
        {
            get { return ToByte(original); }
            set { original = GetByte(value); }
        }

        #endregion

        private byte[,] data = new byte[9, 9];

        private byte[,] answer = new byte[9, 9];
        private byte[,] original = new byte[9, 9];

        private readonly Random r = new Random();
        private Stack<Cell> undoStack = new Stack<Cell>(1024);
        private Stack<Cell> redoStack = new Stack<Cell>(1024);

        private void InitializeData()
        {
            List<byte> num = new List<byte>(9) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            byte[] cells = new byte[9];

            for (int i = 0; i < 9; i++)
            {
                int index = r.Next(1, num.Count) - 1;
                byte v = num[index];
                num.RemoveAt(index);
                cells[i] = v;
            }




            for (int i = 0; i < 9; i++)
            {
                data[i, 0] = cells[i];
            }

            #region data

            data[0, 1] = cells[3];
            data[1, 1] = cells[4];
            data[2, 1] = cells[5];

            data[0, 2] = cells[6];
            data[1, 2] = cells[7];
            data[2, 2] = cells[8];

            data[3, 1] = cells[6];
            data[4, 1] = cells[7];
            data[5, 1] = cells[8];

            data[3, 2] = cells[0];
            data[4, 2] = cells[1];
            data[5, 2] = cells[2];


            data[6, 1] = cells[0];
            data[7, 1] = cells[1];
            data[8, 1] = cells[2];

            data[6, 2] = cells[3];
            data[7, 2] = cells[4];
            data[8, 2] = cells[5];

            #endregion

            Fill(0, 1);
            Fill(1, 1);
            Fill(2, 1);
            Fill(0, 2);
            Fill(1, 2);
            Fill(2, 2);
        }

        private void Fill(int col, int row)
        {
            col = col * 3;
            row = row * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int index = (col + i + 1 == 9 ? 0 : col + i + 1);
                    data[col + i, row + j] = data[index, row + j - 3];
                }
            }
        }

        #region Sudoku

        public byte this[byte x, byte y]
        {
            get
            {
                if (x < 9 && x > 0 && y > 0 && y < 9)
                    return data[x, y];
                return 0;
            }
            set
            {
                lock (data)
                {
                    if (x < 9 && x >= 0 && y >= 0 && y < 9)
                    {
                        undoStack.Push(new Cell { X = x, Y = y, Value = data[x, y] });
                        data[x, y] = value;
                    }
                }
            }
        }

        public void Load(string path)
        {
            using (var stream = System.IO.File.OpenRead(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sudoku));
                Sudoku obj = serializer.Deserialize(stream) as Sudoku;
                stream.Close();
                Data = obj.Data;
                Answer = obj.Answer;
                OriginalArray = obj.OriginalArray;
            }
            if (OnLoadFinish != null)
                OnLoadFinish.Invoke();
        }

        public void Save(string path)
        {
            using (var stream = System.IO.File.OpenWrite(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Sudoku));
                serializer.Serialize(stream, this);
                stream.Close();
            }
        }



        public void Undo()
        {
            if (undoStack.Count < 1) return;
            Cell c = undoStack.Pop();
            redoStack.Push(new Cell { X = c.X, Y = c.Y, Value = data[c.X, c.Y] });
            data[c.X, c.Y] = c.Value;
        }

        public void Redo()
        {
            if (redoStack.Count < 1) return;
            Cell c = redoStack.Pop();
            undoStack.Push(new Cell { X = c.X, Y = c.Y, Value = data[c.X, c.Y] });
            data[c.X, c.Y] = c.Value;
        }

        public void Reset()
        {
            if (original != null)
                data = original.Clone() as byte[,];
        }

        public void Initialize()
        {
            InitializeData();
            answer = data.Clone() as byte[,];
            int max;
            switch (Level)
            {
                case 1:
                    max = 40; break;
                case 2:
                    max = 50; break;
                case 3:
                    max = 60; break;
                case 4:
                    max = 70; break;
                default:
                    max = 40; break;
            }
            for (int i = 0; i < max; i++)
            {
                data[(byte)r.Next(0, 9), (byte)r.Next(0, 9)] = 0;
            }
            original = data.Clone() as byte[,];
            if (OnInitializeFinish != null)
                OnInitializeFinish.Invoke();
            undoStack.Clear();
            redoStack.Clear();
        }

        public byte[,] Current
        {
            get { return data; }
        }

        public byte[,] Original
        {
            get { return original; }
        }


        [XmlIgnore]
        public bool IsFinish
        {
            get
            {
                for (byte i = 0; i < 9; i++)
                    for (byte j = 0; j < 9; j++)
                    {
                        if (data[i, j] == 0)
                            return false;
                        if (!CanSet(i, j, data[i, j]))
                            return false;
                    }
                return true;
            }
        }

        private byte level = 1;

        [XmlIgnore]
        public byte Level
        {
            get { return level; }
            set
            {
                if (value < 4 && value > 0)
                    level = value;
                Initialize();
            }
        }

        private ISudokuSolution solution;

        [XmlIgnore]
        public ISudokuSolution Solution
        {
            get { return solution; }
            set
            {
                if (value == null) return;
                solution = value;
                solution.Sudoku = this;
            }
        }

        public void Solve()
        {
            if (answer == null) return;
            data = Solution != null ? Solution.Solve().Current : answer.Clone() as byte[,];
        }


        public bool CanSet(byte row, byte col, byte value)
        {
            for (int i = 0; i < 9; i++)
            {
                if (data[i, col] == value && i != row)
                {
                    return false;
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (data[row, i] == value && i != col)
                {
                    return false;
                }
            }

            byte ro = (byte)(row / 3 * 3);
            byte c = (byte)(col / 3 * 3);

            for (int i = ro; i < 2; i++)
            {
                for (int j = c; j < 2; j++)
                {
                    if (data[i, j] == value && i != row && j != col)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public List<byte> CanSetValues(byte row, byte col)
        {
            return Solution != null ? Solution.CanSetValues(row, col) : canSetValues(row, col);
        }

        public void Dispose()
        {
            data = null;
            original = null;
            answer = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        private static readonly byte[] numbers = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        private List<byte> canSetValues(byte row, byte col)
        {
            List<byte> temp = new List<byte>(20);
            for (int i = 0; i < 9; i++)
            {
                if (data[i, col] != 0)
                {
                    temp.Add(data[i, col]);
                }
            }
            for (int i = 0; i < 9; i++)
            {
                if (data[row, i] != 0)
                {
                    temp.Add(data[row, i]);
                }
            }

            byte ro = (byte)(row / 3 * 3);
            byte c = (byte)(col / 3 * 3);

            for (int i = ro; i < 2; i++)
            {
                for (int j = c; j < 2; j++)
                {
                    if (data[i, j] != 0)
                    {
                        temp.Add(data[i, j]);
                    }
                }
            }

            List<byte> bytes = new List<byte>(9);
            bytes.AddRange(numbers);
            foreach (var t in temp)
            {
                if (bytes.Contains(t))
                    bytes.Remove(t);
            }
            return bytes;
        }
    }
}
