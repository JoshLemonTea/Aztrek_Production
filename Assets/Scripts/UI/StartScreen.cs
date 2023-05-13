using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public string transitionID;
    public float loadDelay;
    public EasyTransition.TransitionManager transitionManager;

    //[SerializeField] private string startScene;

    public void OnStart()
    {
        //SceneManager.LoadScene("LevelDesign");
        transitionManager.LoadScene("LevelDesign", transitionID, loadDelay);
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
