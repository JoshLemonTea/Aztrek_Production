using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class StartScreen : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string transitionID;
    public float loadDelay;

    public string nextScene = "Tutorial";

    public void OnStart()
    {
        //transitionManager.LoadScene(nextScene, transitionID, loadDelay);
        SceneManager.LoadScene(nextScene);

    }

    public void OnCredits()
    {
        //transitionManager.LoadScene("Credits", transitionID, loadDelay);
        SceneManager.LoadScene("Credits");

    }

    public void OnQuit()
    {
        Application.Quit();
    }
}