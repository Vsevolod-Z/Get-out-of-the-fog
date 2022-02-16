using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class UserInfoItem : MonoBehaviour
{
    [Header("AccountInfo")]
    [SerializeField]
    public Account account;
    
    [Header("TMP_Text")]
    [SerializeField]
    private TMP_Text accountIdText;
    [SerializeField]
    private TMP_Text accountNameText;
    [SerializeField]
    private TMP_Text accountPasswordText;
    [SerializeField]
    private TMP_Text accountPrivilegessText;
    [Header("Characters")]
    [SerializeField]
    private List<Character> accountCharacters = new List<Character>();
    [SerializeField]
    private List<Character> allCharacters = new List<Character>();
    [SerializeField]
    private List<Character> characters = new List<Character>();
    [SerializeField]
    private List<CharacterInventoryItem> characterInventoryItems = new List<CharacterInventoryItem>();
    private List<CharacterInventoryItem> accountcharactersInventoryItems = new List<CharacterInventoryItem>();
    [SerializeField]
    private GameObject scrollViewContent;
    [SerializeField]
    private GameObject characterInventoryItemPrefab;

    public Account _account
    {
        get
        {
            return account;
        }
        set
        {
            account = value;
        }
    }
    
    public void FillInfo(List<Character> _characters, List<CharacterInventoryItem> _characterInventoryItems) 
    {
        _characters.Sort();
        allCharacters = _characters;
        for(int i=0; i < allCharacters.Count;i ++)
        {
            if (allCharacters[i].accountID == Convert.ToInt32(account._accountID))
            {
                accountCharacters.Add(allCharacters[i]);
            }
        }
        accountCharacters.Sort();
        allCharacters.Sort();
        characterInventoryItems = _characterInventoryItems;
        FillAccountInfo();
        FillCharactersInfo();
    }
    private void FillAccountInfo()
    {
        accountIdText.text = "account_id: "  + account._accountID;
        accountNameText.text = "account_name: " + account._accountName;
        accountPasswordText.text = "account_password: " + account._accountPassword;
        accountPrivilegessText.text = "account_privilegess: " +account._accountPrivilegess;
    }
    private void FillCharactersInfo()
    {
        for (int i = 0; i < accountCharacters.Count; i++)
        {
            characters[i].FillInfo(accountCharacters[i]);
        }
        for(int i = 0; i < characterInventoryItems.Count; i++)
        {
            for(int j =0; j < accountCharacters.Count; j++)
            {
                if (Convert.ToInt32(characterInventoryItems[i]._characterID) == Convert.ToInt32(characters[j]._characterID))
                {
                    if (Convert.ToInt32(characterInventoryItems[i]._characterID)  == Convert.ToInt32(characters[j]._characterID))
                    {
                        characters[Convert.ToInt32(characterInventoryItems[i]._characterID) %3]._characterInventoryItems.Add(characterInventoryItems[i]);
                        break;
                    }
                }
            }
           
            
          /*  if (Convert.ToInt32(characterInventoryItems[i]._characterID)-1 == Convert.ToInt32(allCharacters.ElementAt(Convert.ToInt32(characterInventoryItems[i]._characterID) - 1)._characterID) && allCharacters.ElementAt(Convert.ToInt32(characterInventoryItems[i]._characterID) - 1).accountID == Convert.ToInt32(account._accountID) )
            {
                if (Convert.ToInt32(allCharacters.ElementAt(Convert.ToInt32(characterInventoryItems[i]._characterID) -1)._characterID)-1 != 0 )
                {
                    characters[Convert.ToInt32(characterInventoryItems[i]._characterID) - 1]._characterInventoryItems.Add(characterInventoryItems[i]);
                }
                else 
                {
                    characters[Convert.ToInt32(characterInventoryItems[i]._characterID)]._characterInventoryItems.Add(characterInventoryItems[i]);
                }
            }*/
        }
        characters.Sort();
        for (int i = 0; i < characters.Count; i++)
        {
            for (int j = 0; j < characters[i]._characterInventoryItems.Count; j++)
            {
                
                    GameObject characterInventoryItem = Instantiate(characterInventoryItemPrefab, scrollViewContent.transform);
                    characterInventoryItem.GetComponent<CharacterInventoryItem>().FillInfo(characters[i]._characterInventoryItems[j]._characterID, characters[i]._characterInventoryItems[j]._itemID, characters[i]._characterInventoryItems[j]._itemCount);
                
            }
           
        }
    }
   

}
