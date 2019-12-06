using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    Animator animator;
    bool isDead = false;

    public bool IsDead { get => isDead; set => isDead = value; }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            if(!isDead)
            {
                transform.GetComponent<Collider>().enabled = false;
                animator.SetTrigger("death");
                isDead = true;
            }
            else
            {
                return;
            }
        }
        BroadcastMessage("OnDamageTaken");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("3-great_damage_torso_front"))
        {
            animator.ResetTrigger("damage");
        }
        else 
        {
            animator.SetTrigger("damage");
        }

    }
}
