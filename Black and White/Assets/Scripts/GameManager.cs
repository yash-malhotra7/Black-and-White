using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;
    public GameObject Enemy;

    public List<Vector3> playerPositions = new List<Vector3>();
    [HideInInspector]public bool startPartTwo = false;
    [HideInInspector] public bool playerHasDied = false;
    public float enemySpawnInterval;

    private PlayerController Player1;

    private float timeLimit = 100.0f;
    private float currentTime = 0.0f;
    private float enemySpawnTime;
    private bool recordingStarted = false;
    

    private void Start()
    {
        Player1 = FindObjectOfType<PlayerController>();
        Player1.transform.position = StartPos.position;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0.0f || vertical != 0.0f || Input.GetButton("Jump"))
        {
            if (!recordingStarted)
            {
                recordingStarted = true;
            }

            if (currentTime < timeLimit && !startPartTwo)
            {
                playerPositions.Add(Player1.transform.position);
                currentTime += Time.deltaTime;
            }
        }

        if (startPartTwo)
        {
            enemySpawnTime += Time.deltaTime;
            if(enemySpawnTime >= enemySpawnInterval)
            {
                Instantiate(Enemy, StartPos.position, Quaternion.identity);
                enemySpawnTime = 0;
            }
        }
    }

    public void RestartLevel()
    {
        playerHasDied = true;
        Invoke("Restart", 0.5f);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
