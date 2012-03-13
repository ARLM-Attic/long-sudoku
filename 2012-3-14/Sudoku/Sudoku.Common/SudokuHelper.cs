using System.Xml;

namespace Sudoku.Common
{
    /// <summary>
    /// 数独辅助类
    /// </summary>
    public static class SudokuHelper
    {
        /// <summary>
        /// 内部辅助类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="childname">子节点名称</param>
        /// <param name="args">传入参数</param>
        /// <returns>类型实例</returns>
        private static T Create<T>(string childname, params  object[] args)
        {
            return CommonFunc.SafetyRun(delegate
                                            {
                                                XmlNode node = CommonFunc.ReadNode("/sudoku/default");
                                                string nodename = node.SelectSingleNode("nodename").InnerText;
                                                string instancename = node.SelectSingleNode("instancename").InnerText;
                                                string xpath =
                                                    string.Format("/sudoku/node[@name='{0}']/{1}[@name='{2}']", nodename,
                                                                  childname, instancename);
                                                return CommonFunc.CreateInstance<T>(xpath, args);
                                            }, default(T));
        }

        /// <summary>
        /// 创建默认数独实例
        /// </summary>
        /// <returns>数独实例</returns>
        public static  ISudoku CreateDefaultSudoku()
        {
            ISudoku sudoku = Create<ISudoku>("instance",null);
            sudoku.Solution = Create<ISudokuSolution>("solution",null);
            return sudoku;
        }
        /// <summary>
        /// 创建数独实例
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>数独实例</returns>
        public static ISudoku CreateSudoku(params  object[] args)
        {
            return Create<ISudoku>("instance", null);
        }
        /// <summary>
        /// 创建数独解决方法实例
        /// </summary>
        /// <param name="args">传入参数</param>
        /// <returns>解决方法实例</returns>
        public static ISudokuSolution CreateSolution(params  object[] args)
        {
            return Create<ISudokuSolution>("solution", args);
        }
    }
}
