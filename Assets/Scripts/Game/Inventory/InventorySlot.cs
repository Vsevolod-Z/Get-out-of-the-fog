using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private int amount;
    [SerializeField]
    private Image slotImage;
    [SerializeField]
    private Sprite defaultImage;
    [SerializeField]
    private TMP_Text itemText;

    public bool isEmpty = true;
    public bool isFull = false;
    public int _itemID
    {
        get 
        { 
            if(item == null)
            {
                return 0;
            }
            return item._itemID; 
        }
    }
    public int _amount
    {
        get { return amount; }
    }
    void Awake()
    {
        slotImage = transform.GetChild(0).GetComponent<Image>();
        itemText = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
    }
    public void FillSlot(Item _item , int _amount )
    {
        isEmpty = false;
        item = _item;
        if (amount + _amount < 160)
        {
            amount += _amount;
        }
        else
        {
            amount += 160 - amount;
            GameObject.Find("Player").GetComponent<Inventory>().AddItem(_item, _amount - (160 - amount));
        }
        slotImage.sprite = _item._itemImage;
        slotImage.color = new Color(255, 255, 255, 255);
        itemText.text = amount.ToString();
    }

    public void AddExistingItem(int _amount)
    {
        if (amount + _amount <= 160)
        {
            amount += _amount;
        }
        else
        {
            amount += 160 - amount;
            isFull = true;
            GameObject.Find("Player").GetComponent<Inventory>().AddItem(item, _amount - (160 - amount));
        }
        itemText.text = amount.ToString();
    }
    public bool RemoveItem(int _amount)
    {
        if (amount - _amount >= 0)
        {
            amount -= _amount;
            itemText.text = amount.ToString();
            if(amount == 0)
            {
                isEmpty = true;
                item = null;
                slotImage.sprite = defaultImage;
                itemText.text = string.Empty;
            }
            return true;
        }
       
        else
        {
            return false;
        }
        
    }
   
}
