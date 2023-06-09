using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject miniMap;
    private bool showCanvas = false;
    private bool showMiniMap = true;

    public void ShowAndHideMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed");
        if (context.performed == true)
        {
            showCanvas = !showCanvas;
            miniMap.SetActive(!showCanvas);
        }
        ChangeCanvasState();
    }

    public void OnContinue()
    {
        Debug.Log("Yep");
        showCanvas = false;
        ChangeCanvasState();
        miniMap.SetActive(!showCanvas);
    }

    //public void OnCredits()
    //{
    //    SceneManager.LoadScene("Credits");
    //}

    public void OnQuit()
    {
        SceneManager.LoadScene("Start");
    }

    void Start()
    {
        ChangeCanvasState();
    }

    private void ChangeCanvasState()
    {
        canvas.gameObject.SetActive(showCanvas);
        if (showCanvas)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}