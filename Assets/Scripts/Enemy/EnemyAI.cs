using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseDistance = 10f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] AudioSource idleSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] AudioSource chaseSound;
    [SerializeField] AudioSource attackSound;
    [SerializeField] AudioSource deathSound;

    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth enemyHealth;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth.IsDead)
        {
            enabled = false;
            navMeshAgent.enabled = false;
            chaseSound.Stop();
            idleSound.Stop();
            attackSound.Stop();
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseDistance)
        {
            isProvoked = true;
        }
        if(!idleSound.isPlaying && !enemyHealth.IsDead)
        {
            idleSound.PlayDelayed(1f);
        }

        /**/if(target.GetComponent<PlayerHealth>().IsDead())
        {
            MuteSFX();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
        if(!chaseSound.isPlaying)
        {
            idleSound.Stop();
            attackSound.Stop();
            chaseSound.Play();
        }
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
        if (!attackSound.isPlaying)
        {
            idleSound.Stop();
            chaseSound.Stop();
            attackSound.Play();
        }
    }

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
    public void MuteSFX()
    {
        idleSound.Stop();
        chaseSound.Stop();
        attackSound.Stop();
        damageSound.Stop();
    }
    public void OnDamageTaken()
    {
        isProvoked = true;
        if (!damageSound.isPlaying)
        {
            idleSound.Stop();
            chaseSound.Stop();
            attackSound.Stop();
            if(enemyHealth.IsDead)
            {
                deathSound.Play();
                return;
            }
            damageSound.Play();
        }
    }
}
