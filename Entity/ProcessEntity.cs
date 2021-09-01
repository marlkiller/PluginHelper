using System;

namespace PluginHelper.Entity
{
    public class ProcessEntity
    {
        private int processId;
        private string processFullPath;

        public int ProcessId
        {
            get => processId;
            set => processId = value;
        }

        public string ProcessFullPath
        {
            get => processFullPath;
            set => processFullPath = value;
        }
    }
}