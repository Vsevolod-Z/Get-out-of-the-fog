using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu]
public class Item : ScriptableObject
{
   [SerializeField]
   private int itemID;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private float itemPrice;
    [SerializeField]
    private float itemWeight;
   [SerializeField]
    private Sprite itemImage;

   public  int _itemID
    {
        get { return itemID; }
    }
    public string _itemName
    {
        get { return itemName; }
    }
    public float _itemPrice
    {
        get { return itemPrice; }
    }
    public float _itemWeight
    {
        get { return itemWeight; }
    }
    public Sprite _itemImage
    {
        get { return itemImage; }
    }
    public Item(object _itemID, object _itemName , object _itemPrice , object _itemWeight , Sprite _itemImage) 
    {
        itemID = Convert.ToInt32(  _itemID.ToString() );
        itemName = Convert.ToString(_itemName.ToString());
        itemPrice =float.Parse(_itemPrice.ToString());
        itemWeight = float.Parse(_itemWeight.ToString())*0.1f;
        itemImage = _itemImage;
    }
}
