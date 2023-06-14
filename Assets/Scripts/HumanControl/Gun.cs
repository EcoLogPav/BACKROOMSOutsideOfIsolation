using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public static RaycastHit hit;
    public static Ray ray;
    public static bool _isRayReleased;
    [SerializeField] private int RayDistance = 3;
    [SerializeField]private GameObject PickUpObject;
    [SerializeField] private Transform Arm;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.green);
            if (Physics.Raycast(ray, out hit, RayDistance))
            {
                if (hit.collider.gameObject.tag == "PickUpObject")
                {
                    hit.collider.gameObject.transform.position=Arm.transform.position;
                }
            }
        }
    }
   
}
