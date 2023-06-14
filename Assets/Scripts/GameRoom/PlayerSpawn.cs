using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private Camera SearchPlayer;
    private PhotonView view;
    private bool _IsGameStarted=false;
  
   
    void Start()
    {
        view=GetComponent<PhotonView>();

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)&&!_IsGameStarted)
        {
            _IsGameStarted = true;
            StartGame();
        }
    }
    void StartGame()
    {
      
        view.RPC("StartGameTP", RpcTarget.All);
    }
    [PunRPC]
    public void StartGameTP()
    {
        SearchPlayer.gameObject.SetActive(false);
         PhotonNetwork.Instantiate("BIGAgent", new Vector3(Random.Range(13, 30), 1, Random.Range(6, 13)), Quaternion.identity);
    }

}
