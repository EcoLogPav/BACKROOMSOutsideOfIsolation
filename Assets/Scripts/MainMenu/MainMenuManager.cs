
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] ListItem item;
    [SerializeField] Transform content;
    public TMP_InputField InputField;
    private string roomID;
    List<RoomInfo> allRoomsInfo = new List<RoomInfo>();
    GameObject[] RoomsPages = new GameObject[15];


    public void CreateRoom()
    {
        roomID = InputField.text;
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomID, roomOptions);
        
    }
    public override void OnCreatedRoom()
    {
    }
    public void Exit()
    {
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
        foreach (GameObject pages in RoomsPages)
        {
            
            Destroy(pages);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
                ListItem listItem = Instantiate(item, content);
                listItem.SetInfo(roomList[i]);
                RoomsPages[i] = listItem.gameObject;
                
        }
        Debug.Log(roomList.Count);

    }
}