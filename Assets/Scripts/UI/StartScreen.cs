using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;

public class StartScreen : MonoBehaviour
{
    public string nextScene = "Tutorial";
    public string transitionID;
    public float loadDelay;
    public EasyTransition.TransitionManager transitionManager;

    //[SerializeField] private string startScene;

    public void OnStart()
    {
        //SceneManager.LoadScene("LevelDesign");
        transitionManager.LoadScene(nextScene, transitionID, loadDelay);
    }

    public void OnCredits()
    {
        transitionManager.LoadScene("Credits", transitionID, loadDelay);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}