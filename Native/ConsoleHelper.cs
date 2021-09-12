using System;

namespace PluginHelper.Native
{
    
    public enum Level
    {
        DEBUG,
        INFO,
        WAIN,
        ERROR,
    }
    public class ConsoleHelper
    {
        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="format"></param>  
        /// <param name="args"></param>  
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }
        public static void WriteLine(Level level,string format, params object[] args)
        {
            WriteLine(string.Format(format, args),level);
        }
        
        public static void WriteLine(Level level, string format)
        {
            WriteLine(format,level);
        }

        /// <summary>  
        /// 输出信息  
        /// </summary>  
        /// <param name="output"></param>
        /// <param name="level"></param>  
        public static void WriteLine(string output,Level level = Level.INFO)
        {
            Console.ForegroundColor = GetConsoleColor(level);
            Console.WriteLine(@"[{0}]{1}", DateTimeOffset.Now, output);
        }

        /// <summary>  
        /// 根据输出文本选择控制台文字颜色  
        /// </summary>  
        /// <param name="output"></param>  
        /// <returns></returns>  
        private static ConsoleColor GetConsoleColor(Level level)
        {
            
            switch (level)
            {
                case Level.INFO:
                    return ConsoleColor.Green;
                case Level.WAIN:
                    return ConsoleColor.Yellow;
                case Level.ERROR:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}