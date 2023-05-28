using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerModel : MonoBehaviour
{
    [SerializeField] private GameObject girlModel;
    [SerializeField] private GameObject boyModel;
    [HideInInspector] public static bool isBoy = true;
    // Start is called before the first frame update
    void Start()
    {
        girlModel.SetActive(!isBoy);
        boyModel.SetActive(isBoy);
    }
}
