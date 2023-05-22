using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFloatingBlocks : MonoBehaviour
{
    [SerializeField]
    private bool _makesPlatformsAppear;
    [SerializeField]
    private GameObject[] _floatingPlatforms;

    private void Start()
    {
        foreach(GameObject g in _floatingPlatforms)
        {
            g.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            foreach(GameObject g in _floatingPlatforms)
            {
                g.SetActive(_makesPlatformsAppear);
            }
        }
    }
}
