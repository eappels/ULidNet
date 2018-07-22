using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject pnl_Menu, pnl_Server, pnl_Client, btn_Back;
    private static UIManager instance;
    public static UIManager GetInstance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
        ShowOrHideMenuUI(true);
        ShowOrHideServerUI(false);
        ShowOrHideClientUI(false);
        ShowOrHideBtnBack(false);
    }

    private void Start()
    {
        Application.runInBackground = true;
    }

    internal void ShowOrHideMenuUI(bool show)
    {
        pnl_Menu.SetActive(show);
    }

    internal void ShowOrHideServerUI(bool show)
    {
        pnl_Server.SetActive(show);
    }

    internal void ShowOrHideClientUI(bool show)
    {
        pnl_Client.SetActive(show);
    }

    internal void ShowOrHideBtnBack(bool show)
    {
        btn_Back.SetActive(show);
    }
}