using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class MainChracter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Camera mainCamera;
    [Header("Speed")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float sprintSpeed = 0.2f;
    [SerializeField]
    private float walkSpeed = 0.1f;

    [Header("Camera indentation")]
    [SerializeField]
    private float yIndentation = 0;
    [SerializeField]
    private float zIndentation = 0;
    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    public AudioClip[] walkStepSounds;
    [SerializeField]
    public AudioClip[] runStepSounds;

    [Header("Grenade")]
    [SerializeField]
    private GameObject[] pool_Grenades;
    [SerializeField]
    private float throwForce = 10;

    private GameObject grenade;

    private bool isIdle = true;
    private bool isSprint = false;
    private bool isAiming = false;

    private Animator animator;

    private Vector3 moveVector;
    private Vector3 tempCamera;
    private Vector3 mousePosition;
    private Vector3 mousePositionWorld;
    private Vector3 lookPos;
    private Vector3 grenadePosition;

    private int currentPoolElementID = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        animator  = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }
    void PlayStepSound(string moveType)
    {
        switch(moveType)
        {
            case "Walk":
                audioSource.PlayOneShot(walkStepSounds[Random.Range(0, walkStepSounds.Length)]);
                break;
            case "Run":
                audioSource.PlayOneShot(runStepSounds[Random.Range(0, runStepSounds.Length)]);
                break;
        }
       

    }

    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowGrenade();
        }
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isSprint", isSprint);
        animator.SetBool("isAiming", isAiming);
    }

    private void LateUpdate()
    {
        isSprint = Input.GetKey(KeyCode.LeftShift);
        CameraMove();
        LookAtMouse();
        Aiming();
        if (Input.GetKeyDown(KeyCode.E))
        SelectObject();
    }
    private void LookAtMouse()
    {
        GetMousePosition();
        lookPos = mousePositionWorld - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);

    }
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.transform.position.y;
        mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePosition);

    }

    private void Move()
    {
        moveSpeed = isSprint ? sprintSpeed : walkSpeed;
        isIdle= (moveVector.x == 0 && moveVector.z == 0) ? true : false;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.y -= 9.8f;
        moveVector.z = Input.GetAxis("Vertical");
        moveVector = moveVector.normalized;
        controller.Move(moveVector * moveSpeed);
    }

    private void CameraMove()
    {
        tempCamera.y = transform.position.y+yIndentation;
        tempCamera.x = transform.position.x;
        tempCamera.z = transform.position.z + zIndentation;
        mainCamera.transform.position = tempCamera;

    }
    private void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mainCamera.transform.position,mousePositionWorld, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.transform.GetComponent<InteractebleItem>())
            {
                hit.transform.GetComponent<InteractebleItem>().Interact(gameObject);
            }
           
            else
            {
                Debug.Log("Невозможно взаимодействовать");
            }
        }

    }

    private void Aiming()
    {
        isAiming = Input.GetKey(KeyCode.Mouse1) ? true : false;
    }

    private void ThrowGrenade()
    {
        
       GameObject grenade = PlaceGranade();
        Rigidbody grenadeRigidbody = grenade.GetComponent<Rigidbody>();
        grenadeRigidbody.isKinematic = false;
        grenadeRigidbody.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        StartCoroutine(grenade.GetComponent<Explosion>().ExplodeWithDelay());
    }
    private GameObject PlaceGranade()
    {
        grenadePosition = transform.position;
        grenadePosition.y = transform.position.y + 1.4f;
        grenadePosition.z = transform.position.z + 0.36f;
        grenade = pool_Grenades[currentPoolElementID];
        grenade.transform.position = grenadePosition;
        grenade.transform.rotation = Quaternion.Euler(0, 0, 0);
        grenade.transform.GetComponent<MeshRenderer>().enabled = true;
        grenade.transform.GetComponent<Explosion>()._explosionDone = false;
        currentPoolElementID++;
        if (currentPoolElementID > pool_Grenades.Length - 1)
            currentPoolElementID = 0;
        return grenade;
    }

}
