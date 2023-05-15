using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    public string transitionID;
    public float loadDelay;
    public EasyTransition.TransitionManager transitionManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            transitionManager.LoadScene(_nextScene, transitionID, loadDelay);
        }
    }
}
