using Lidgren.Network;
using System;

public class LidClient : LidPeer
{

    public event Action<string> DebugMessage = null, UnknownMessage = null;
    public event Action Connected = null, Disconnected = null;
    public readonly NetClient netclient;

    public LidClient()
        : base()
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
        if (connectionstring.Contains(":"))
        {
            string[] tmpstringarray = connectionstring.Split(':');
            netclient.Connect(tmpstringarray[0], int.Parse(tmpstringarray[1]));
        }
        else
        {
            netclient.Connect(connectionstring, LidPeer.PORT);
        }
    }

    public void Disconnect()
    {
        netclient.Disconnect("Client disconnect");
    }

    public void Shutdown()
    {
        netclient.Shutdown("Client shutdown");
    }

    protected override void OnDebugMessage(NetIncomingMessage nim)
    {
        if (DebugMessage != null) DebugMessage(nim.ReadString());
    }

    protected override void OnUnknownMessage(NetIncomingMessage nim)
    {
        if (UnknownMessage != null) UnknownMessage(nim.ReadString());
    }

    protected override void OnStatusChanged(NetIncomingMessage nim)
    {
        switch (nim.SenderConnection.Status)
        {
            case NetConnectionStatus.Connected:
                if (Connected != null) Connected();
                break;
            case NetConnectionStatus.Disconnected:
                if (Disconnected != null) Disconnected();
                break;
        }
    }
}