using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AImove : MonoBehaviour
{
    public List<Transform> targets = new List<Transform>();
    public float baseSpeed = 10f;
    public float minSpeed = 3f;
    public float maxSpeed = 10f;
    public float randomOffsetRange = 1.5f;
    public float arrivalThreshold = 3.0f;

    private NavMeshAgent agent;
    private int currentTargetIndex = 0;
    private bool isNearTarget = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed;
        agent.stoppingDistance = 0;
        agent.autoBraking = false;

        if (targets.Count > 0)
        {
            MoveToNextTarget();
        }
    }

    void Update()
    {
        if (targets.Count == 0) return;

        float remainingDist = agent.remainingDistance;
        
        if (!agent.pathPending)
        {
            if (remainingDist > arrivalThreshold)
            {
                agent.speed = baseSpeed;
                isNearTarget = false;
            }
            else if (remainingDist > 1.0f)
            {
                AdjustSpeedBasedOnTurn();
                isNearTarget = true;
            }
            else if (isNearTarget)
            {
                FaceNextTarget();
                agent.speed = baseSpeed;
                isNearTarget = false;
            }

            if (remainingDist < 1.0f)
            {
                MoveToNextTarget();
            }
        }
    }

    void MoveToNextTarget()
    {
        if (targets.Count == 0) return;

        Vector3 randomOffset = Random.insideUnitSphere * randomOffsetRange;
        randomOffset.y = 0;
        Vector3 targetPosition = targets[currentTargetIndex].position + randomOffset;

        agent.SetDestination(targetPosition);
        currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
    }

    void AdjustSpeedBasedOnTurn()
    {
        if (targets.Count == 0 || agent.velocity.magnitude < 0.1f) return;

        Vector3 currentDirection = agent.velocity.normalized;
        Vector3 targetDirection = (targets[currentTargetIndex].position - transform.position).normalized;

        float angle = Vector3.Angle(currentDirection, targetDirection);
        float speedFactor = Mathf.Clamp01(1 - (angle / 180f));

        agent.speed = Mathf.Lerp(minSpeed, maxSpeed, speedFactor);
    }

    void FaceNextTarget()
    {
        Vector3 targetDir = (targets[currentTargetIndex].position - transform.position).normalized;
        if (targetDir.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(targetDir);
        }
    }
}