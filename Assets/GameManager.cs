using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI scoreText;
    public int score;

    public TextMeshProUGUI timerText;

    public float roundDuration;
    private float actualRoundDuration;

    public TextMeshProUGUI maxScoreText;
    public TextMeshProUGUI continueText;

    public ModelController playerPrefab;
    public bool isGameRunning = true;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            actualRoundDuration = roundDuration;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (continueText.gameObject.activeSelf)
        {
            if (Gamepad.current.aButton.isPressed)
            {
                StartGame();
                maxScoreText.gameObject.SetActive(false);
            }
        }
        else
        {
            scoreText.text = score.ToString();
            actualRoundDuration -= Time.deltaTime;
            timerText.text = actualRoundDuration.ToString("F1");

            if (actualRoundDuration < 0)
            {
                EndRound();
            }
        }
    }

    private void StartGame()
    {
        continueText.gameObject.SetActive(false);
        maxScoreText.gameObject.SetActive(false);
        
        isGameRunning = true;

        //dirty boi
        playerPrefab.transform.position = playerPrefab.startingPos;
        playerPrefab.index = 0;
        playerPrefab.GetComponent<NavMeshAgent>().SetDestination(playerPrefab.points[playerPrefab.index].position);
        
        actualRoundDuration = roundDuration;
    }

    private void EndRound()
    {
        int maxScore = PlayerPrefs.GetInt("Score");
        print(maxScore + " max score");

        if (maxScore < score)
        {
            PlayerPrefs.SetInt("Score", score);
            maxScoreText.gameObject.SetActive(true);
            maxScoreText.text = "New High Score: " + score;
        }

        isGameRunning = false;
        continueText.gameObject.SetActive(true);
        score = 0;
    }
}
