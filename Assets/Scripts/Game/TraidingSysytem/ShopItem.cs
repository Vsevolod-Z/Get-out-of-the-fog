using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private float buyItemPrice;
    [SerializeField]
    private float sellItemPrice;
    [SerializeField]
    private TMP_Text buyItemTextPrice;
    [SerializeField]
    private TMP_Text sellItemTextPrice;
    [SerializeField]
    private Image shopItemImage;
    [SerializeField]
    private TradingSystem tradingSystem;
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private Button sellButton;
    
    public int _itemID
    {
        get { return item._itemID; }
    }
    public float _buyItemPrice
    {
        get { return buyItemPrice; }
    }
    public float _sellItemPrice
    {
        get { return sellItemPrice; }
    }
    public Item _item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
        }
    }
    private void Start()
    {

        sellItemPrice = item._itemPrice * 0.93f;
        buyItemPrice = item._itemPrice * 1.17f;
        buyItemTextPrice.text = "Buy Price: " + buyItemPrice.ToString();
        sellItemTextPrice.text = "Sell Price: " + sellItemPrice.ToString();
        shopItemImage.sprite = item._itemImage;
        tradingSystem = FindObjectOfType<TradingSystem>();


    }

    public void BuyClick()
    {
        tradingSystem.BuyItem(this);
    }
    public void SellClick()
    {
        tradingSystem.SellItem(this);
    }
}
