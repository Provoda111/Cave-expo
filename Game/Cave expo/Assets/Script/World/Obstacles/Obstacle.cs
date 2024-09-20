using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Obstacle : MonoBehaviour
{
    public GameObject obstacleUI;
    public Transform player;
    public Transform obstacle;
    public float interactionDistance = 1.5f;
    private bool canInteract = false;

    private void Update()
    {
        float distanceX = Mathf.Abs(player.position.x - obstacle.position.x);
        float distanceZ = Mathf.Abs(player.position.z - obstacle.position.z);

        if (distanceX <= interactionDistance && distanceZ <= interactionDistance)
        {
            obstacleUI.SetActive(true);
            canInteract = true;
        }
        else
        {
            obstacleUI.SetActive(false);
            canInteract = false;
        }
        if (canInteract && Input.GetKeyUp(KeyCode.E))
        {
            ActivateHealthPotion();
        }
    }
    void ActivateHealthPotion()
    {
        Debug.Log("A");
    }
}
