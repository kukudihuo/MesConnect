using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MainApp
{
    /// <summary>
    /// 单客户链接的tcp server类
    /// </summary>
    public class SingleTcpServer
    {
        public SingleTcpServer()
        {

        }

        bool mIsOpend = false;
        TcpListener mListener;
        object mLockObj = new object();
        public int Open(string strIP, int port)
        {
            lock (mLockObj)
            {
                if (mIsOpend)
                {
                    Close();
                }
                IPAddress address = (0 == strIP.Length) ? IPAddress.Any : IPAddress.Parse(strIP);
                try
                {
                    mListener = new TcpListener(address, port);
                    mListener.Start();
                    DoAccept();
                    mIsOpend = true;
                    return 0;
                }
                catch (Exception ex)
                {
                    return -2;
                }
            }
            return -1;
        }

        void DoAccept()
        {
            mListener.BeginAcceptTcpClient(new AsyncCallback(HandleTcpClientAccepted), mListener);
        }

        TcpClient mLastClient;
        TcpClient mCurrentClient;
        private void HandleTcpClientAccepted(IAsyncResult ar)
        {
            lock (mLockObj)
            {
                TcpListener tcpListener = (TcpListener)ar.AsyncState;
                try
                {
                    mCurrentClient = tcpListener.EndAcceptTcpClient(ar);
                    if (null != mLastClient) { mLastClient.Close(); }
                    mLastClient = mCurrentClient;
                    if (null != mAcceptCb) { mAcceptCb(); }
                    DoReceive();
                    DoAccept();
                }
                catch (Exception ex)
                {

                }
            }
        }

        static readonly int MAX_BFFER = 4096;
        class StateObject
        {
            public byte[] mBuffer = new byte[MAX_BFFER];
        }

        void HandleDatagramReceived(IAsyncResult ar)
        {
            StateObject obj = (StateObject)ar.AsyncState;
            int numberOfReadBytes = 0;
            //if (null != mCallback)
            {
                lock (mLockObj)
                {
                    try
                    {
                        numberOfReadBytes = mCurrentClient.GetStream().EndRead(ar);
                        if (null != mCallback)
                            mCallback(obj.mBuffer, numberOfReadBytes);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("HandleDatagramReceived Excption,may be two client connect with the same port");
                    }
                }
            }
            if (mIsOpend && numberOfReadBytes > 0)//mCurrentClient.Connected
                DoReceive();
            else
            {
                mCurrentClient.Close();
            }

        }

        private void DoReceive()
        {
            try
            {
                NetworkStream stream = mCurrentClient.GetStream();
                //if (stream.DataAvailable)
                { 
                    StateObject state = new StateObject();
                    stream.BeginRead(state.mBuffer, 0, MAX_BFFER, HandleDatagramReceived, state);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excption");
            }
        }

        public int Close()
        {
            lock (mLockObj)
            {
                if (mIsOpend && null != mListener)
                {
                    mListener.Stop();
                    mIsOpend = false;
                }
            }
            return 0;
        }

        public int Send(byte[] msg)
        {

            if (null != mLastClient)
            {
                try
                {
                    NetworkStream stream = mLastClient.GetStream();
                    stream.Write(msg, 0, msg.Length);
                }
                catch (Exception ex)
                {

                }
            }
            return 0;
        }

        public delegate void ReceiveMsgCallback(byte[] msg, int len);
        ReceiveMsgCallback mCallback = null;
        public void SetReceiveCallback(ReceiveMsgCallback cb)
        {
            mCallback = cb;
        }

        public delegate void AcceptCallback();
        AcceptCallback mAcceptCb = null;
        public void SetAcceptCallback(AcceptCallback cb)
        {
            mAcceptCb = cb;
        }
    }
}
