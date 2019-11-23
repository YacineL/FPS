using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera fPCamera;
    [SerializeField] float weaponDamage = 30f;
    [SerializeField] float rangeOfShot = 100f;
    [SerializeField] GameObject standardHitEffect;
    [SerializeField] GameObject enemyHitEffect;
    [SerializeField] UnityEvent onShoot;


    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        onShoot.Invoke();
        ProcessRaycast();
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
            target.TakeDamage(weaponDamage);
        }
        else
        {
            return;
        }
    }
}
