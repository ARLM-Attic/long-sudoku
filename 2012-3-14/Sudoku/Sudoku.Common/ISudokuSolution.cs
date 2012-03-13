
using System.Collections.Generic;

namespace Sudoku.Common
{
    /// <summary>
    /// 数独解决方法实例
    /// </summary>
    public interface ISudokuSolution
    {
        /// <summary>
        /// 设置或获取要解决的数独
        /// </summary>
        ISudoku Sudoku { get; set; }

        /// <summary>
        /// 解决数独
        /// </summary>
        /// <returns>解决完成的数独</returns>
        ISudoku Solve();

        /// <summary>
        /// 检测该位置的点是否能放置
        /// </summary>
        /// <param name="row">行坐标</param>
        /// <param name="col">列坐标</param>
        /// <param name="value">值</param>
        /// <returns>如果能放置则返回true</returns>
        bool CanSet(byte row, byte col,byte  value);
        /// <summary>
        /// 获取该位置放置的值集合
        /// </summary>
        /// <param name="row">行坐标</param>
        /// <param name="col">列坐标</param>
        /// <returns>放置值的集合</returns>
        List<byte> CanSetValues(byte row, byte col);
    }
}
