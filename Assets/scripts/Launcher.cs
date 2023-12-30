using UnityEngine;
using Photon.Pun;


public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Transform spawnPoint;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
    
    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }
}
