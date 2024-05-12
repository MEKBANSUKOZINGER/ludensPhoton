using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); //마스터 서버 접속
    }

    public override void OnConnectedToMaster() //마스터 서버 접속시 콜백
    {
        PhotonNetwork.JoinLobby(); //로비 접속
    }

    public override void OnJoinedLobby()
    {
        
    }

}
