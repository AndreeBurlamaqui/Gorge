using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    #region SINGLETON

    private static HUDManager _instance;
    public static HUDManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HUDManager>();
            }

            return _instance;
        }
    }

    #endregion

    #region FIELDS

    [Header("Health HUD")]
    [SerializeField] GameObject lifePointRoot;
    [SerializeField] GameObject lifePointPrefab;
    private List<Image> lifeList = new List<Image>();
    private const float lifeFillStage = 0.25f;

    #endregion

    /// <summary>
    /// Instantiate as many life points related to <paramref name="maxLife"/>
    /// </summary>
    public void SetupLifePoints(int maxLife)
    {
        int uiLifeCount = maxLife / 4; // Each life UI is split in 4 stages

        for (int l = 0; l < uiLifeCount; l++)
        {
            GameObject newLife = Instantiate(lifePointPrefab, lifePointRoot.transform);
            Image i = newLife.transform.GetChild(0).GetComponent<Image>();
            i.fillAmount = 1;
            lifeList.Add(i);
        }
    }

    public void UpdateLifePoints(int currentLife)
    {

        // Each life UI is split in 4 stages
        // So, to update them correctly, we need to know which sprites to update
        // And then, update the fill stages
        int minLifePoints = currentLife / 4;
        float lifeFill = ((float)currentLife / 4) % 1;

        Debug.Log($"Updating life points by {currentLife} on life count {lifeFill}");

        // Directly change the fill amount
        //lifeList[minLifePoints].fillAmount = uiCurLifeCount % 1;

        // Cycle through the life list in case we need to update the heart that are below on this
        for(int l = 0; l < lifeList.Count; l++)
        {
            if (l == minLifePoints)
            {
                // We need to update the fill amount
                lifeList[l].fillAmount = lifeFill;

                Debug.Log($"Fill amount of life {1} is {lifeFill}", lifeList[l]);
            }
            else
            {
                lifeList[l].fillAmount = l > minLifePoints ? 0 : 1;
            }
        }
    }

}
