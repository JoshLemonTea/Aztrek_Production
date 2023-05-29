using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngameUI : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject player;
    [Range(0, 1)][SerializeField] private float emptyAlpha;
    private Canvas canvas;

    // Hearts
    [Header("Hearts")]
    [SerializeField] private GameObject firstHeart;
    [SerializeField] private float spaceBetweenHearts;
    private List<GameObject> hearts = new List<GameObject>();
    private Health healthScript;

    //Tributes
    [Header("Tributes")]
    [SerializeField] private List<TMPro.TextMeshProUGUI> tributeCounts = new List<TMPro.TextMeshProUGUI>();
    private TributeManager tributeManager;

    //MiniMap
    [Header("Minimap")]
    [SerializeField] private GameObject playerArrow;

    // Start is called before the first frame update
    void Start()
    {
        healthScript = player.GetComponent<Health>();
        hearts.Add(firstHeart);
        SpawnHearts(healthScript._maxHealth);

        tributeManager = player.GetComponent<TributeManager>();
       
    }

    private void UpdateTributeCounts()
    {
        for (int i = 0; i < tributeManager.collectedTributes.Count; i++)
        {
            string newText = tributeManager.collectedTributes[i] + "/" + tributeManager.maxTributes[i];
            tributeCounts[i].text = newText;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
       
        Vector3 newArrowRotation = playerArrow.GetComponent<RectTransform>().eulerAngles;
        newArrowRotation.z = player.transform.eulerAngles.y;
        playerArrow.GetComponent<RectTransform>().eulerAngles = newArrowRotation;
        UpdateTributeCounts();
    }

    private void SpawnHearts(int heartAmount)
    {
        for (int i = 1; i < heartAmount; i++)
        {
            Vector3 newHeartPos = firstHeart.transform.position + new Vector3(spaceBetweenHearts * i, 0, 0);
            GameObject newHeart = GameObject.Instantiate(firstHeart, newHeartPos, Quaternion.identity);
            newHeart.transform.parent = this.transform;
            //newHeart.transform.localScale = new Vector3(.9f, .9f, .9f);
            hearts.Add(newHeart);
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Image thisHeartSprite = hearts[i].GetComponent<Image>();
            int heartNumber = i + 1;

            if (heartNumber > healthScript.CurrentHealth)
            {
                Color newColor = thisHeartSprite.color;
                //newColor.a = emptyAlpha;
                thisHeartSprite.color = newColor;

                if (i == healthScript.CurrentHealth && healthScript.HeartPieceCount > 0)
                {
                    float fillAmount = (float)healthScript.HeartPieceCount / healthScript.RequiredHearthPieces;
                    thisHeartSprite.fillAmount = fillAmount;
                }
                else
                {
                    thisHeartSprite.fillAmount = 0f; // Empty heart
                }
            }
            else
            {
                thisHeartSprite.color = Color.white;
                thisHeartSprite.fillAmount = 1f; // Filled heart
            }
        }
    }

  
}