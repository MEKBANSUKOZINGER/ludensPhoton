using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject connectPanel;
    public GameObject lobbyPanel;
    public GameObject gamePanel;

    public GameState gameState;

    public Transform[] spawnPoints;

    public UnityStandardAssets.Cameras.AutoCam autoCam;

    public int GetIndex
    {
        get
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
                {
                    return i;
                }
            }
            return -1;
        }
    }
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

    public void GameStart() 
    {
        print("Hello World!");
        ShowPanel("GamePanel");
        gameState = GameState.RacingStart;
        SpawnCar();
    }

    private void SpawnCar()
    {
        PhotonNetwork.Instantiate("Car", spawnPoints[GetIndex].position, spawnPoints[GetIndex].rotation);
    }

    public void SetAutoCamTarget(Transform target)
    {
        autoCam.SetTarget(target);
    }
}
