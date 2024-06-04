using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class IntroSceneUI : MonoBehaviour
{
    [SerializeField] GameObject playGameUI;
    [SerializeField] GameObject mainUi, controlUi;
    [SerializeField] GameObject abilityOne, abilityTwo, abilityThree;

    public void PlayScreen()
    {
        mainUi.gameObject.SetActive(false);
        playGameUI.gameObject.SetActive(true);
    }

    public void PlayGameMed()
    {
        SceneManager.LoadScene("MainSceneMedium");
    }
    public void PlayGameLight()
    {
        SceneManager.LoadScene("MainSceneLight");
    }
    public void PlayGameHeavy()
    {
        SceneManager.LoadScene("MainSceneHeavy");
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
        abilityOne.gameObject.SetActive(false);
        abilityTwo.gameObject.SetActive(false);
        abilityThree.gameObject.SetActive(false);
        playGameUI.gameObject.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void AbiltiyP1()
    {
        abilityOne.gameObject.SetActive(true);

        mainUi.gameObject.SetActive(false);
        controlUi.gameObject.SetActive(false);
        abilityTwo.gameObject.SetActive(false);
        abilityThree.gameObject.SetActive(false);
    }
    public void AbiltiyP2()
    {
        abilityTwo.gameObject.SetActive(true);

        abilityOne.gameObject.SetActive(false);
        abilityThree.gameObject.SetActive(false);
    }
    public void AbiltiyP3()
    {
        abilityThree.gameObject.SetActive(true);

        abilityOne.gameObject.SetActive(false);
        abilityTwo.gameObject.SetActive(false);
    }
}
