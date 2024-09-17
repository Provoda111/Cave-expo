using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Animator animator;
    public Transform playerCamera;
    private PlayerCheckpoint playerCheckpoint;
    public Transform sword;
    private GameObject enemy;
    private EnemyController enemyController;
    
    private AudioSource audioSource;
    public AudioClip swordMiss;
    public AudioClip swordTouch;
    public AudioClip shieldTouched;
    public AudioClip PlayerGetDamage;
    public AudioClip PlayerGetArmorDamage;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerRotation = 720f;
    [SerializeField] private float playerJump = 20f;
    [SerializeField] public float playerAttack = 5f;
    [SerializeField] private float playerHP = 100f;
    [SerializeField] private bool shieldProtect = false;

    public Vector3 movement;
    public Vector3 moveDirection;

    private bool isGrounded;
    private float enemyAttack;

    public GameObject health1Level;
    public GameObject health2Level;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerCheckpoint = GetComponent<PlayerCheckpoint>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyAttack = enemy.GetComponent<EnemyController>().enemyAttack;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (playerHP <= 40)
        {
            health1Level.SetActive(true);
            health2Level.SetActive(true);
        }

        HandleAnimations();
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
        rb.AddForce(transform.position + worldMovement * playerSpeed * Time.deltaTime);
        //rb.MovePosition(transform.position + worldMovement);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotation * Time.deltaTime);
        }
       
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed += 2.5f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("isGrounded false");
            isGrounded = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemySword")
        {
            if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))
            {
                enemy.GetComponent<EnemyController>().Attack();
            }
        }
    }
    protected void HandleAnimations()
    {
        if (Input.GetButtonUp("Jump") && isGrounded)
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
            animator.SetTrigger("Block");
            shieldProtect = true;
            StartCoroutine(ShieldCooldown());
        }
        else
        {
            shieldProtect = false;
        }
        if (playerHP <= 0)
        {
            animator.SetTrigger("Death");
        }
    }
    public void Attack()
    {
        enemy.GetComponent<EnemyController>().GetHit(playerAttack);
        Debug.Log("Got damage");
    }
    public void GetHit(float amount)
    {
        if (shieldProtect && animator.GetCurrentAnimatorStateInfo(0).IsName("Shield@ShieldAttack01"))
        {
            audioSource.PlayOneShot(shieldTouched);
        }
        else
        {
            animator.SetTrigger("GetHit");
            audioSource.PlayOneShot(PlayerGetDamage);
            audioSource.PlayOneShot(PlayerGetArmorDamage);
            playerHP -= amount;
        }
    }
    private void Stunned()
    {
        animator.SetTrigger("Stunned");
    }
    private IEnumerator DisableSwordColliderAfterAttack()
    {
        yield return new WaitForSeconds(1.2f); 
    }
    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(2.4f);
    }
}