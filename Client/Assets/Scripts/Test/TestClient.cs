using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;

public class TestClient : MonoBehaviour
{
    // �������� IP ��ַ�Ͷ˿ں�
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
            // ����һ�� UdpClient ʵ��
            using (UdpClient client = new UdpClient())
            {
                // Ҫ���͵���Ϣ
                string message = "Hello, UDP Server!";
                byte[] sendBytes = Encoding.ASCII.GetBytes(message);

                // ���������ս��
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                // �������������Ϣ
                client.Send(sendBytes, sendBytes.Length, serverEndPoint);

                Debug.Log($"Sent message: {message} to {serverIp}:{serverPort}");

                // ���շ���������Ӧ
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
