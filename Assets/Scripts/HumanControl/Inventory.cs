using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;
    public static bool _isInventoryOn;
    private PickUp pickUp;
    [SerializeField] private Sprite MendalWatherImage;
    [SerializeField] private Sprite MedKitImage;
    [SerializeField] private Sprite Null;
    [SerializeField] private Image[] cells = new Image[6];
    public static int CountItems = 0;

    void Start()
    {
        pickUp = GetComponent<PickUp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _isInventoryOn = !_isInventoryOn;
            CheckInventoryOn();
        }

    }
    void CheckInventoryOn()
    {
        if (_isInventoryOn)
        {
            InventoryUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            InventoryUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void PlusToInventoryList()
    {
        for (int i = 0; i < cells.Length; i++)//+Object
        {
            {
                if (cells[i].sprite == null|| cells[i].sprite == Null)
                {
                    switch (pickUp.ItemID)
                    {
                        case 0:
                            cells[i].sprite = MendalWatherImage;
                            break;
                        case 1:
                            cells[i].sprite = MedKitImage;
                            break;
                    }

                    return;
                }
            }
        }
        CountItems++;
    }
     public void MinusToInventoryList()//+Object
     {
        bool _isItemDrop = false;
        int i = 0;
        while (!_isItemDrop)
        {
            if (cells[i].sprite.name == pickUp.Items[pickUp.ItemID].name+"Sprite")
            {
                cells[i].sprite=Null;
                _isItemDrop = true;
            }
            i++;
        }
    }
}
    
