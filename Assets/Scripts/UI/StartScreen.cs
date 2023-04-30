using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private string startScene;

    public void OnStart()
    {
        SceneManager.LoadScene(startScene);
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
