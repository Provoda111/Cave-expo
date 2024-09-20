using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject player;
    public GameObject bookUI;       
    public GameObject interactUI;        
  
    private bool canRead = false;    
    private bool isReading = false;  
    private AudioSource audioSource;

    public float activationDistance = 3.5f; 

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= activationDistance && !isReading)
        {
            interactUI.SetActive(true);
            Debug.Log("Test 1");
            canRead = true;
        }
        else
        {
            interactUI.SetActive(false);
            canRead = false;
        }
        if (canRead && Input.GetKeyDown(KeyCode.E))
        {
            OpenBook();
            Debug.Log("Test 3");
        }
        if (isReading && Input.GetKeyDown(KeyCode.E))
        {
            CloseBook();
        }
    }
    private void OpenBook()
    {
        isReading = true;
        bookUI.SetActive(true);
        audioSource.Play();
        interactUI.SetActive(false);
    }
    private void CloseBook()
    {
        isReading = false; 
        bookUI.SetActive(false);
        audioSource.Stop();
        interactUI.SetActive(true);
    }
}
