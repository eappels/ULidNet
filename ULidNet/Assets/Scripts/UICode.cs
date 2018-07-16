using UnityEngine;
using UnityEngine.UI;

public class UICode : MonoBehaviour
{

    public GameObject pnl_Menu, pnl_Server, pnl_Client, btn_Server, btn_Client, btn_StartStopServer, txt_IpaddressText, btn_ConnectDisconnect;
    private LidServerBehaviour lsbhaviour;
    private LidClientBehaviour lcbhaviour;

    private void Awake()
    {
        pnl_Menu.SetActive(true);
        pnl_Server.SetActive(false);
        pnl_Client.SetActive(false);
    }

    private void Start()
    {
        if (Debug.isDebugBuild)
        {
            txt_IpaddressText.GetComponent<InputField>().text = "127.0.0.1:25000";
        }
        btn_Server.GetComponent<Button>().onClick.AddListener(btn_Server_Click);
        btn_Client.GetComponent<Button>().onClick.AddListener(btn_Client_Click);
        btn_StartStopServer.GetComponent<Button>().onClick.AddListener(btn_StartStopServer_Click);
        btn_ConnectDisconnect.GetComponent<Button>().onClick.AddListener(btn_ConnectDisconnect_Click);
        lsbhaviour = gameObject.GetComponent<LidServerBehaviour>();
        lsbhaviour.enabled = false;
        lcbhaviour = gameObject.GetComponent<LidClientBehaviour>();
        lcbhaviour.enabled = false;
    }

    private void btn_Server_Click()
    {
        pnl_Menu.SetActive(false);
        pnl_Server.SetActive(true);
        pnl_Client.SetActive(false);
        lsbhaviour.enabled = true;
    }

    private void btn_Client_Click()
    {
        pnl_Menu.SetActive(false);
        pnl_Server.SetActive(false);
        pnl_Client.SetActive(true);
        lcbhaviour.enabled = true;
        lcbhaviour.lidclient.OnConnected += OnConnected;
        lcbhaviour.lidclient.OnDisconnected += OnDisconnected;
    }

    private void btn_StartStopServer_Click()
    {
        if (!lsbhaviour.Isrunning)
        {
            lsbhaviour.StartServer();
            btn_StartStopServer.GetComponentInChildren<Text>().text = "Stop server";
        }
        else
        {
            lsbhaviour.StopServer();
            btn_StartStopServer.GetComponentInChildren<Text>().text = "Start server";
        }
    }

    private void btn_ConnectDisconnect_Click()
    {
        if (!lcbhaviour.Isconnected)
        {
            lcbhaviour.Connect(txt_IpaddressText.GetComponent<Text>().text);
        }
        else
        {
            lcbhaviour.Disconnect();
        }
    }

    private void OnConnected()
    {
        txt_IpaddressText.SetActive(false);
        btn_ConnectDisconnect.GetComponentInChildren<Text>().text = "Disconnect";
    }

    private void OnDisconnected()
    {
        txt_IpaddressText.SetActive(true);
        btn_ConnectDisconnect.GetComponentInChildren<Text>().text = "Connect";
    }
}