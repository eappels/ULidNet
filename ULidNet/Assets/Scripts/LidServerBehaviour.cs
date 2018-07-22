using UnityEngine;
using UnityEngine.UI;

public class LidServerBehaviour : MonoBehaviour
{

    public GameObject btn_StartStopServer, btn_Back;
    public LidServer lidserver;
    private UIManager uimanager;

    private void OnEnable()
    {
        uimanager = UIManager.GetInstance;
        btn_Back.GetComponent<Button>().onClick.AddListener(btn_Back_Click);
        uimanager.ShowOrHideBtnBack(true);
        lidserver = new LidServer();
        lidserver.DebugMessage += OnDebugMessage;
        lidserver.UnknownMessage += OnUnknownMessage;
        btn_StartStopServer.GetComponent<Button>().onClick.AddListener(btn_StartStopServer_Click);
    }

    private void OnDisable()
    {
        lidserver.DebugMessage -= OnDebugMessage;
        lidserver.UnknownMessage -= OnUnknownMessage;
        lidserver = null;
        btn_Back.GetComponent<Button>().onClick.RemoveAllListeners();
        btn_StartStopServer.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void btn_Back_Click()
    {
        uimanager.ShowOrHideMenuUI(true);
        uimanager.ShowOrHideServerUI(false);
        uimanager.ShowOrHideBtnBack(false);
    }

    private void OnDebugMessage(string debugstring)
    {
        Debug.Log(debugstring);
    }

    private void OnUnknownMessage(string unknownstring)
    {
        Debug.Log(unknownstring);
    }

    private void FixedUpdate()
    {
        if (lidserver != null) lidserver.MessagePump();
    }

    public void btn_StartStopServer_Click()
    {
        if (lidserver.netserver.Status == Lidgren.Network.NetPeerStatus.NotRunning)
        {
            uimanager.ShowOrHideBtnBack(false);
            btn_StartStopServer.GetComponentInChildren<Text>().text = "Stop server";
            lidserver.StartServer();
        }
        else
        {
            uimanager.ShowOrHideBtnBack(true);
            btn_StartStopServer.GetComponentInChildren<Text>().text = "Start server";
            lidserver.StopServer();
        }
    }
}