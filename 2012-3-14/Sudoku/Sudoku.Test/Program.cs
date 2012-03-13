using System;
using System.Collections.Generic;

namespace Sudoku.Test
{
    class Program
    {
        static byte[,] data = new byte[9, 9];

        static void Main()
        {
            List<byte> num = new List<byte>(9) { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            byte[] cells=new byte[9];

            Random r=new Random();


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

            FillBoxByBox(0, 1);
            // Box 5
            FillBoxByBox(1, 1);
            // Box 6
            FillBoxByBox(2, 1);
            // Box 7
            FillBoxByBox(0, 2);
            // Box 8
            FillBoxByBox(1, 2);
            // Box 9
            FillBoxByBox(2, 2);

            


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(data[i,j]);
                    Console.Write("\t");
                }
                Console.WriteLine();
            }



            Console.ReadKey();
        }

        private static void FillBoxByBox(int col,int row)
        {
            col = col*3;
            row = row*3;
            int index = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    index=(col+i+1==9?0:col+i+1);
                    data[col + i, row + j] = data[index, row + j - 3];
                }
            }
        }
    }
}
