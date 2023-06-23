using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviourPunCallbacks
{
    public static bool _isPaused;
    [SerializeField] private GameObject PauseUI;
    void Start()
    {
        _isPaused = false;
        PauseCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!Inventory._isInventoryOn)
        {
            _isPaused = !_isPaused;
            PauseCheck();
        }
    }
    private void PauseCheck()
    {
        if(_isPaused)
        {
            PauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            PauseUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void Resume()
    {
        _isPaused = !_isPaused;
        PauseCheck();

    }

    public void Exit()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LoadScene");
    }
   
}
