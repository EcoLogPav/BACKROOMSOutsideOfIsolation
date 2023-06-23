using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Gun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    private static RaycastHit hit;
    private static Ray ray;
    public static bool _isHaveGun = false;
    private Animation animations;

    void Start()
    {
        animations = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isHaveGun)
        {
            ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.green);
            if (Physics.Raycast(ray, out hit))
            {
                animations.Play("PistolShot");

                if (hit.collider.gameObject.name == "Cube")
                {
                    PhotonNetwork.Destroy(hit.collider.gameObject);

                }
            }
        }
    }
}
