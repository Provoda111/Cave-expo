using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float mouseSensitivity = 1.5f;

    private Vector3 offset;
    private float yaw = 0f;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButton("Fire2"))
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        }
        //transform.rotation = Quaternion.Euler(0, yaw, 0);
    }
}
