using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    public Rigidbody[] AllRigiBodys;

    private void Awake()
    {
        foreach (Rigidbody rigidbody in AllRigiBodys)
        {
            rigidbody.isKinematic = true;
        }
    }
    public  void RagDollOn()
    {
        foreach(Rigidbody rigidbody in AllRigiBodys)
        {
            rigidbody.isKinematic = false;
        }
    }
}
