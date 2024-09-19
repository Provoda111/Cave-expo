using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

enum EndState { Died, Winned }

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerController;
    private EndState endState;
    public TMP_Text endStateText;
    // Start is called before the first frame update
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.playerHP <= 0)
        {

        }
        switch (endState)
        {
            case EndState.Died:
                endStateText.text = "You died!";
                break;
            case EndState.Winned:
                endStateText.text = "You winned!";
                break;
        }
    }

    void DrawEndMenu()
    {

    }
    void RestartGame()
    {

    }
    void EnterMainMenu()
    {

    }
}
