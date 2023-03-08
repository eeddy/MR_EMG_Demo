using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

public class EMGReader : MonoBehaviour
{
    public string IP = "127.0.0.1";
    public int port = 12346;
    Thread readThread;
    UdpClient client;
    public string control = "";
    private float speed = 0.0f;
    
    async void Start() 
    {
#if UNITY_EDITOR
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
#endif
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading messages
    public void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        client.Close();
    }

    // receive thread function
    private void ReceiveData()
    {
        client = new UdpClient(port);
        while (true)
        {
            // receive bytes
            IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
            byte[] buff = client.Receive(ref anyIP);

            // encode UTF8-coded bytes to text format
            string text = Encoding.UTF8.GetString(buff);
            string[] parts = text.Split(' ');
            control = parts[0];
            // speed = float.Parse(parts[1]);
        }
    }

    public string ReadControlFromArmband() 
    {
        // Reset before a new read
        string temp = control;
        control = "";
        return temp;
    }

    public float ReadSpeedFromArmband() 
    {
        return speed;
    }
}