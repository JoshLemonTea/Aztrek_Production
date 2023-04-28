using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(() => LoadScene("name"));
    }

    private void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
