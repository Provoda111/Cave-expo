using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float playerSpeed = 50.5f;
    [SerializeField] private float playerJump = 5f;
    public Vector3 movement;
    public Transform mainCamera;
    [SerializeField] private float cameraTurnSpeed = 20.0f;
    private float horizontalMovement; private float verticalMovement;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal"); verticalMovement = Input.GetAxis("Vertical");
        movement = new Vector3(horizontalMovement, 0.0f, verticalMovement).normalized;
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // JUMP
            rb.AddForce(Vector3.up * playerJump, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        moveCharacter(movement);
        if (movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity);
        }
    }
    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * playerSpeed * Time.deltaTime;
    }
}
