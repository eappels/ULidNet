using UnityEngine;

public class LidServerBehaviour : MonoBehaviour
{

    private LidServer lidserver;

    private void Start()
    {
        lidserver = new LidServer();
    }

    private void FixedUpdate()
    {
        if (lidserver != null) lidserver.MessagePump();
    }

    public void StartServer()
    {
        lidserver.StartServer();
    }

    public void StopServer()
    {
        lidserver.StopServer();
    }

    public bool Isrunning
    {
        get { return lidserver != null && lidserver.netserver.Status == Lidgren.Network.NetPeerStatus.Running; }
    }
}