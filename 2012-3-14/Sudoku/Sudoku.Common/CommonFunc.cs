using System;
using System.Reflection;
using System.Xml;

namespace Sudoku.Common
{
    /// <summary>
    /// 泛型委托
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <returns>泛型委托</returns>
    public delegate T Function<T>();

    /// <summary>
    /// 无返回值委托
    /// </summary>
    public delegate void Function();

    /// <summary>
    /// 定义了一部分公用方法
    /// </summary>
    public static class CommonFunc
    {
        /// <summary>
        /// 安全运行方法，抛出异常
        /// 内部已经自动与异常挂接
        /// 使用了条件编译，DEBUG时会抛出异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="function">具体方法</param>
        /// <param name="errorValue">默认值</param>
        /// <returns>运行结果</returns>
        public static T SafetyRun<T>(Function<T> function, T errorValue)
        {
            return SafetyRun(function, errorValue, true);
        }
        /// <summary>
        /// 安全运行方法
        /// 内部已经自动与异常挂接
        /// 使用了条件编译，DEBUG时会抛出异常
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="function">具体方法</param>
        /// <param name="errorValue">默认值</param>
        /// <param name="isThrow">是否抛出异常</param>
        /// <returns>运行结果</returns>
        public  static  T SafetyRun<T>(Function<T> function, T errorValue,bool  isThrow)
        {
            try
            {
                return function.Invoke();
            }
            catch
            {
#if DEBUG
                throw;
#endif
                if (isThrow) throw;
                return errorValue;
            }
        }

        /// <summary>
        /// 安全运行方法,抛出异常
        /// 内部已经自动与异常挂接
        /// 使用了条件编译，DEBUG时会抛出异常
        /// </summary>
        /// <param name="function">具体方法</param>
        public static void SafetyRun(Function function)
        {
            SafetyRun(function, true);
        }
        /// <summary>
        /// 安全运行方法
        /// 内部已经自动与异常挂接
        /// 使用了条件编译，DEBUG时会抛出异常
        /// </summary>
        /// <param name="function">具体方法</param>
        /// <param name="isThrow">是否抛出异常</param>
        public static void SafetyRun(Function function,bool  isThrow)
        {
            try
            {
                function.Invoke();
            }
            catch
            {
#if DEBUG
                 throw;
#endif
                 if (isThrow) throw;
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="xpath">类型的路径</param>
        /// <param name="args">输入参数</param>
        /// <returns>类型实例</returns>
        public static T CreateInstance<T>(string xpath, params  object[] args)
        {
            return SafetyRun(delegate
                                    {
                                        T ret = default(T);
                                        XmlNode node = ReadNode(xpath);
                                        if (node != null && node.ChildNodes.Count > 0)
                                        {
                                            string assembly = node.SelectSingleNode("assembly").InnerText;
                                            string classname = node.SelectSingleNode("classname").InnerText;
                                            Type type = Assembly.LoadFrom(assembly).GetType(classname);
                                            object obj = Activator.CreateInstance(type, args);
                                            ret = (T)obj;
                                        }
                                        return ret;
                                    }, default(T));
        }
        /// <summary>
        /// 读取节点
        /// </summary>
        /// <param name="xpath">节点路径</param>
        /// <returns>一个Xml节点</returns>
        public static XmlNode ReadNode(string xpath)
        {
            return SafetyRun(delegate
                          {
                              XmlDocument document = new XmlDocument();
                              document.Load("Sudoku.xml");
                              XmlNode node = document.SelectSingleNode(xpath);
                              return node;
                          }, null);
        }


    }
}
