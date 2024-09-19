using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerCheckpoint : MonoBehaviour
{
    public Transform player;
    private int currentCheckpoint = -1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && currentCheckpoint >= 0)
        {
            LoadPlayerCheckpoint(currentCheckpoint);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Transform[] playerCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Select(go => go.transform).ToArray();
            for (int i = 0; i < playerCheckpoints.Length; i++)
            {
                if (playerCheckpoints[i] == other.transform)
                {
                    currentCheckpoint = i;
                    Debug.Log("Touched checkpoint: " + currentCheckpoint);
                    break;
                }
            }
        }
    }
    public void LoadPlayerCheckpoint(int checkpointIndex)
    {
        Transform[] playerCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoint").Select(go => go.transform).ToArray();

        if (checkpointIndex >= 0 && checkpointIndex < playerCheckpoints.Length)
        {
            player.position = playerCheckpoints[checkpointIndex].position;
            currentCheckpoint = checkpointIndex;
        }
    }
}
