using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;
public class Obelisk : MonoBehaviour
{
    private int hitCount = 0;
    private GameManager gameManager;
    private Transform player;
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sword" && player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("RightHand@Attack01"))
        {
            hitCount++;

            if (hitCount >= 3)
            {
                Destroy(gameObject);
                gameManager.DrawEndMenu();
            }
        }
    }
}
