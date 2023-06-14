using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]private GameObject Moudle;
    [SerializeField] private Transform[]modules=new Transform[3];
    private Transform MovingCamera;
    private float timer = 0;
    void Start()
    {
        MovingCamera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        MovingCamera.localPosition += MovingCamera.forward * Time.deltaTime * 3f;
        timer += 1 * Time.deltaTime;
        if (timer >= 20)
        {
            Destroy(modules[0].gameObject);
            RefreshMassive();
            modules[2]=Instantiate(Moudle, new Vector3(modules[1].position.x, modules[1].position.y, modules[1].position.z + 167.6557f), Quaternion.identity).transform;
            timer = 0;
        }
        
    }
    public void RefreshMassive()
    {
        for (int i=0; i<modules.Length-1; i++) 
        {
            modules[i] = modules[i+1];
        }
    }
}
