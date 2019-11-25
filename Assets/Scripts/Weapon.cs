using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;
    [SerializeField] float weaponDamage = 30f;
    [SerializeField] float rangeOfShot = 100f;
    [SerializeField] GameObject standardHitEffect;
    [SerializeField] GameObject enemyHitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    
    
    [SerializeField] UnityEvent onShoot;
    

    bool canShoot = true;

    public float WeaponDamage { get => weaponDamage; set => weaponDamage = value; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            onShoot.Invoke();
            ProcessRaycast();
            //ammoSlot.ReduceCurrentAmmo();
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    public void CreateHitImpact(RaycastHit hit , GameObject hitEffect)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal),hit.transform.parent);
        Destroy(impact, .2f);
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(fPCamera.transform.position, fPCamera.transform.forward, out hit, rangeOfShot))
        {
            print(hit.transform.name);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                CreateHitImpact(hit, standardHitEffect);
                return;
            }
            CreateHitImpact(hit, enemyHitEffect);
            target.TakeDamage(WeaponDamage);
        }
        else
        {
            return;
        }
    }
}
