using UnityEngine;
using Photon.Pun;
using UnityEngine.SocialPlatforms;

public class PickUp : MonoBehaviour
{
    private static RaycastHit hit;
    private static Ray ray;
    public Inventory inventory;
    [SerializeField] private int RayDistance = 3;
    [SerializeField] private Transform Arm;
    public GameObject[]ArmItems=new GameObject[3];
    public GameObject[]Items = new GameObject[3];
    public static GameObject PickUpObj;
    [SerializeField] private float ThrowPower=500f;
    [SerializeField] private Animator humanAnimator;
    [SerializeField] private GameObject ArmObject=null;
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
        SetArmObjPos();
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
                PutDownAllState();
                PickUpObj = PhotonNetwork.Instantiate(Items[ItemID].name, Arm.position, Quaternion.identity);//clone
                PickUpObj.name = PickUpObj.name.Replace("(Clone)", "").Trim(); ;
                PickUpObj.transform.position = Arm.position;
                PickUpObj.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowPower);
                Debug.Log("----------1"+"1");
                inventory.MinusToInventoryList();
                Debug.Log("----------2" + "2");

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
            case "pistol":
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
            ArmItems[ItemID].SetActive(false);
            view.RPC("PutDownObject", RpcTarget.All, ItemID);
            _isPick = false;
    }
   public void PickUpAllState()
    {
        _isPick = true;
        humanAnimator.SetBool("_haveItem", true);//animation
        ArmItems[ItemID].SetActive(true);
        ArmObject = PhotonNetwork.Instantiate(Items[ItemID].name + "ForArm", Arm.position, Quaternion.identity);
        ArmObject.SetActive(false);
        view.RPC("DestroyObj", RpcTarget.AllBuffered, hit.collider.gameObject.GetComponent<PhotonView>().ViewID);
        view.RPC("PickUpObject", RpcTarget.AllBuffered, Arm.GetComponent<PhotonView>().ViewID, ArmObject.GetComponent<PhotonView>().ViewID);
    }
    public void SetArmObjPos()//+Object
    {
       
            if (_isPick)
            {
                switch (ItemID)
                {
                    case 0:
                        ArmObject.transform.eulerAngles = new Vector3(0, 0, 0);//+Object
                        break;
                    case 1:
                        ArmObject.transform.eulerAngles = new Vector3(60, 100, -50);
                        break;
                }
            }
        
    }
    [PunRPC]
    public void PickUpObject( int ArmId,int ArmObjectId)
    {

            PhotonView.Find(ArmObjectId).transform.SetParent(PhotonView.Find(ArmId).transform);          
    }

    [PunRPC]
    public void PutDownObject(int ItemID)
    {
        if (_isPick)
        {
            PhotonNetwork.Destroy(ArmObject);
            ArmObject = null;
        }
    }
    [PunRPC]
    public void DestroyObj(int id)
    {
        PhotonView.Find(id).gameObject.SetActive(false);
    }
}
