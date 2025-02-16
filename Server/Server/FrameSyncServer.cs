using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Server
{
    internal class FrameSyncServer
    {
        private const int BufferSize = 1024;
        private const int Port = 12345;
        private UdpClient server;
        private IPEndPoint clientEndPoint;
        private Dictionary<IPEndPoint, byte[]> clientInputs;
        private byte[] combinedInput;

        public FrameSyncServer()
        {
            server = new UdpClient(Port);
            clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            clientInputs = new Dictionary<IPEndPoint, byte[]>();
            combinedInput = new byte[0];
        }

        public void Start()
        {
            Console.WriteLine($"Server started, waiting for client connections...");
            while (true)
            {
                try
                {
                    byte[] receiveBytes = server.Receive(ref clientEndPoint);
                    if (!clientInputs.ContainsKey(clientEndPoint))
                    {
                        clientInputs.Add(clientEndPoint, receiveBytes);
                    }
                    else
                    {
                        clientInputs[clientEndPoint] = receiveBytes;
                    }

                    // 整合所有客户端输入
                    combinedInput = CombineInputs();
                    Console.WriteLine(Encoding.ASCII.GetString(combinedInput));
                    // 广播整合后的输入给所有客户端
                    Broadcast(combinedInput);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: {e.Message}");
                }
            }
        }

        private byte[] CombineInputs()
        {
            List<byte> combined = new List<byte>();
            foreach (var input in clientInputs.Values)
            {
                combined.AddRange(input);
            }
            return combined.ToArray();
        }

        private void Broadcast(byte[] data)
        {
            foreach (var endPoint in clientInputs.Keys)
            {
                try
                {
                    server.Send(data, data.Length, endPoint);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error broadcasting to {endPoint}: {e.Message}");
                }
            }
        }
    }
}
