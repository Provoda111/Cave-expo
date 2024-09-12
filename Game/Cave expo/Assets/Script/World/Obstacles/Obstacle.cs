using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Obstacle : MonoBehaviour
{
    // Для унаследования
    protected bool isHealthPotion;
    protected bool isSpeedPotion;

    public abstract void ApplyEffect(GameObject player);

    public TMP_Text obstacleNameText;
    private PlayerController playerController;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        // Проверка, если игрок рядом с объектом, то может его использовать нажав на E. Появляется интерфейс
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowUI();
        }
    }
    public void ShowUI()
    {
        if (player.transform.position == gameObject.transform.position)
        {

        }
    }
}
