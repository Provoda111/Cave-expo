using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccountRegistration : MonoBehaviour
{
    public InputField nicknameInputField;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RegisterAccount()
    {
        string nickname = nicknameInputField.text;
        if (!string.IsNullOrEmpty(nickname))
        {
            gameManager.AddPlayer(nickname);
        }
        else
        {
            Debug.Log("Nickname cannot be empty");
        }
    }
}
