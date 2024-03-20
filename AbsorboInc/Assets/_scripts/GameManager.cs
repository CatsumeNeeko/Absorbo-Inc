using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float survivalTimer;
    public float survivalPercentage;
    private float maximumTime = 150f;

    private void Start()
    {
        survivalTimer = maximumTime;
    }
    public void Update()
    {
        Timer();
    }
    public void Timer()
    {
        survivalTimer -= Time.deltaTime;
        survivalPercentage = (survivalTimer / maximumTime) * 100;
        if(survivalTimer <= 0f)
        {
            Debug.Log("Time out");
        }

    }
    public void GameOver()
    {
        Debug.Log("Game is over!");
    }
}
