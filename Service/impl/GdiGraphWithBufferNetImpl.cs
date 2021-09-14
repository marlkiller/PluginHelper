using System;

namespace PluginHelper.Service
{
    public class GdiGraphWithBufferNetImpl : IGraph
    {
       
        public GameEspForm gameEspForm;

        public void init(IntPtr processId)
        {
            if (gameEspForm != null) return;
            gameEspForm = new GameEspForm();
            gameEspForm.prosessId = processId;
        }

        public void destory()
        {
            if (gameEspForm.thread.IsAlive)
            {
                gameEspForm.thread.Abort();
            }
            gameEspForm.Close();
        }

        public void start()
        {
            gameEspForm.thread.Start(new object());
        }
    }
}