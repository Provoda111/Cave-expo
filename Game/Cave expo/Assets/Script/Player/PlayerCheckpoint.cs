using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerCheckpoint : MonoBehaviour
{
    public Transform player;
    public Transform[] playerCheckpoint;
    private int currentCheckpoint = -1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            for (int i = 0; i < playerCheckpoint.Length; i++)
            {
                if (playerCheckpoint[i] == other.transform)
                {
                    currentCheckpoint = i;
                    break;
                }
            }
        }
    }

    void savePlayerCheckpoint()
    {
        if (player.CompareTag("Checkpoint"))
        {
            currentCheckpoint += 1;
            Debug.Log("Touched checkpoint");
        }
    }
    void loadPlayerCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex >= 0 && checkpointIndex < playerCheckpoint.Length)
        {
            player.position = playerCheckpoint[currentCheckpoint].position;
            currentCheckpoint = checkpointIndex;

        }
    }
}
