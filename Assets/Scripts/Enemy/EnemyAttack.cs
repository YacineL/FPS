using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (target == null) return;
        if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            target.TakeDamage(damage);
            target.GetComponent<DamageDisplay>().ShowDamageImpact();
        }
    }
}
