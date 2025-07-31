using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrolling")]
    public float patrolWaitTime = 2f;
    public float wanderRadius = 7f;
    public float driftSpeed = 2f;
    public float patrolRange = 7f;
    [Range(0, 180)]
    public float patrolConeAngle = 30f;

    [Header("Detection")]
    public float viewRange = 5f;
    public float shootRange = 5f;

    [Header("Shooting")]
    public float shootCoolDown = 1f;
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float bulletForce = 10f;

    [Header("Animation")]
    public float speedSmoothTime = 0.1f;

    public float speedSmoothTimeUp = 0.1f;   // fast to start walking
    public float speedSmoothTimeDown = 0.5f; // slow to go back to idle
    private float speedVelocity;

    private NavMeshAgent agent;
    private Transform player;

    private float patrolWaitTimer = 0f;
    private float shootTimer = 0f;
    private float currentSpeedPercent = 0f;

    private bool hasSeenPlayer = false;
    private bool waitingAtPoint = false;

    private Vector3 patrolCenter;
    private Vector3 lastWanderPoint;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 0f;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component missing!");
            enabled = false;
            return;
        }

        if (player == null)
        {
            Debug.LogWarning("Player not found. Enemy will only patrol.");
        }

        if (player != null)
        {
            Vector3 forwardDir = (player.position - transform.position).normalized;
            Vector3 initialPatrolCenter = transform.position;
            Vector3 randomDestination = GetRandomNavMeshLocation(initialPatrolCenter, forwardDir);
            agent.SetDestination(randomDestination);
        }
        else
        {
            Vector3 forwardDir = transform.forward;
            Vector3 initialPatrolCenter = transform.position;

            Vector3 randomDestination = GetRandomNavMeshLocation(initialPatrolCenter, forwardDir);
            agent.SetDestination(randomDestination);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (player != null)
        {
            patrolCenter = Vector3.MoveTowards(patrolCenter, player.position, driftSpeed * Time.deltaTime);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (HasLineOfSight())
            {
                hasSeenPlayer = true;
            }

            if (hasSeenPlayer)
            {
                HandleChaseAndShoot(distanceToPlayer);
                return;
            }
        }

        HandlePatrolling();
        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        if (animator!= null && agent != null)
        {
            bool iswalking = agent.velocity.sqrMagnitude > 0.1f;
            animator.SetBool("isWalking", iswalking);
        }
    }
    void HandlePatrolling()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!waitingAtPoint)
            {
                patrolWaitTimer = 0f;
                waitingAtPoint = true;
            }

            patrolWaitTimer += Time.deltaTime;

            if (patrolWaitTimer >= patrolWaitTime)
            {
                Vector3 patrolCenter = transform.position;
                Vector3 forwardDir = (player != null) ? (player.position - transform.position).normalized : transform.forward;
                Vector3 newDestination = GetRandomNavMeshLocation(patrolCenter, forwardDir);
                agent.SetDestination(newDestination);

                waitingAtPoint = false;
            }
        }
    }

    void HandleChaseAndShoot(float distanceToPlayer)
    {
        agent.SetDestination(player.position);

        if (distanceToPlayer <= shootRange && HasLineOfSight())
        {
            agent.isStopped = true;
            FacePlayer();

            if (shootTimer >= shootCoolDown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || gunBarrel == null) return;

        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.AddForce(gunBarrel.forward * bulletForce, ForceMode.Impulse);
        }
    }

    bool HasLineOfSight()
    {
        Vector3 origin = gunBarrel.position;
        Vector3 direction = (player.position + Vector3.up * 0.5f - origin).normalized;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, viewRange))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    void FacePlayer()
    {
        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0f;

        if (lookDir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    Vector3 GetRandomNavMeshLocation(Vector3 center, Vector3 forward)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirInCone = RandomDirectionInCone(forward, patrolConeAngle);
            Vector3 randomPoint = center + randomDirInCone * patrolRange;

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, patrolRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }

    Vector3 RandomDirectionInCone(Vector3 forward, float maxAngleDeg)
    {
        float maxAngleRad = maxAngleDeg * Mathf.Deg2Rad;
        float angle = Random.Range(0f, maxAngleRad);
        float randRot = Random.Range(0f, 2f * Mathf.PI);

        Quaternion rot = Quaternion.LookRotation(forward);
        Vector3 localDir = new Vector3(Mathf.Sin(angle) * Mathf.Cos(randRot), Mathf.Sin(angle) * Mathf.Sin(randRot), Mathf.Cos(angle));
        Vector3 worldDir = rot * localDir;

        return worldDir;
    }
    void OnDrawGizmos()
    {
        if (agent == null) agent = GetComponent<NavMeshAgent>();
        if (player == null) player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (agent == null) return;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        Vector3 forwardDir = (player != null) ? (player.position - origin).normalized : transform.forward;

        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);

        int segments = 36;
        float angleStep = patrolConeAngle * 2f / segments;
        Vector3 lastPoint = Vector3.zero;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -patrolConeAngle + angleStep * i;
            Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 dir = rot * forwardDir;

            Vector3 point = origin + dir * patrolRange;

            Gizmos.DrawLine(origin, point);
            if (i > 0)
                Gizmos.DrawLine(lastPoint, point);

            lastPoint = point;
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(agent.destination + Vector3.up * 0.2f, 0.3f);

        Gizmos.color = new Color(0f, 0.5f, 1f, 0.75f);
        Gizmos.DrawSphere(origin, viewRange);
    }

}
