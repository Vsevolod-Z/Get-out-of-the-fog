using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Account
{
    private string accountID;
    private string accountName;
    private string accountPassword;
    private string accountPrivilegess;
    
    public List<Character> characters = new List<Character>();

    public string _accountID
    {
        get { return accountID; }
    }
    public string _accountName
    {
        get { return accountName; }
    }
    public string _accountPassword
    {
        get { return accountPassword; }
    }
    public string _accountPrivilegess
    {
        get { return accountPrivilegess; }
    }

    public Account(string _accountID, string _accountName, string _accountPassword, string _accountPrivilegess)
    {
        accountID = _accountID;
        accountName = _accountName;
        accountPassword = _accountPassword;
        accountPrivilegess = _accountPrivilegess;
    }
}
