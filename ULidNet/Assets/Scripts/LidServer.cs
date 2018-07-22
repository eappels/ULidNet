using Lidgren.Network;
using System;

public class LidServer : LidPeer
{

    public event Action<string> DebugMessage = null, UnknownMessage = null;
    public readonly NetServer netserver;

    public LidServer()
        :base ()
    {
        instance = this;
        isserver = true;
        isclient = false;
        var config = CreateConfig();
        config.Port = LidPeer.PORT;
        netpeer = netserver = new NetServer(config);
    }

    public void StartServer()
    {
        netserver.Start();
    }

    public void StopServer()
    {
        netserver.Shutdown("Server shutdown");
    }

    protected override void OnDebugMessage(NetIncomingMessage nim)
    {
        if (DebugMessage != null) DebugMessage(nim.ReadString());
    }

    protected override void OnUnknownMessage(NetIncomingMessage nim)
    {
        if (UnknownMessage != null) UnknownMessage(nim.ReadString());
    }
}