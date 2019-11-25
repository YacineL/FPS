using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
   
    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
