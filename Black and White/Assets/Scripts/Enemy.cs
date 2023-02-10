using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;

    private int currentPosition = 0;
    public float speed = 1.0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameManager.playerHasDied = true;
            gameManager.RestartLevel();
        }
    }

    void Update()
    {
        if (!gameManager.playerHasDied)
        {
            if (currentPosition < gameManager.playerPositions.Count)
            {
                transform.position = Vector3.Lerp(transform.position, gameManager.playerPositions[currentPosition], speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, gameManager.playerPositions[currentPosition]) < 0.1f)
                {
                    currentPosition++;
                }
            }
            if (currentPosition >= gameManager.playerPositions.Count)
            {
                Destroy(gameObject);
            }
        }
    }
}
