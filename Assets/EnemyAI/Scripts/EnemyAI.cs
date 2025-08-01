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

    [Header("Shooting Settings")]
    public float bulletForce = 20f;
    public GameObject bulletPrefab;
    public Transform gunBarrel;
    public float shootCooldown = 1.5f;
    public float rotationSpeed = 720f;

    [Header("Animation")]
    public float speedSmoothTime = 0.1f;

    [Header("Chase Behavior")]
    public float stopDistanceFromPlayer = 7f;

    [Header("Prediction Settings")]
    public float leadTimeMultiplier = 1.2f;
    public float velocityMultiplier = 1.0f;
    public float minPredictionDistance = 1.5f;
    public float maxPredictionDistance = 30f;

    private NavMeshAgent agent;
    private Transform player;


    private float patrolWaitTimer = 0f;
    private float shootTimer = 0f;

    private bool hasSeenPlayer = false;
    private bool waitingAtPoint = false;

    private Vector3 patrolCenter;
    private Vector3 lastPlayerPos;
    private Vector3 estimatedPlayerVelocity;

    private Animator animator;
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
            patrolCenter = transform.position;
        }
        else
        {
            playerRb = player.GetComponent<Rigidbody>();
            patrolCenter = transform.position;
            lastPlayerPos = player.position;
        }

        Vector3 forwardDir = (player != null) ? (player.position - transform.position).normalized : transform.forward;
        Vector3 randomDestination = GetRandomNavMeshLocation(patrolCenter, forwardDir);
        agent.SetDestination(randomDestination);

        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (player == null || !player.gameObject.activeInHierarchy)
        {
            hasSeenPlayer = false;
            HandlePatrolling();
            UpdateAnimation();
            return;
        }

        if (player != null)
        {
            Vector3 currentPos = player.position;
            estimatedPlayerVelocity = (currentPos - lastPlayerPos) / Time.deltaTime;
            lastPlayerPos = currentPos;
        }
        Vector3 rawVelocity = (player.position - lastPlayerPos) / Time.deltaTime;
        estimatedPlayerVelocity = Vector3.Lerp(estimatedPlayerVelocity, rawVelocity, Time.deltaTime * 5f);
        lastPlayerPos = player.position;

        patrolCenter = Vector3.MoveTowards(patrolCenter, player.position, driftSpeed * Time.deltaTime);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (HasLineOfSight())
        {
            hasSeenPlayer = true;
        }

        if (hasSeenPlayer)
        {
            HandleChaseAndShoot(distanceToPlayer);
        }
        else
        {
            HandlePatrolling();
        }

        UpdateAnimation();
    }
    void UpdateAnimation()
    {
        if (animator != null && agent != null)
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
                Vector3 forwardDir = (player != null) ? (player.position - transform.position).normalized : transform.forward;
                Vector3 newDestination = GetRandomNavMeshLocation(patrolCenter, forwardDir);
                agent.SetDestination(newDestination);

                waitingAtPoint = false;
            }
        }
    }

    [System.Obsolete]
    void HandleChaseAndShoot(float distanceToPlayer)
    {
        if (distanceToPlayer > stopDistanceFromPlayer)
        {
            agent.isStopped = false;
            agent.updateRotation = true;
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true);
        }
        else
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.updateRotation = false;
            animator.SetBool("isWalking", false);
        }

        if (distanceToPlayer <= shootRange && HasLineOfSight())
        {
            FacePlayer();

            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }
        }
    }

    [System.Obsolete]
    void Shoot()
    {
        if (bulletPrefab == null || gunBarrel == null || player == null) return;

        Vector3 shooterPos = gunBarrel.position;
        Vector3 targetPos = player.position + Vector3.up * 0.5f;

        Vector3 targetVelocity = playerRb != null ? playerRb.velocity : estimatedPlayerVelocity;

        float bulletMass = bulletPrefab.GetComponent<Rigidbody>()?.mass ?? 1f;
        float bulletSpeed = bulletForce / bulletMass;

        Vector3 leadTarget = PredictFuturePosition(shooterPos, targetPos, targetVelocity, bulletSpeed);
        Vector3 shootDir = (leadTarget - shooterPos).normalized;

        GameObject bullet = Instantiate(bulletPrefab, shooterPos, Quaternion.LookRotation(shootDir));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.AddForce(shootDir * bulletForce, ForceMode.Impulse);
        }
    }

    Vector3 PredictFuturePosition(Vector3 shooterPos, Vector3 targetPos, Vector3 targetVelocity, float bulletSpeed)
    {
        Vector3 toTarget = targetPos - shooterPos;
        float distanceToTarget = toTarget.magnitude;

        if (distanceToTarget < minPredictionDistance || distanceToTarget > maxPredictionDistance)
            return targetPos;

        float timeToReach = distanceToTarget / bulletSpeed;
        Vector3 leadOffset = targetVelocity * timeToReach * leadTimeMultiplier * velocityMultiplier;

        return targetPos + leadOffset;
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
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

        Gizmos.color = new Color(0f, 0.5f, 1f, 0f);
        Gizmos.DrawSphere(origin, viewRange);
    }
}
