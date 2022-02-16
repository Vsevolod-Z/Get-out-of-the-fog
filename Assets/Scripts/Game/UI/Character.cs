using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Character : MonoBehaviour, IComparable
{
    [Header("CharacterData")]
    [SerializeField]
    private string characterID;
    [SerializeField]
    private string characterName;
    [SerializeField]
    private string characterMoney;
    [SerializeField]
    private string characterHealth;
    [SerializeField]
    private string characterPosition;
    [SerializeField]
    private TMP_Text characterIdText;
    [SerializeField]
    private TMP_Text characterNameText;
    [SerializeField]
    private TMP_Text characterMoneyText;
    [SerializeField]
    private TMP_Text characterHealthText;
    [SerializeField]
    private TMP_Text characterPositionText;
    [SerializeField]
    public List<CharacterInventoryItem> _characterInventoryItems = new List<CharacterInventoryItem>();

    public int accountID;

    public string _characterID
    {
        get { return characterID; }
    } 
    public string _characterName
    {
        get { return characterName; }
    } 
    public string _characterMoney
    {
        get { return characterMoney; }
    } 
    public string _characterHealth
    {
        get { return characterHealth; }
    } 
    public string _characterPosition
    {
        get { return characterPosition; }
    }

    public int CompareTo(object? obj)
    {
        if ((obj == null) || (!(obj is Character)))
            return 0;
        else
            return string.Compare(characterID, ((Character)obj).characterID);
    }

    public Character(string _characterID, string _characterName, string _characterMoney, string _characterHealth, string _characterPosition , string _accountID)
    {
        characterID = _characterID;
        characterName = _characterName;
        characterMoney = _characterMoney;
        characterHealth = _characterHealth;
        characterPosition = _characterPosition;
        accountID = Convert.ToInt32(_accountID);
    }
    public void FillInfo(Character _character)
    {
        characterID = _character._characterID;
        characterName = _character._characterName;
        characterMoney = _character._characterMoney;
        characterHealth = _character._characterHealth;
        characterPosition = _character._characterPosition;
        accountID = _character.accountID;
       // _characterInventoryItems = _character._characterInventoryItems;
        characterIdText.text = "character_id: "  + characterID;
        characterNameText.text = "character_name: " + characterName;
        characterMoneyText.text = "character_money: " + characterMoney;
        characterHealthText.text = "character_health: " + characterHealth;
        characterPositionText.text = "character_position: " + characterPosition;
    }
}
