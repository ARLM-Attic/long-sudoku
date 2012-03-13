using System;
using System.Collections.Generic;

namespace Sudoku.Common
{
    public delegate void FinishHandler();

    /// <summary>
    /// 数独接口
    /// </summary>
    public interface ISudoku:IDisposable
    {
        event FinishHandler OnInitializeFinish;

        event FinishHandler OnLoadFinish;

        /// <summary>
        /// 设置点
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>点</returns>
        byte this[byte x, byte y] { get; set; }

        /// <summary>
        /// 载入数独
        /// </summary>
        /// <param name="path">路径</param>
        void Load(string path);
        /// <summary>
        /// 保存数独
        /// </summary>
        /// <param name="path">路径</param>
        void Save(string path);

        /// <summary>
        /// 撤销
        /// </summary>
        void Undo();
        /// <summary>
        /// 重复
        /// </summary>
        void Redo();
        /// <summary>
        /// 重置
        /// </summary>
        void Reset();

        /// <summary>
        /// 初始化数独
        /// </summary>
        void Initialize();

        /// <summary>
        /// 获取数独数据
        /// </summary>
        /// <returns></returns>
        byte[,] Current { get; }
        /// <summary>
        /// 原数组
        /// </summary>
        byte[,] Original { get; }
        /// <summary>
        /// 等级
        /// </summary>
        byte Level { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        bool IsFinish { get; }

        /// <summary>
        /// 设置或获取数独的解决方法
        /// </summary>
        ISudokuSolution Solution { get; set; }
        /// <summary>
        /// 使用内置的解决方法解决当前数独
        /// </summary>
        void Solve();

        /// <summary>
        /// 检测该位置的点是否能放置
        /// </summary>
        /// <param name="row">行坐标</param>
        /// <param name="col">列坐标</param>
        /// <param name="value">值</param>
        /// <returns>如果能放置则返回true</returns>
        bool CanSet(byte row, byte col, byte value);
        /// <summary>
        /// 获取该位置放置的值集合
        /// </summary>
        /// <param name="row">行坐标</param>
        /// <param name="col">列坐标</param>
        /// <returns>放置值的集合</returns>
        List<byte> CanSetValues(byte row, byte col);
    }
}
