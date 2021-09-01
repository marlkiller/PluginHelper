using System;

namespace PluginHelper.Handler
{
    public class LogEventHandler:EventArgs
    {
        public string log { get; set; }
        public LogEventHandler(string log)
        {
            this.log = log;
        }
    }
}