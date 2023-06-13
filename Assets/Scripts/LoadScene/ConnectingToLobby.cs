using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectingToLobby : MonoBehaviourPunCallbacks
{

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("вы подключенны к мастер серверу");
    }
}
