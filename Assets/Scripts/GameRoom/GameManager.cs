using Photon.Pun;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviourPunCallbacks
{
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
