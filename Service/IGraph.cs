using System;

namespace PluginHelper.Service
{
    public interface IGraph
    {

        void init(IntPtr processId);
        
        void destory();

        void start();
    }
}