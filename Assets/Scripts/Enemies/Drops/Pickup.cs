using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public Drop dropSO;
    private bool canBePicked;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = dropSO.image;

        StartCoroutine(PickupCooldown());
    }

    IEnumerator PickupCooldown()
    {
        canBePicked = false;
        yield return new WaitForSeconds(0.2f);
        canBePicked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBePicked)
        {
            //trigger for object pickup
            if (collision.gameObject.tag == "Player")
            {
                Stomach_Inventory playerStomach = collision.GetComponent<Stomach_Inventory>();


                if (dropSO.active != null)
                {
                    playerStomach.PickupAbility(dropSO.active, dropSO);
                    DestroyPickup();
                }
                else
                {
                    playerStomach.PickupAbility(dropSO.passive, this);
                }
            }
        }
    }

    public void DestroyPickup()
    {
        Destroy(gameObject);
    }
    


}
