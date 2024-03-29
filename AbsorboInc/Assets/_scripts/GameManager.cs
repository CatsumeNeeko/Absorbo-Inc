using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float stabilityTimer;
    public float stabilityPercentage;
    private float maximumTime = 150f;

    private void Start()
    {
        stabilityTimer = maximumTime;
    }
    public void Update()
    {
        Timer();
    }
    public void Timer()
    {
        stabilityTimer -= Time.deltaTime;
        stabilityPercentage = (stabilityTimer / maximumTime) * 100;
        if(stabilityTimer <= 0f)
        {
            Debug.Log("Time out");
            GameOver();
        }

    }
    public void IncreaseTimer(float time)
    {
        stabilityTimer += time;
        if(stabilityTimer >= maximumTime)
        {
            stabilityTimer = maximumTime;
        }
    }
    public void GameOver()
    {
        Debug.Log("Game is over!");
    }
}
