using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private bool showCanvas = false;

    public void ShowAndHideMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Pressed");
        if (context.performed == true)
        {
            showCanvas = !showCanvas;
        }
        ChangeCanvasState();
    }

    public void OnContinue()
    {
        showCanvas = false;
        ChangeCanvasState();
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("Start");
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeCanvasState();
    }

    // Update is called once per frame
    void Update()
    {
        
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