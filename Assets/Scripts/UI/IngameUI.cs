using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
    //General
    [Header("General")]
    [SerializeField] private GameObject player;
    private Canvas canvas;

    //Hearts
    [Header("Hearts")]
    [SerializeField] private GameObject firstHeart;
    [SerializeField] private float spaceBetweenHearts;
    [Range(0,1)][SerializeField] private float emptyHeartAlpha;
    private List<GameObject> hearts = new List<GameObject>();
    private Health healthScript;

    //Tributes
    [Header("Tributes")]
    [SerializeField] private List<Texture2D> tributeSprites = new List<Texture2D>();
    [SerializeField] private Vector2 firstTributePos;
    [SerializeField] private float xDistanceBetweenTributes;
    [SerializeField] private float yDistanceBetweenTributes;
    private List<float> currentYDistanceBetweenTributes;
    private TributeManager tributeManager;
    private GameObject[,] tributeIcons;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = player.GetComponent<Health>();
        hearts.Add(firstHeart);
        SpawnHearts(healthScript._maxHealth);

        tributeManager = player.GetComponent<TributeManager>();
        InitialiseTributes();
    }

    private void InitialiseTributes() 
    {
        for (int i = 0; i < tributeSprites.Count; i++)
        {
            currentYDistanceBetweenTributes.Add(0);
        }
        
        for (int i = 1; i < tributeSprites.Count; i++)
        {
            for (int j = 0; j < tributeManager.collectedTributes[i]; j++)
            {
                currentYDistanceBetweenTributes[i] += yDistanceBetweenTributes;
                float xPos = xDistanceBetweenTributes * i;
                Vector3 newTributePos = new Vector3(xPos, currentYDistanceBetweenTributes[i], 0);

                GameObject newTributeIcon = GameObject.Instantiate(firstHeart, newTributePos, Quaternion.identity);
                newTributeIcon.transform.parent = this.transform;
                tributeIcons[i, j] = newTributeIcon;
            }
        }
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

    public void AddTributeToUI(int tributeIndex)
    {
        currentYDistanceBetweenTributes[tributeIndex] += yDistanceBetweenTributes;
        float xPos = xDistanceBetweenTributes * tributeIndex;

        Vector2 tributeIconPosition = new Vector2(xPos, currentYDistanceBetweenTributes[tributeIndex]);
        GameObject newTributeIcon = GameObject.Instantiate(firstHeart, tributeIconPosition, Quaternion.identity);
        newTributeIcon.transform.parent = this.transform;
    }
}
