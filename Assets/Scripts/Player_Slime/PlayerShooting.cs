using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform aimRoot, aimTrigger;
    [SerializeField] private GameObject bulletPF;
    private InputMap _inputMap;
    [SerializeField] private bool isHoldShooting, canShoot = true;
    private PlayerController controller;
    public float shootTimerReductor = 0;
    public int extraDamage = 0;
    public GameObject muzzleFX;

    private void OnEnable()
    {
        if(controller == null)
            controller = GetComponent<PlayerController>();
    
        controller.actionReader.OnShootEvent += TryShootEvent;
    }

    private void OnDisable()
    {
        controller.actionReader.OnShootEvent -= TryShootEvent;
    }

    private void TryShootEvent(bool isPerforming)
    {
        if (isPerforming)
        {
            if (!isHoldShooting)
                isHoldShooting = true;

        }
        else 
        { 
            if (isHoldShooting)
                isHoldShooting = false;
        }
    }
    void Update()
    {
        ActiveAbilitySO bullet = controller.InventoryHandler.shootingType;
        if (bullet != null)
        {
            if (!aimTrigger.gameObject.activeSelf)
                aimTrigger.gameObject.SetActive(true);

            UpdateAim();

            if (isHoldShooting && canShoot)
            {
                bullet.Shoot(aimTrigger, aimRoot.rotation);
                muzzleFX.SetActive(true);
                StartCoroutine(ShootCooldown(bullet.cooldownTime));
            }
        }
        else
        {
            if (aimTrigger.gameObject.activeSelf)
            {
                aimTrigger.gameObject.SetActive(false);
                //aimRoot.transform.up = Vector3.zero;
                aimRoot.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }

    void UpdateAim()
    {

        // Rotation
        Vector2 dir = InputReader.GetAimDirection(aimRoot.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //float angle = Vector2.SignedAngle(Vector2.right, dir);

        //aimRoot.rotation = Quaternion.Euler(0, 0, angle);
        aimRoot.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator ShootCooldown(float canShootTimer)
    {
        canShoot = false;
        yield return new WaitForSeconds(canShootTimer - shootTimerReductor);
        canShoot = true;
    }


}
