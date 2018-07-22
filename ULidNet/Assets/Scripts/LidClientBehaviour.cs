using System;
using UnityEngine;
using UnityEngine.UI;

public class LidClientBehaviour : MonoBehaviour
{

    public GameObject pnl_Connect, pnl_Disconnect, btn_Connect, btn_Disconnect, btn_Back, txt_Ipaddress;
    public LidClient lidclient;
    private UIManager uimanager;

    private void OnEnable()
    {
        pnl_Disconnect.SetActive(false);
        uimanager = UIManager.GetInstance;
        btn_Back.GetComponent<Button>().onClick.AddListener(btn_Back_Click);
        uimanager.ShowOrHideBtnBack(true);
        lidclient = new LidClient();
        lidclient.DebugMessage += OnDebugMessage;
        lidclient.UnknownMessage += OnUnknownMessage;
        lidclient.Connected += OnConnected;
        lidclient.Disconnected += OnDisconnected;
        btn_Connect.GetComponent<Button>().onClick.AddListener(btn_Connect_Click);
        btn_Disconnect.GetComponent<Button>().onClick.AddListener(btn_Disconnect_Click);
    }

    private void btn_Connect_Click()
    {
        lidclient.Connect(txt_Ipaddress.GetComponent<InputField>().text);
    }

    private void btn_Disconnect_Click()
    {
        lidclient.Disconnect();
    }

    private void OnDisable()
    {
        lidclient.Shutdown();
        lidclient.DebugMessage -= OnDebugMessage;
        lidclient.UnknownMessage -= OnUnknownMessage;
        lidclient = null;
    }

    private void btn_Back_Click()
    {
        uimanager.ShowOrHideMenuUI(true);
        uimanager.ShowOrHideServerUI(false);
        uimanager.ShowOrHideClientUI(false);
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
        if (lidclient != null) lidclient.MessagePump();
    }

    private void OnConnected()
    {
        pnl_Connect.SetActive(false);
        pnl_Disconnect.SetActive(true);
        uimanager.ShowOrHideBtnBack(false);
    }

    private void OnDisconnected()
    {
        pnl_Connect.SetActive(true);
        pnl_Disconnect.SetActive(false);
        uimanager.ShowOrHideBtnBack(true);
    }
}