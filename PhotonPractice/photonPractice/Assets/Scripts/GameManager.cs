using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject connectPanel;
    public GameObject lobbyPanel;
    public GameObject gamePanel;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        ShowPanel("ConnectPanel");
    }

    public void ShowPanel(string panelName)
    {
        connectPanel.SetActive(false);
        lobbyPanel.SetActive(false);
        gamePanel.SetActive(false);

        if (panelName == connectPanel.name) connectPanel.SetActive(true);
        else if (panelName == lobbyPanel.name) lobbyPanel.SetActive(true);
        else if (panelName == gamePanel.name) gamePanel.SetActive(true);
    }
}
