using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioSource deathSound;
    DeathHandler deathHandler;
    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            deathSound.Play();
            deathHandler = GetComponent<DeathHandler>();
            deathHandler.StartCoroutine(deathHandler.HandleDeath());
            return;
        }
        damageSound.Play();
    }
    public bool IsDead()
    {
        return hitPoints <= 0f;
    }
}
