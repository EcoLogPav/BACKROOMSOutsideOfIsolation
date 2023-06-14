using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text RoomName;
    [SerializeField] TMP_Text PlayersCount;
    
    public void SetInfo(RoomInfo info)
    {
        RoomName.text = info.Name;
        PlayersCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }
    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(RoomName.text);
    }
}
