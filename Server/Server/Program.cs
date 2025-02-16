using System;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FrameSyncServer server = new FrameSyncServer();
            server.Start();
        }
    }
}
