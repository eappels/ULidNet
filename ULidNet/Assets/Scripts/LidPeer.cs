using Lidgren.Network;
using UnityEngine;

public abstract class LidPeer
{

    public NetPeer netpeer;
    public static LidPeer instance;
    public static bool isserver, isclient;
    public const string APPID = "MyGame";
    public const int PORT = 25000;

    public LidPeer()
        : base()
    {
    }

    public void MessagePump()
    {
        NetIncomingMessage nim;
        while ((nim = netpeer.ReadMessage()) != null)
        {
            switch (nim.MessageType)
            {
                case NetIncomingMessageType.Data:
                    OnDataMessage(nim);
                    break;

                case NetIncomingMessageType.StatusChanged:
                    OnStatusChanged(nim);
                    break;

                case NetIncomingMessageType.VerboseDebugMessage:
                case NetIncomingMessageType.DebugMessage:
                case NetIncomingMessageType.WarningMessage:
                case NetIncomingMessageType.ErrorMessage:
                case NetIncomingMessageType.Error:
                    OnDebugMessage(nim);
                    break;

                #region Ununsed types
                //case NetIncomingMessageType.UnconnectedData:
                //    break;
                //case NetIncomingMessageType.ConnectionApproval:
                //    break;
                //case NetIncomingMessageType.Receipt:
                //    break;
                //case NetIncomingMessageType.DiscoveryRequest:
                //    break;
                //case NetIncomingMessageType.DiscoveryResponse:
                //    break;
                //case NetIncomingMessageType.NatIntroductionSuccess:
                //    break;
                //case NetIncomingMessageType.ConnectionLatencyUpdated:
                //    break;
                #endregion

                default:
                    OnUnknownMessage(nim);
                    break;
            }
            netpeer.Recycle(nim);
        }
    }

    public NetPeerConfiguration CreateConfig()
    {
        return new NetPeerConfiguration(APPID);
    }

    public virtual NetOutgoingMessage CreateMessage()
    {
        return netpeer.CreateMessage();
    }

    protected virtual void OnDataMessage(NetIncomingMessage nim)
    {
        Debug.Log("Data message: " + nim.LengthBytes + " bytes");
    }

    protected virtual void OnDebugMessage(NetIncomingMessage nim)
    {
        Debug.Log("Debug message: " + nim.ReadString());
    }

    protected virtual void OnStatusChanged(NetIncomingMessage nim)
    {
        Debug.Log("Status changed: " + nim.SenderConnection.Status);
    }

    protected virtual void OnUnknownMessage(NetIncomingMessage nim)
    {
        Debug.Log("Unhandled message type: " + nim.MessageType);
    }
}