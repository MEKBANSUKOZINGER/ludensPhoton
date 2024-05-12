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
        PhotonNetwork.ConnectUsingSettings(); //������ ���� ����
    }

    public override void OnConnectedToMaster() //������ ���� ���ӽ� �ݹ�
    {
        PhotonNetwork.JoinLobby(); //�κ� ����
    }

    public override void OnJoinedLobby()
    {
        
    }

}
