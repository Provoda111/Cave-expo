using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCollisions : MonoBehaviour
{
    private GameObject player1;
    private Transform player2;
    private PlayerController player3;
    private PlayerController player4;
    // Start is called before the first frame update
    void Start()
    {
        player2 = GameObject.FindGameObjectWithTag("Player").transform;
        player1 = GameObject.FindGameObjectWithTag("Player");
        player3 = player1.GetComponent<PlayerController>();
        player4 = player2.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got hit by " + other.gameObject.name);
    }
}
