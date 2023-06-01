using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickCharacter : MonoBehaviour
{
    public string nextScene = "Tutorial";
    public string transitionID;
    public float loadDelay;
    public EasyTransition.TransitionManager transitionManager;
    public void PickedGirl()
    {
        SetPlayerModel.isBoy = false;
        NextScene();
    }

    public void PickedBoy()
    {
        SetPlayerModel.isBoy = true;
        NextScene();
    }

    private void NextScene()
    {
        transitionManager.LoadScene(nextScene, transitionID, loadDelay);
        //SceneManager.LoadScene(nextScene);

    }
}
