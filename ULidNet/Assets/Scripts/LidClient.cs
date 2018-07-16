using Lidgren.Network;
using System;

public class LidClient : LidPeer
{

    public event Action OnConnected = null, OnDisconnected = null;
    public readonly NetClient netclient;

    public LidClient()
    {
        instance = this;
        isserver = false;
        isclient = true;
        var config = CreateConfig();
        netpeer = netclient = new NetClient(config);
        netclient.Start();
    }

    public void Connect(string connectionstring)
    {
        string[] tmparray = connectionstring.Split(':');
        netclient.Connect((string)tmparray[0], int.Parse(tmparray[1]));
    }

    public void Disconnect()
    {
        netclient.Disconnect("Client disconnect");
    }

    public void Shutdown()
    {
        netclient.Shutdown("Client shutdown");
    }

    protected override void OnStatusChanged(NetIncomingMessage nim)
    {
        switch (nim.SenderConnection.Status)
        {
            case NetConnectionStatus.Connected:
                if (OnConnected != null) OnConnected();
                break;

            case NetConnectionStatus.Disconnected:
                if (OnDisconnected != null) OnDisconnected();
                break;
        }
    }
}