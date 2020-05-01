using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text LogText;
    [SerializeField] private Button createRoomButton;

    private void Start()
    {
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "0.01";
        PhotonNetwork.ConnectUsingSettings();
        createRoomButton.interactable = false;
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 3 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined To Room");
        PhotonNetwork.LoadLevel("GameScene");
    }

    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        Log("Connected To Master");
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
