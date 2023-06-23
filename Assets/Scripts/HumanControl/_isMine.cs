using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _isMine : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    [SerializeField] private Pause pause;
    [SerializeField] private PlayerCamera cameraMove;
  //  [SerializeField] private PickUp PickUp;
    [SerializeField] private PlayerMove Move;
    [SerializeField] private GameObject PlayerBody;

    private PhotonView view;
    void Start()
    {
        view = GetComponent<PhotonView>();
        if(!view.IsMine)
        {
            Camera.SetActive(false);
            pause.enabled = false;
            cameraMove.enabled = false;
       //     PickUp.enabled = false;
            Move.enabled = false;
            
        }
        else
        {
            PlayerBody.SetActive(false);
        }
    }

}
