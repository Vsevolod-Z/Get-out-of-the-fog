using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Sliders & Texts")]
    [SerializeField]
    private TMP_Text moneyText; 
    [SerializeField]
    private GameObject notificationBar;
    [Header("inventoryPanel")]
    [SerializeField]
    GameObject inventoryPanel;
    [SerializeField]
    private Inventory userInventory;
    [SerializeField]
    private TMP_Text money;
    [SerializeField]
    private TMP_Text ammo;
    [SerializeField]
    private TMP_Text grenade;
    [SerializeField]
    private TMP_Text weight;
    // Start is called before the first frame update
    [Header("AdminPanel")]
    [SerializeField]
    private GameObject adminPanel;
    [SerializeField]
    public GameObject adminPanelButton;
    [Header("ShopPanel")]
    [SerializeField]
    private GameObject shopPanel;
    private void Start()
    {
        UpdateUI();
       
    }
    void LateUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
           ChangeInventoryVision();
        }
    }
  
    public IEnumerator MakeNotification (string _string)
    {
        notificationBar.SetActive(true);
        notificationBar.GetComponent<TMP_Text>().text = _string;
        yield return new WaitForSeconds(2f);
        notificationBar.GetComponent<TMP_Text>().text = "";
        notificationBar.SetActive(false);
    }

    private void ChangeInventoryVision()
    {
        inventoryPanel.SetActive( (inventoryPanel.activeInHierarchy) ? false:true);
    } 
    public void ChangeAdminPanelVision()
    {
        adminPanel.SetActive( (adminPanel.activeInHierarchy) ? false:true);
    } 
    public void OpenShopPanelVision()
    {
        shopPanel.SetActive(true);
    }
    public void CloseShopPanelVision()
    {
        shopPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        int[] values = new int[4];
        values = userInventory.GetInventoryValues();
        ammo.text = values[0].ToString();
        grenade.text = values[1].ToString();
        money.text = values[2].ToString();
        weight.text = (values[3] * 0.1).ToString();

    }
}
