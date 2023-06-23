using UnityEngine;
using Photon.Pun;

public class PickUp : MonoBehaviour
{
    private static RaycastHit hit;
    private static Ray ray;
    public Inventory inventory;
    [SerializeField] private int RayDistance = 3;
    [SerializeField] private Transform Arm;
    public GameObject[]ArmItems=new GameObject[3];
    public GameObject[]PrefubItems = new GameObject[3];
    public GameObject[]ArmItemForOtherPlayer = new GameObject[3];
    public static GameObject PickUpObj;
    [SerializeField] private float ThrowPower=500f;
    [SerializeField] private Animator humanAnimator;
    private bool _isPick=false;
    public bool _isGravity = false;
    private PhotonView view;
    public  int ItemID;
   
    private void Start()
    {
      inventory = GetComponent<Inventory>();
      view=GetComponent<PhotonView>();
    }
    void Update()
    {
       // SetArmObjPos();
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.F) && _isPick == false&&Inventory.CountItems<6)
            {
                ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.green);
                if (Physics.Raycast(ray, out hit, RayDistance))
                {
                    if (hit.collider.gameObject.tag == "PickUpObject")
                    {
                        humanAnimator.SetBool("_haveItem", true);//animation
                        ItemID = SetID(hit);
                        PickUpAllState();
                        inventory.PlusToInventoryList();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.F) && _isPick && Inventory.CountItems < 6)
            {
                ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                Debug.DrawRay(transform.position, new Vector3(Screen.width / 2, Screen.height / 2, 0), Color.green);
                if (Physics.Raycast(ray, out hit, RayDistance))
                {
                    if (hit.collider.gameObject.tag == "PickUpObject")
                    {
                        humanAnimator.SetBool("_haveItem", true);//animation
                        PutDownAllState();
                        ItemID = SetID(hit);
                        PickUpAllState();
                        inventory.PlusToInventoryList();
                       
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.T) && _isPick&& !Inventory._isInventoryOn)
            {
                
                Debug.Log(ItemID);
                humanAnimator.SetBool("_haveItem", false);//animation
                humanAnimator.SetBool("_haveGun", false);
                PutDownAllState();
                PickUpObj = PhotonNetwork.Instantiate(PrefubItems[ItemID].name, Arm.position, Quaternion.identity);//clone
                PickUpObj.name = PickUpObj.name.Replace("(Clone)", "").Trim(); ;
                PickUpObj.transform.position = Arm.position;
                PickUpObj.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowPower);
                
                inventory.MinusToInventoryList();
               

            }
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
              
            case "MedKit"://+Object
                id = 1;
                break;
            case "Pistol":
                Gun._isHaveGun = true;
                humanAnimator.SetBool("_haveGun", true);
                id = 2;
                break;
        }
        return id;
    }
    public void UseObject(int id)
    {

    }
    public void PutDownAllState()
    {
            _isPick = false;
            Gun._isHaveGun = false;
            ArmItems[ItemID].SetActive(false);
            view.RPC("PutDownObject", RpcTarget.All,ArmItemForOtherPlayer[ItemID].GetComponent<PhotonView>().ViewID);
            
    }
   public void PickUpAllState()
    {
        _isPick = true;
        ArmItems[ItemID].SetActive(true);
        view.RPC("PickUpObject", RpcTarget.AllBuffered, ArmItemForOtherPlayer[ItemID].GetComponent<PhotonView>().ViewID);
        view.RPC("DestroyObj", RpcTarget.AllBuffered, hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
    }
    
        
    [PunRPC]
    public void PickUpObject(int id)
    {
        
        PhotonView.Find(id).gameObject.SetActive(true);
        if (view.IsMine)
        {
            PhotonView.Find(id).gameObject.SetActive(false);
        }
         
    }

    [PunRPC]
    public void PutDownObject(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(false);
    }
    [PunRPC]
    public void DestroyObj(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(false);
    }
}
//159666