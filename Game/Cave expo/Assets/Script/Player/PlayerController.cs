using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float playerJump = 20f;
    public Vector3 movement;
    private float horizontalMovement; private float verticalMovement;
    public Animator animator;

    private bool isGrounded;

    private float yaw = 0f;
    public Camera playerCamera;
    public float mouseSensitivity = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    async void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //playerSpeed += 2.5f;
        }
        horizontalMovement = Input.GetAxis("Horizontal"); verticalMovement = Input.GetAxis("Vertical");
        movement = new Vector3(horizontalMovement, 0.0f, verticalMovement).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.forward * playerJump, ForceMode.Impulse);
            //rb.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
            
            animator.SetBool("isJumping", false);
            isGrounded = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isWalking", true);
        }
        moveCharacter();
        HandleCameraRotation();
        HandleAnimations();
    }
    void moveCharacter()
    {
        Vector3 worldMovement = transform.TransformDirection(movement);
        rb.MovePosition(transform.position + worldMovement * playerSpeed * Time.fixedDeltaTime);
    }
    private void HandleCameraRotation()
    {
        if (Input.GetButton("Fire2"))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            playerCamera.transform.localRotation = Quaternion.Euler(0, yaw, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    protected void HandleAnimations()
    {
        
    }
}