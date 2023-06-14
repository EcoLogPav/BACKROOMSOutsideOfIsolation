using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] ListItem item;
    [SerializeField] Transform content;
    public TMP_Text Log;
    public TMP_InputField InputField;
    public TMP_InputField NameInputField;
    private string roomID;
    List<RoomInfo> allRoomsInfo = new List<RoomInfo>();
    public int LogStep = 0;
    public void CreateRoom()
    {
        roomID = InputField.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomID, roomOptions);
    }

    public void Exit()
    {
        PhotonNetwork.Disconnect();
       
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Вы отключены");
        Application.Quit();
    }
 
    public void JoinRoom()
    {
        roomID = InputField.text;
        PhotonNetwork.JoinRoom(roomID);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("GameRoom");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        foreach (RoomInfo info in roomList)
        {

            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if (allRoomsInfo[i].masterClientId == info.masterClientId)
                {
                    return;
                }
            }
            ListItem listItem = Instantiate(item, content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
                allRoomsInfo.Add(info);
            }


        }

    }
}
