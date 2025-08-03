using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour, IDamage
{
    [Header("Stats")]
    [SerializeField] float MaxHp;
    [SerializeField] float currHp;
    public bool dead;

    private NavMeshAgent agent;
    private EnemyAI enemyAi;
    private Collider col;
    private Animator animator;

    [Header("Death Settings")]
    public float deathAnimationDuration = 4f;
    public GameObject[] puDrops;
    [Range(0f, 100f)] public float dropChancePercentage = 25f;

    private void Start()
    {
        currHp = MaxHp;
        dead = false;

        agent = GetComponent<NavMeshAgent>();
        enemyAi = GetComponent<EnemyAI>();
        col = GetComponent<Collider>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        currHp -= damage;
        currHp = Mathf.Max(currHp, 0);

        Debug.Log($"{gameObject.name} took {damage} damage, current HP: {currHp}");

        if (currHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        dead = true;
        Debug.Log($"{gameObject.name} died!");
        enemyAi.PlayDeathSound();
        int randomNum = UnityEngine.Random.Range(0, 100);

        // Play death animation using "isDead" bool
        if (animator != null)
        {
            animator.SetBool("isDead", true);
        }

        // Stop movement and AI
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        if (enemyAi != null)
        {
            enemyAi.enabled = false;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        Debug.Log("Drop Chance Num: " + randomNum);
        if (randomNum <= dropChancePercentage)
        {
            int randomValue = UnityEngine.Random.Range(0, puDrops.Length);
            GameObject objectToSpawn = puDrops[randomValue];

            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }

        Destroy(gameObject, deathAnimationDuration);
    }
}
