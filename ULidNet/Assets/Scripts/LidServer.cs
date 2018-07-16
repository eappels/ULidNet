using Lidgren.Network;

public class LidServer : LidPeer
{

    public readonly NetServer netserver;

    public LidServer()
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

    protected override void OnStatusChanged(NetIncomingMessage nim)
    {
        switch (nim.SenderConnection.Status)
        {
            case NetConnectionStatus.Connected:
                break;

            case NetConnectionStatus.Disconnected:
                break;
        }
    }
}