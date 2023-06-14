using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float xRotate = 0f;
    public static float sensitivityMouse = 200f;
    public Transform Player;
    //  public Camera PlayerCamera;
    //PhotonView view;
    void Start()
    {
        //  view = GetComponent<PhotonView>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!Pause._isPaused)
        {

            mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;
            xRotate -= mouseY;
            xRotate = Mathf.Clamp(xRotate, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
            Player.Rotate(mouseX * Vector3.up);
        }
    }
}
