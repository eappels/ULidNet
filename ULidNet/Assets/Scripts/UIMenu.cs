using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    public GameObject btn_Server, btn_Client;
    private UIManager uimanager;

    private void Start()
    {
        uimanager = UIManager.GetInstance;
        btn_Server.GetComponent<Button>().onClick.AddListener(btn_Server_Click);
        btn_Client.GetComponent<Button>().onClick.AddListener(btn_Client_Click);
    }

    private void btn_Server_Click()
    {
        uimanager.ShowOrHideMenuUI(false);
        uimanager.ShowOrHideServerUI(true);
    }

    private void btn_Client_Click()
    {
        uimanager.ShowOrHideMenuUI(false);
        uimanager.ShowOrHideClientUI(true);
    }
}