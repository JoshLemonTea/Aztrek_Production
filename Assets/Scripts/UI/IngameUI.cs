using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    //General
    [SerializeField] private GameObject player;
    private Canvas canvas;

    //Hearts
    [SerializeField] private GameObject firstHeart;
    [SerializeField] private float spaceBetweenHearts;
    [Range(0,1)][SerializeField] private float emptyHeartAlpha;
    private List<GameObject> hearts = new List<GameObject>();
    private Health healthScript;

    //Tributes
    [SerializeField] private List<Texture2D> tributes = new List<Texture2D>();
    private TributeManager tributeManager;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = player.GetComponent<Health>();
        hearts.Add(firstHeart);
        SpawnHearts(healthScript._maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
    }

    private void SpawnHearts(int heartAmount)
    {
        for (int i = 1; i < heartAmount; i++)
        {
            Vector3 newHeartPos = firstHeart.transform.position + new Vector3(spaceBetweenHearts * i, 0, 0);
            GameObject newHeart = GameObject.Instantiate(firstHeart, newHeartPos, Quaternion.identity);
            newHeart.transform.parent = this.transform;
            hearts.Add(newHeart);
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Image thisHeartSprite = hearts[i].GetComponent<Image>();
            int heartNumber = i + 1;
            if (heartNumber > healthScript._currentHealth)
            {
                Color newColor = thisHeartSprite.color;
                newColor.a = emptyHeartAlpha;
                thisHeartSprite.color = newColor;
            }
            else
            {
                Color newColor = thisHeartSprite.color;
                newColor.a = 255;
                thisHeartSprite.color = newColor;
            }
        }
    }
}
