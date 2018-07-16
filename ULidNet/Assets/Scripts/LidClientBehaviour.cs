using UnityEngine;

public class LidClientBehaviour : MonoBehaviour
{

    public LidClient lidclient;

    private void Start()
    {
        lidclient = new LidClient();
    }

    public void FixedUpdate()
    {
        if (lidclient != null) lidclient.MessagePump();
    }

    public void Connect(string connectionstring)
    {
        lidclient.Connect(connectionstring);
    }

    public void Disconnect()
    {
        lidclient.Disconnect();
    }

    public bool Isrunning
    {
        get { return lidclient != null && lidclient.netclient.Status == Lidgren.Network.NetPeerStatus.Running; }
    }

    public bool Isconnected
    {
        get { return lidclient.netclient.ConnectionStatus == Lidgren.Network.NetConnectionStatus.Connected; }
    }
}