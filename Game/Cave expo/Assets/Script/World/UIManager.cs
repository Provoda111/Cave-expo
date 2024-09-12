using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text obstacleNameText;
    public GameObject ObstacleUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowObstacleUI()
    {
        ObstacleUI.SetActive(true);
        obstacleNameText.text = "Press E to interact with this obstacle";
    }
}
