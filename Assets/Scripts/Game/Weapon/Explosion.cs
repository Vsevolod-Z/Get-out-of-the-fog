using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float explosiveDelay = 5f;
    [SerializeField]
    private float explosiveRadius = 5f;
    [SerializeField]
    private float explosiveForce = 300f;
    [SerializeField]
    private Collider[] overlappedColliders;
    [SerializeField]
    public bool _explosionDone = false;
    [SerializeField]
    public Explosion nearExplosion;
    [SerializeField]
    private ParticleSystem explosiveEffect;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private Vector3 pool_GrenadesPosition;

    private Rigidbody grenadeRigibody;
    private MeshRenderer grenadeMeshRenderer;

    private void Awake()
    {
        grenadeRigibody = transform.GetComponent<Rigidbody>();
        grenadeMeshRenderer = transform.GetComponent<MeshRenderer>();
    }
    public IEnumerator ExplodeWithDelay()
    {
        yield return new WaitForSeconds(explosiveDelay);
        Explode();
    }
    public void ExplodeNear()
    {
        Invoke("Explode", 0.5f);
    }

    public void Explode()
    {
        if (_explosionDone)
            return;
        else
        {
            grenadeRigibody.isKinematic = true;
            _explosionDone = true;
            overlappedColliders = Physics.OverlapSphere(transform.position, explosiveRadius);
           
            for (int i = 0; i < overlappedColliders.Length; i++) 
            { 
                Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
                if (rigidbody)
                {
                    if (rigidbody.GetComponent<RagDoll>())
                    {
                        rigidbody.GetComponent<RagDoll>().RagDollOn();
                    }
                        rigidbody.AddExplosionForce(explosiveForce, transform.position, explosiveRadius);
                    nearExplosion = rigidbody.GetComponent<Explosion>();
                    if (nearExplosion)
                    {
                        nearExplosion.ExplodeNear();
                    }
                }
            }
           
            StartCoroutine(EndLife());
        }
    }
    
    private IEnumerator EndLife()
    {
        explosiveEffect.Play();
        audioSource.Play();
        grenadeMeshRenderer.enabled = false;
        yield return new WaitForSeconds(1f);
        transform.position = pool_GrenadesPosition;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosiveRadius);
    }
}
