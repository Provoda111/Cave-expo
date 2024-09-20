using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using System.IO;

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

    // Update is called once per frame
    void Update()
    {
        /*if (playerController.playerHP <= 0)
        {

        }*/
        if (endStateText == null)
        {
            //Debug.LogError("endStateText is not assigned!");
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
