using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Stability")]
    public float stabilityTimer;
    public float stabilityPercentage;
    private float maximumTime = 150f;

    [Header("Escape")]
    [SerializeField] GameObject elevator;
    [SerializeField] GameObject elevatorText;
    [SerializeField] GameObject escapeObject;
    public float escapeTimer;
    public float maxEscapeTimer = 300f;
    private bool canEscape;


    [Header("SceneTransition")]
    [SerializeField] string gameOverScene;
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
        if(escapeTimer <= 0f)
        {
            EscapeBegin();
            //Debug.Log("Escape Has begun");
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
        Destroy(elevator);
        elevatorText.SetActive(false);
        escapeObject.SetActive(true);
    }
    public void GameOver()
    {
        Debug.Log("Game is over!");
        SceneManager.LoadScene(gameOverScene);
    }
}
