using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputReader actionReader;
    public Player_Movement movementHandler { get; private set; }
    public PlayerShooting shootingHandler { get; private set; }
    public Stomach_Inventory inventoryHandler { get; private set; }

    private void Start()
    {
        movementHandler = GetComponent<Player_Movement>();
        shootingHandler = GetComponent<PlayerShooting>();
        inventoryHandler = GetComponent<Stomach_Inventory>();
        actionReader.EnableActions();
    }

    private void OnDisable()
    {
        actionReader.DisableActions();
    }
}
