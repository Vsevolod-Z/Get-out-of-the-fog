using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterInventoryItem : MonoBehaviour
{
    [Header("CharacterItemInfo")]
    [SerializeField]
    private string characterID;
    [SerializeField]
    private string itemID;
    [SerializeField]
    private string itemCount; 
    [Header("CharacterItemTMP")]
    [SerializeField]
    private TMP_Text characterIdText;
    [SerializeField]
    private TMP_Text itemIdText;
    [SerializeField]
    private TMP_Text itemCountText;

    public string _characterID
    {
        get { return characterID; }
        set { characterID = value; }
    }

    public string _itemID
    {
        get { return itemID; }
        set { itemID = value; }
    }
    public string _itemCount
    {
        get { return itemCount; }
        set { itemCount = value; }
    }

    public CharacterInventoryItem(string _characterID, string _itemID, string _itemCount )
    {
        characterID = _characterID.ToString();
        itemID = _itemID;
        itemCount = _itemCount;
    }
    public void FillInfo(string _characterID, string _itemID, string _itemCount)
    {
        characterID = _characterID;
        itemID = _itemID;
        itemCount = _itemCount;

        characterIdText.text = "character_id:  " + characterID;
        itemIdText.text = "item_id: " +  itemID;
        itemCountText.text = "item_count: " + itemCount;
    }
}
