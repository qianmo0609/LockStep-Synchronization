using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class TestClient : MonoBehaviour
{
    // 服务器的 IP 地址和端口号
    string serverIp = "127.0.0.1";
    int serverPort = 12345;


    void Start()
    {
        InitClient();
    }
    
    void InitClient()
    {
        try
        {
            // 创建一个 UdpClient 实例
            using (UdpClient client = new UdpClient())
            {
                // 要发送的消息
                string message = "Hello, UDP Server!";
                byte[] sendBytes = Encoding.ASCII.GetBytes(message);

                // 服务器的终结点
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                // 向服务器发送消息
                client.Send(sendBytes, sendBytes.Length, serverEndPoint);

                Debug.Log($"Sent message: {message} to {serverIp}:{serverPort}");

                // 接收服务器的响应
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receiveBytes = client.Receive(ref remoteEndPoint);
                string receivedMessage = Encoding.ASCII.GetString(receiveBytes);

                Debug.Log($"Received message: {receivedMessage} from {remoteEndPoint}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e.Message}");
        }
    }
}
