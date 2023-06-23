using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private Image image;
    [SerializeField] private PickUp pickUp;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void SelectPageButton()
    {
        pickUp.PutDownAllState();
        switch (image.sprite.name)//PlusObject
        {
            case "MendalWatherSprite":
                pickUp.ItemID = 0;
                pickUp.PickUpAllState();
                break;
            case "MedKitSprite":
                pickUp.ItemID = 1;
                pickUp.PickUpAllState();
                break;
     
        
        }
    }

}
