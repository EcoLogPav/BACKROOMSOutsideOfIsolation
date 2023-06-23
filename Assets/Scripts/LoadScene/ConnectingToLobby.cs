using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectingToLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private string region;
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(region);
       
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
       
        SceneManager.LoadScene("MainMenu");
        Debug.Log("вы подключенны к мастер серверу");
    }
}
