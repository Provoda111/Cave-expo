using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine.SceneManagement;

enum EndState { Died, Winned }

public class GameManager : MonoBehaviour
{
    public GameObject player;
    [System.Serializable]
    public class Player
    {
        public string nickname;
        public int kills;
        public int deaths;
    }
    public List<Player> playerList = new List<Player>();
    private string dbPath;
    private PlayerController playerController;
    private EndState endState;
    public TMP_Text endStateText;
    private bool inMainMenu = false;

    public GameObject mainMenuCanvas;
    public GameObject endMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        dbPath = Path.Combine("gamedatabase.db");
        playerController = gameObject.GetComponent<PlayerController>();
        if (!File.Exists(dbPath))
        {
            CreateDatabase();
        }
    }
    void Update()
    {
        if (endStateText == null)
        {
            return;
        }
        switch (endState)
        {
            case EndState.Died:
                endStateText.text = "You died!";
                break;
            case EndState.Winned:
                endStateText.text = "You winned!";
                break;
            default:
                endStateText.text = string.Empty;
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DrawEndMenu();
            inMainMenu = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && inMainMenu)
        {
            ExitMainMenu();
        }

    }
    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection("URl=file:" + dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"CREATE TABLE IF NOT EXISTS Users (
                                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Nickname TEXT NOT NULL UNIQUE,
                                        Deaths INTEGER DEFAULT 0,
                                        Kills INTEGER DEFAULT 0)";
                command.ExecuteNonQuery();
            }
        }
    }
    public void AddPlayer(string nickname)
    {
        if (!PlayerExists(nickname))
        {
            Player newPlayer = new Player { nickname = nickname, kills = 0, deaths = 0 };
            playerList.Add(newPlayer);
            using (var connection = new SqliteConnection("URl=file:" + dbPath))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Users (Nickname, Deaths, Kills) VALUES (@Nickname, 0, 0)";
                    command.Parameters.AddWithValue("@Nickname", nickname);
                    command.ExecuteNonQuery();
                }
            }
            Debug.Log($"Player {nickname} added to the database");
        }
        else
        {
            Debug.Log($"Player {nickname} already exists");
        }
    }
    private bool PlayerExists(string nickname)
    {
        using (var connection = new SqliteConnection("URl=file:" + dbPath))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE Nickname = @Nickname";
                command.Parameters.AddWithValue("@Nickname", nickname);
                int count = System.Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
    public void DrawEndMenu()
    {
        mainMenuCanvas.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Sample Scene");
    }
    public void LeaveGame()
    {
        Application.Quit();
    }
    public void ExitMainMenu()
    {
        mainMenuCanvas.SetActive(false);
        inMainMenu = false;
    }
}
