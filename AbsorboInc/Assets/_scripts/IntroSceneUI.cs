using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] GameObject mainUi, controlUi;

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Controls()
    {
        mainUi.gameObject.SetActive(false);
        controlUi.gameObject.SetActive(true);
    }
    public void GoBack()
    {
        mainUi.gameObject.SetActive(true);
        controlUi.gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
