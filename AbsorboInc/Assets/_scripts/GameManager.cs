using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float stabilityTimer;
    public float stabilityPercentage;
    private float maximumTime = 150f;


    public float escapeTimer;
    public float maxEscapeTimer = 300f;
    private bool canEscape;


    private void Start()
    {
        stabilityTimer = maximumTime;
        escapeTimer = maxEscapeTimer;
    }
    public void Update()
    {
        Timer();
    }
    public void Timer()
    {
        //
        stabilityTimer -= Time.deltaTime;
        stabilityPercentage = (stabilityTimer / maximumTime) * 100;
        if(stabilityTimer <= 0f)
        {
            Debug.Log("Time out");
            GameOver();
        }
        //
        escapeTimer -= Time.deltaTime;
        if(escapeTimer <= 0f && canEscape)
        {
            EscapeBegin();
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
    public void EscapeBegin()
    {

    }
    public void GameOver()
    {
        Debug.Log("Game is over!");
    }
}
