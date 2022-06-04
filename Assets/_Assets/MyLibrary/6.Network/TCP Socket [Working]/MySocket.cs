using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MyLibrary.Network
{
    // TODO : 1. Send 부분 작업 혹은 Receiver Sender Class 분할
    // TODO : 2. 각종 기본 셋팅 설정( IP,Port 프로퍼티 화 등 ) 가능하게 작업.
    
    public class MySocket
    {
        private TcpListener listener;

        // LoopBack
        [SerializeField] private string IPAdress = "127.0.0.1";
        [SerializeField] private int Port = 8888;

        private int dataLength; // Receive Data Length. (byte)
        Queue<byte> readedBytes = new Queue<byte>();

        byte[] bytes = new byte[2097152];
        TcpClient tcpClient;

        public static bool isReaded = false;
        public static bool isRendered = false;

        public MySocket()
        {

        }

        public void Close()
        {
            listener.Stop();
        }

        int ReadProtocolID(NetworkStream ns)
        {
            int remaindByte = 4;
            while (remaindByte != 0)
            {
                if (ns.DataAvailable && ns.CanRead)
                {
                    int readedCount = ns.Read(bytes, 0, remaindByte);
                    for (int i = 0; i < readedCount; ++i)
                        readedBytes.Enqueue(bytes[i]);
                    remaindByte -= readedCount;
                }
            }

            byte[] datas = new byte[4];
            for (int i = 0; i < 4; ++i)
                datas[i] = readedBytes.Dequeue();

            return BitConverter.ToInt32(datas, 0);
        }

        int ReadLength(NetworkStream ns)
        {
            int remaindByte = 4;
            while (remaindByte != 0)
            {
                if (ns.DataAvailable && ns.CanRead)
                {
                    int readedCount = ns.Read(bytes, 0, remaindByte);
                    for (int i = 0; i < readedCount; ++i)
                        readedBytes.Enqueue(bytes[i]);
                    remaindByte -= readedCount;
                }
            }

            byte[] datas = new byte[4];
            for (int i = 0; i < 4; ++i)
                datas[i] = readedBytes.Dequeue();

            return BitConverter.ToInt32(datas, 0);

        }

        string ReadString(NetworkStream ns)
        {
            return Encoding.UTF8.GetString(ReadingBytes(ns));
        }

        T[] ByteToArray<T>(byte[] bytes) where T : struct
        {
            T[] array = new T[bytes.Length / System.Runtime.InteropServices.Marshal.SizeOf<T>()];
            Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
            return array;
        }

        byte[] ReadingBytes(NetworkStream ns)
        {
            int remaindByte = ReadLength(ns);
            dataLength = remaindByte;
            while (remaindByte != 0)
            {
                if (ns.DataAvailable && ns.CanRead)
                {
                    int readedCount = ns.Read(bytes, 0, remaindByte);
                    for (int i = 0; i < readedCount; ++i)
                        readedBytes.Enqueue(bytes[i]);
                    remaindByte -= readedCount;
                }
            }

            byte[] datas = new byte[dataLength];
            for (int i = 0; i < dataLength; ++i)
                datas[i] = readedBytes.Dequeue();

            return datas;
        }

        void ReadCallBack(IAsyncResult result)
        {
            isReaded = false;
            Console.WriteLine("Client Connected");
            TcpListener listener = (TcpListener) result.AsyncState;
            tcpClient = listener.EndAcceptTcpClient(result);

            Task.Run(() =>
            {
                NetworkStream ns = tcpClient.GetStream();
                ns.Write(BitConverter.GetBytes(1), 0, 4);

                try
                {
                    while (tcpClient.Connected)
                    {
                        if (ns.DataAvailable)
                        {
                            int protocolID = ReadProtocolID(ns);

                            switch (protocolID)
                            {
                                case 0:
                                    //Read Proc
                                    break;

                                case -1:
                                    //Exit...
                                    break;
                            }
                        }
                        else
                        {
                            ns.Write(new byte[1], 0, 1);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message + e.StackTrace);
                }

                ns.Close();
                tcpClient.Close();
                Debug.Log("disconnected");
            });
        }

        public void StartListening()
        {
            if (tcpClient != null && tcpClient.Connected)
            {
                tcpClient.GetStream().Close();
                tcpClient.Close();
            }

            try
            {
                if (listener == null)
                {
                    listener = new TcpListener(IPAddress.Parse(IPAdress), Port);
                    listener.Start();
                }

                listener.BeginAcceptTcpClient(ReadCallBack, listener);
                Debug.Log("Server is listening");
            }
            catch (SocketException socketException)
            {
                Debug.Log("SocketException " + socketException.ToString());
            }
        }
    }
}
