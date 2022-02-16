using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField]
    private float damage;
    [SerializeField]
    private float fireRate = 0.1f;    
    [SerializeField]
    private float range = 10;[
        SerializeField]
    private float force = 100;
    [Header("Effects Settings")]
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private ParticleSystem cartridgeEjectEffect;
    [SerializeField]
    private GameObject[] pool_ShootHoles; 
    private GameObject shootHole;
    [SerializeField]
    private ParticleSystem[] shootHoleEffects;
    private ParticleSystem shootHoleParticle;

    [SerializeField]
    private LineRenderer lineRenderer;
    

    [Header("Audio Settings")]
    [SerializeField]
    private AudioSource audioSource; 
    [SerializeField]
    private AudioClip rifleShoot;

    private float nextTime = 0.0f;

    private int currentPoolElementID = 0;
   

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextTime)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            nextTime = Time.time + fireRate;
            Shoot();
        }
        Debug.DrawRay(transform.position, transform.forward*range,Color.yellow);
    }


  
    void Shoot()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (!hit.collider.isTrigger)
            {

                PlaceShootHole(hit, hit.transform.gameObject.layer);
                if (hit.rigidbody != null)
                {
                    if (hit.rigidbody.GetComponent<RagDoll>())
                    {
                        hit.rigidbody.GetComponent<RagDoll>().RagDollOn();
                    }
                    hit.rigidbody.AddForce(-hit.normal * force);
                }
            }
        }
        audioSource.PlayOneShot(rifleShoot);
        muzzleFlash.Play();
        cartridgeEjectEffect.Play();
    }
    private void PlaceShootHole( RaycastHit hit , int layerNum)
    {
        int childNum = (layerNum % 10) - 1;
        
        shootHole = pool_ShootHoles[currentPoolElementID];
        if(childNum > 0 && childNum < shootHoleEffects.Length)
            shootHoleParticle = shootHole.transform.GetChild(childNum).GetComponent<ParticleSystem>();
        else
            shootHoleParticle = shootHole.transform.GetChild(0).GetComponent<ParticleSystem>();
        shootHole.transform.position = hit.point + hit.normal * 0.01f;
        shootHole.transform.rotation = Quaternion.Euler(0, 0, 0);
        shootHole.transform.rotation = Quaternion.FromToRotation(shootHole.transform.forward, hit.normal);
        shootHoleParticle.Play();
        shootHole.transform.parent = hit.transform;
        currentPoolElementID++;
        if (currentPoolElementID > pool_ShootHoles.Length - 1)
            currentPoolElementID = 0;
    }
    
    
}
