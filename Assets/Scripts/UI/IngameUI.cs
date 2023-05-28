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
        //InitialiseTributes();
    }

    private void UpdateTributeCounts()
    {
        for (int i = 0; i < tributeManager.collectedTributes.Count; i++)
        {
            string newText = tributeManager.collectedTributes[i] + "/" + tributeManager.maxTributes[i];
            tributeCounts[i].text = newText;
        }
    }

    //private void InitialiseTributes()
    //{
    //    for (int i = 0; i < Mathf.Min(tributeSprites.Count, tributeManager.maxTributes.Count); i++)
    //    {
    //        currentYDistanceBetweenTributes.Add(firstTributePos.y);
    //        tributeIcons.Add(new List<GameObject>());
    //    }

    //    for (int i = 0; i < Mathf.Min(tributeSprites.Count, tributeManager.maxTributes.Count); i++)
    //    {
    //        for (int j = 0; j < tributeManager.maxTributes[i]; j++)
    //        {
    //            currentYDistanceBetweenTributes[i] -= yDistanceBetweenTributes;
    //            float xPos = firstTributePos.x + (xDistanceBetweenTributes * i);
    //            Vector3 newTributePos = new Vector3(xPos, currentYDistanceBetweenTributes[i], 0);

    //            GameObject newTributeIcon = GameObject.Instantiate(tributeIconExample, newTributePos, Quaternion.identity);
    //            newTributeIcon.GetComponent<Image>().sprite = tributeSprites[i];
    //            newTributeIcon.transform.parent = this.transform;
    //            List<GameObject> currentTributeIconsList = tributeIcons[i];
    //            currentTributeIconsList.Add(newTributeIcon);
    //        }
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
        //UpdateTributeIcons();
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
            newHeart.transform.localScale = new Vector3(.9f, .9f, .9f);
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

    //public void UpdateTributeIcons()
    //{
    //    for (int i = 0; i < tributeIcons.Count; i++)
    //    {
    //        List<GameObject> currentTributeIconsList = tributeIcons[i];
    //        for (int j = 0; j < currentTributeIconsList.Count; j++)
    //        {
    //            GameObject currentIcon = currentTributeIconsList[j];
    //            //Check if the tribute has been collected
    //            if (j >= tributeManager.collectedTributes[i])
    //            {
    //                Color newColor = currentIcon.GetComponent<Image>().color;
    //                newColor.a = emptyAlpha;
    //                currentIcon.GetComponent<Image>().color = newColor;
    //            }
    //            else
    //            {
    //                Color newColor = currentIcon.GetComponent<Image>().color;
    //                newColor.a = 255;
    //                currentIcon.GetComponent<Image>().color = newColor;
    //            }
    //        }
    //    }
    //}
}