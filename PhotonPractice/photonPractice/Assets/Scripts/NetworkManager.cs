using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance { get; private set; }

    public TMP_Text quickMatchText;
    public int maxPlayers;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        
    }

    public void ConnectButtonClick(TMP_InputField nickname)
    {
        PhotonNetwork.ConnectUsingSettings(); //마스터 서버 접속

        string nick = nickname.text.Length > 0 ? nickname.text : $"Player {UnityEngine.Random.Range(0, 100)}";
        PhotonNetwork.NickName = nick;

        print($"Nickname : {PhotonNetwork.NickName}");
    }

    public void QuickMatchButtonClick()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameState.None: 
                GameManager.Instance.gameState = GameState.QuickMatching;
                quickMatchText.gameObject.SetActive(true);
                PhotonNetwork.JoinRandomOrCreateRoom(null, (byte)maxPlayers, 
                    MatchmakingMode.FillRoom, null, null, 
                    $"room {UnityEngine.Random.Range(0, 10000)}", 
                    new RoomOptions { MaxPlayers = (byte)maxPlayers });
                break;
            case GameState.QuickMatching:
                GameManager.Instance.gameState = GameState.None;
                quickMatchText.gameObject.SetActive(false);
                PhotonNetwork.LeaveRoom();
                break;
        }
    }

    public void UpdatePlayerCountOrStartGame() 
    {
        quickMatchText.text = $"{PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";

        if (PhotonNetwork.CurrentRoom.PlayerCount != maxPlayers)
        {
            return;
        }

        GameManager.Instance.gameState = GameState.QuickMatchDone;
        GameManager.Instance.GameStart();
    }

    public override void OnConnectedToMaster() //마스터 서버 접속시 콜백
    {
        PhotonNetwork.JoinLobby(); //로비 접속
    }

    public override void OnJoinedLobby()
    {
        print("done");
        GameManager.Instance.ShowPanel("LobbyPanel");
    }

    public override void OnJoinedRoom()
    {
        UpdatePlayerCountOrStartGame();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerCountOrStartGame();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerCountOrStartGame();
    }
}
