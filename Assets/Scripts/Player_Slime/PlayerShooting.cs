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
    private Stomach_Inventory si;
    public float shootTimerReductor = 0;
    public int extraDamage = 0;
    public GameObject muzzleFX;

    private void Awake()
    {
        _inputMap = new InputMap();
        _inputMap.Action.Shoot.performed += OnShoot;
        _inputMap.Action.Shoot.canceled += OnShoot;

        si = GetComponent<Stomach_Inventory>();

    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }
    private void OnDisable()
    {
        _inputMap.Disable();
    }

    private void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!isHoldShooting)
                isHoldShooting = true;

        }
        
        if (ctx.canceled)
        {
            if (isHoldShooting)
                isHoldShooting = false;
        }
    }
    void Update()
    {
        ActiveAbilitySO bullet = si.shootingType;
        if (bullet != null)
        {
            if (!aimTrigger.gameObject.activeSelf)
                aimTrigger.gameObject.SetActive(true);

            Vector2 dir = _inputMap.Action.MousePos.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(aimRoot.position);
            float mAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            aimRoot.rotation = Quaternion.AngleAxis(mAngle - 90, Vector3.forward);


            if (isHoldShooting && canShoot)
            {
                bullet.Shoot(aimTrigger, mAngle);
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

    IEnumerator ShootCooldown(float canShootTimer)
    {
        canShoot = false;
        yield return new WaitForSeconds(canShootTimer - shootTimerReductor);
        canShoot = true;
    }


}
