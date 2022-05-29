using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputReader actionReader;
    public Player_Movement MovementHandler { get; private set; }
    public PlayerShooting ShootingHandler { get; private set; }
    public Stomach_Inventory InventoryHandler { get; private set; }
    public HealthManager HealthHandler { get; private set; }

    private void OnEnable()
    {
        if(MovementHandler == null)
            MovementHandler = GetComponent<Player_Movement>();
    
        if(ShootingHandler == null)
            ShootingHandler = GetComponent<PlayerShooting>();
    
        if(InventoryHandler == null)
            InventoryHandler = GetComponent<Stomach_Inventory>();

        if(HealthHandler == null)
            HealthHandler = GetComponent<HealthManager>();


        actionReader.EnableActions();

        HUDManager.Instance.SetupLifePoints(HealthHandler.maxLife);
    }

    private void OnDisable()
    {
        actionReader.DisableActions();
    }


    public void HitPlayer(int maxLife, int currentLife)
    {
        HUDManager.Instance.UpdateLifePoints(currentLife);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we hit a heal object
        if (collision.CompareTag("LifePoint"))
        {
            // Avoid healing when we're already full
            // imo this can be a bit frustrating to pick the object while you're evading something
            if (HealthHandler.currentLife < HealthHandler.maxLife)
            {

                HealthHandler.currentLife = HealthHandler.maxLife;
                HUDManager.Instance.UpdateLifePoints(HealthHandler.currentLife);
                // TODO: Despawn on Pool
                Destroy(collision.gameObject);
            }
        }

    }

}
