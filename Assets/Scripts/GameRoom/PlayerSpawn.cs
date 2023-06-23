using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject MasterStartUI;
    [SerializeField] private GameObject OtherStartUI;
    [SerializeField] private GameObject StartUI;
    private PhotonView view;
  
   
    void Start()
    {
        PhotonNetwork.JoinLobby();
        view = GetComponent<PhotonView>();
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            MasterStartUI.SetActive(true);
        }
        else
        {
            OtherStartUI.SetActive(true);
        }
        
    }
    private void Update()
    {
      
    }
    public void StartGame()
    {
        view.RPC("StartGameTP", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void StartGameTP()
    {
        StartUI.SetActive(false);
        PhotonNetwork.Instantiate("Stranger", new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)), Quaternion.identity);
    }

}
