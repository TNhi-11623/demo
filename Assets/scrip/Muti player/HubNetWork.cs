using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Realtime;
namespace MutiPlayer
{
    public class HubNetWork : MonoBehaviourPunCallbacks
    {
        [SerializeField] Button joinRoomButton;
        [SerializeField] Button leaveRoomButton;
        [SerializeField] GameObject rooomItemPrefab;
        void Start()
        {
            leaveRoomButton.gameObject.SetActive(false);
            joinRoomButton.gameObject.SetActive(true);
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Connected to Photon Network");
            }
            else
            {
                Debug.Log("Not connected to Photon Network");
                PhotonNetwork.ConnectUsingSettings();
            }
        }
        public override void OnRoomListUpdate(List<Photon.Realtime.RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            Debug.Log("Room list updated");
            Transform roomListParent = rooomItemPrefab.transform.parent;
            while (roomListParent.childCount > 1)
            {
                Destroy(roomListParent.GetChild(1).gameObject);
            }
            foreach (var room in roomList)
            {
                var go = Instantiate(rooomItemPrefab, roomListParent);
                go.name = room.Name;
                go.SetActive(true);  
            }
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master Server");
            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
            joinRoomButton.gameObject.SetActive(true);
            leaveRoomButton.gameObject.SetActive(false);
        }
        

        public void JoinRoom()
        {
            PhotonNetwork.JoinRandomOrCreateRoom();
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined a room");
            joinRoomButton.gameObject.SetActive(false);
            leaveRoomButton.gameObject.SetActive(true);
            // You can instantiate player objects or perform other actions here
        }
        public void LeaveRoom()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
                joinRoomButton.gameObject.SetActive(true);
                leaveRoomButton.gameObject.SetActive(false);
                Debug.Log("Left the room");
            }
            else
            {
                Debug.LogWarning("Not in a room to leave");
            }
        }
    }
}


