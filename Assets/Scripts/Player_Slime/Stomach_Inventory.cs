using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stomach_Inventory : MonoBehaviour
{
    public ActiveAbilitySO shootingType;
    public PassiveAbilitySO[] passives = new PassiveAbilitySO[2];
    public Drop[] currentDrop = new Drop[3];
    [SerializeField] private GameObject dropPrefab;
    public SpriteRenderer[] itemSlots = new SpriteRenderer[3];
    private Image[] lifeHearts;
    private HealthManager hm;
    [SerializeField] private GameObject healFX;
    private void Awake()
    {
        Debug.Log("Quando fizer os checkpoints verifica os shootings aq", this);
        //Da? tira essas linhas de baixo
        shootingType = null;
        for (int x = 0; x < passives.Length; x++)
        {
            passives[x] = null;
        }
    }
    private void Start()
    {
        hm = GetComponent<HealthManager>();
        lifeHearts = GetComponentsInChildren<Image>();
    }

    

    public void PickupAbility(ActiveAbilitySO active, Drop newCurrentDrop)
    {
        if (shootingType != null)
            SpitDrop(0);

        shootingType = Instantiate(active);
        currentDrop[0] = newCurrentDrop;
        itemSlots[0].sprite = currentDrop[0].image;
    }

    public void PickupAbility(PassiveAbilitySO passive, Pickup whichPickup)
    {
        for(int x = 0; x < passives.Length; x++)
        {

            if(passives[x] == null)
            {
                currentDrop[x + 1] = whichPickup.dropSO;
                itemSlots[x+1].sprite = currentDrop[x + 1].image;
                passives[x] = Instantiate(passive);
                whichPickup.DestroyPickup();
                UpdatePassiveEffects();

                break;
            }

        }
    }

    private void SpitDrop(int slot)
    {
        GameObject dpf = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        dpf.GetComponent<Pickup>().dropSO = Instantiate(currentDrop[slot]);
    }

    public void RemoveActive()
    {
        shootingType = null;
        itemSlots[0].sprite = null;
    }

    private void UpdatePassiveEffects()
    {
        for (int x = 0; x < passives.Length; x++)
        {
            if (passives[x] != null)
            {
                // First reset in case we already have something buffed up    
                passives[x].ResetEffect();
            }

        }

        for (int x = 0; x < passives.Length; x++)
        {
            if (passives[x] != null)
            {
                // Now apply the effect
                passives[x].ApplyEffect();
            }

        }

    }

}
