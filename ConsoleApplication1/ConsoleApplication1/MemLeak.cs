namespace ConsoleApplication1
{
    class MemLeak
    {
        private byte[] allocatedMemory;
        private EventClass evEvent;

        public MemLeak(EventClass evPass)
        {
            evEvent = evPass;
            allocatedMemory = new byte[10000];
            evPass.StrValChanged += new EventHandler(evPass_StringValueChanged);
        }

        private void evPass_StringValueChanged(object sender, EventArgs e)
        {
            Console.WriteLine("evPass_StringValueChanged");
        }

    }
}
