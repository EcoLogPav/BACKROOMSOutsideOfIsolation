using UnityEngine;
using Photon.Pun;

public class PickUp : MonoBehaviour
{
    private static RaycastHit hit;
    private static Ray ray;
    [SerializeField] private int RayDistance = 3;
    [SerializeField] private Transform Arm;
    [SerializeField] private GameObject[]ArmItems=new GameObject[2];
    [SerializeField] private GameObject[]Items = new GameObject[2];
    [SerializeField] private GameObject PickUpObj;
    [SerializeField] float ThrowPower=10f;
    private bool _isPick=false;
    public bool _isGravity = false;
    private PhotonView view;
    public int ItemID;
    private void Start()
    {
      view=GetComponent<PhotonView>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&_isPick==false)
        {

            ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.green);
            if (Physics.Raycast(ray, out hit, RayDistance))
            {
                if (hit.collider.gameObject.tag == "PickUpObject")
                {
                   
                    ItemID=SetID(hit);
                    _isPick = true;
                    view.RPC("PickUpObject", RpcTarget.All, ItemID);
                    view.RPC("DestroyObj", RpcTarget.All,hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
                }
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.F) && _isPick)
        {

            PhotonNetwork.Instantiate(Items[ItemID].name, transform.position, Quaternion.identity).transform.position = Arm.position;
            _isPick = false;
            view.RPC("PutDownObject", RpcTarget.All, ItemID);
        }
        if(Input.GetMouseButtonDown(0)&&_isPick)
        {
         
           PickUpObj= PhotonNetwork.Instantiate(Items[ItemID].name, transform.position, Quaternion.identity);
            PickUpObj.transform.position = Arm.position;
            PickUpObj.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowPower);
            _isPick = false;
            view.RPC("PutDownObject", RpcTarget.All, ItemID);
        }
        if(Input.GetMouseButtonDown(1) && _isPick)
        {
            _isPick=false;
        }
      
       
    }

    public int SetID(RaycastHit hit)
    {
        int id=0;
        switch (hit.collider.name)
        {
            case "MendalWather":
                id = 0;
                break;
              
            case "2":
                id = 1;
                break;
            case "3":
                id = 2;
                break;
        }
        return id;
    }
    public void UseObject(int id)
    {

    }
    [PunRPC]
    public void PickUpObject(int ItemID)
    {
        Debug.Log(ItemID);
        gameObject.GetComponent<PickUp>().ArmItems[ItemID].SetActive(true);
       
    }

    [PunRPC]
    public void PutDownObject(int ItemID)
    {
        Debug.Log("Detroy");
        gameObject.GetComponent<PickUp>().ArmItems[ItemID].SetActive(false);
    }
    [PunRPC]
    public void DestroyObj(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(false);
    }

}
