using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float playerSpeed = 10.5f;
    public float playerJump = 5f;
    public Vector3 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // JUMP
        }
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }


    void moveCharacter(Vector3 direction)
    {
        rb.velocity = direction * playerSpeed * Time.deltaTime;
    }
}
