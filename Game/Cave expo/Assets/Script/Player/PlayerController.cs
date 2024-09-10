using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Animator animator;
    public Transform playerCamera;
    private PlayerCheckpoint playerCheckpoint;
    public Transform sword;
    public EnemyController enemy;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerRotation = 720f;
    [SerializeField] private float playerJump = 20f;
    [SerializeField] private int playerAttack = 5;
    [SerializeField] private float playerHP = 100f;
    [SerializeField] private bool shieldProtect = false;

    public Vector3 movement;
    public Vector3 moveDirection;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCheckpoint = GetComponent<PlayerCheckpoint>();
        
    }
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        HandleAnimations();

        if (playerHP >= 0)
        {

        }
    }
    void moveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();

        moveDirection = (forward * moveZ + right * moveX).normalized;

        Vector3 worldMovement = moveDirection * playerSpeed * Time.deltaTime;
        rb.AddForce(transform.position + worldMovement * playerSpeed * Time.fixedDeltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotation * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        Debug.Log("OnCollisionEnter void works");
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log($"isGrounded {isGrounded}");
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log($"isGrounded {isGrounded}");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("RightHand@Attack01"))
            {
                Attack();
            }
            
        }
    }
    protected void HandleAnimations()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 200, 0), ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            animator.SetBool("isWalking", true);
            moveCharacter();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetTrigger("Attacks");
            StartCoroutine(DisableSwordColliderAfterAttack());
        }
        if (Input.GetButtonUp("Fire2"))
        {
            shieldProtect = true;
            animator.SetTrigger("Block");
        }
        else
        {
            shieldProtect = false;
        }
    }
    protected void Attack()
    {
        
    }
    private IEnumerator DisableSwordColliderAfterAttack()
    {
        yield return new WaitForSeconds(0.5f); 
    }
}