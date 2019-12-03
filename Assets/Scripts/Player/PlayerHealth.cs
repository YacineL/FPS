using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    DeathHandler deathHandler;
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            deathHandler = GetComponent<DeathHandler>();
            deathHandler.StartCoroutine(deathHandler.HandleDeath());
        }
    }
    public bool IsDead()
    {
        return hitPoints <= 0f;
    }
}
