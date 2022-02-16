using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    private UIController uIController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainChracter>())
        {
            uIController.OpenShopPanelVision();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<MainChracter>())
        {
            uIController.CloseShopPanelVision();
        }
    }
}
