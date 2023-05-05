using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;

public class Tribute : MonoBehaviour
{
    public string tributeType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<TributeManager>().CollectTribute(tributeType);
            gameObject.SetActive(false);
        }
    }
}
