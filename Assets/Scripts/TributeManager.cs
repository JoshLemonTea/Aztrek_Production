using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyTransition;

public class TributeManager : MonoBehaviour
{
    [SerializeField] private List<string> tributeNames = new List<string>();
    [SerializeField] private string endScene;
    [HideInInspector] public List<int> collectedTributes = new List<int>();
    [HideInInspector] public List<int> maxTributes = new List<int>();
    private List<bool> finishedTributes = new List<bool>();

    //Transition
    [Header("Transition stuff")]
    public string transitionID;
    public float loadDelay;
    public EasyTransition.TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tributeNames.Count; i++)
        {
            maxTributes.Add(0);
            finishedTributes.Add(false);
            collectedTributes.Add(0);
        }

        CountMaxTributes();

        Debug.Log(tributeNames.Count);
    }

    private void CountMaxTributes()
    {
        List<GameObject> allTributes = new List<GameObject>();
        allTributes.AddRange(GameObject.FindGameObjectsWithTag("Tribute"));

        //Add the found tributes to their corresponding maxTributes index
        foreach (GameObject tribute in allTributes)
        {
            for (int i = 0; i < tributeNames.Count; i++)
            {
                if (tribute.GetComponent<Tribute>().tributeType == tributeNames[i])
                {
                    maxTributes[i] += 1;
                }
            }
        }
    }

    public void CollectTribute(string name)
    {
        for (int i = 0; i < tributeNames.Count; i++)
        {
            if (name == tributeNames[i])
            {
                collectedTributes[i] += 1;
                Debug.Log(tributeNames[i] + "'s collected:  " + collectedTributes[i]);
            }
        }
        CheckIfTributeGoalReached();
        CheckIfAllTributesCollected();
    }

    private void CheckIfTributeGoalReached()
    {
        for (int i = 0; i < maxTributes.Count; i++)
        {
            if (maxTributes[i] == collectedTributes[i])
            {
                finishedTributes[i] = true;
                if (finishedTributes[i])
                {
                    Debug.Log("Collected all tributes of type: " + tributeNames[i]);
                }
            }
        }
    }

    private void CheckIfAllTributesCollected()
    {
        bool hasAllTributes = true;
        foreach (bool hasFinished in finishedTributes)
        {
            if (hasFinished == false)
            {
                hasAllTributes = false;
            }
        }

        if (hasAllTributes)
        {
            transitionManager.LoadScene(endScene, transitionID, loadDelay);
        }
    }
}
