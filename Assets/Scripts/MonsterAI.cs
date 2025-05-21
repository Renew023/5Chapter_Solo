using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering,
    Attacking
}

public class MonsterAI : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int health;
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;

    [Header("AI")]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float detectDistance;
    [SerializeField]
    private AIState aiState;

    [Header("Wandering")]
    [SerializeField]
    private float minWanderDistance;
    [SerializeField]
    private float maxWanderDistance;
    [SerializeField]
    private float minWanderWaitTime;
    [SerializeField]
    private float maxWanderWaitTime;

    [Header("Combat")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private float attackRate;
	private float lastAttackTime;
    [SerializeField]
    private float attackDistance;

    private float playerDistance;

    [SerializeField]
    private float fieldOfView = 150;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
	}

    void Start()
    {
        SetState(AIState.Idle);
    }

    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, target.transform.position);

        switch (aiState)
        {
            case AIState.Idle:
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    public void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }
    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
			Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
		}

        if (playerDistance < detectDistance) // 플레어와의 거리가 디텍트 사거리보다 짧아지면 공격한다.
        {
            SetState(AIState.Attacking);
        }
    }

    //좌표 지정해주기
    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;

        SetState(AIState.Wandering);
		agent.SetDestination(GetWanderLocation());
	}

    //랜덤하게 돌아다닐 위치 받아오기 - 
    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        int i = 0;

        NavMesh.SamplePosition(
            transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance,
            maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);


        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
			NavMesh.SamplePosition(
			transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance,
			maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            if (i == 30) break;
		}

        return hit.position;
    }

	void AttackingUpdate()
	{
		if (playerDistance < attackDistance && IsPlayerInFieldOfView())
		{
			agent.isStopped = true;
			if (Time.time - lastAttackTime > attackRate)
			{
				lastAttackTime = Time.time;
				//딜 넣기
			}
		}
		else
		{
			if (playerDistance < detectDistance)
			{
				agent.isStopped = false;
				NavMeshPath path = new NavMeshPath();
				if (agent.CalculatePath(target.transform.position, path))
				{
					agent.SetDestination(target.transform.position);
				}
				else
				{
					agent.SetDestination(transform.position);
					agent.isStopped = true;
					SetState(AIState.Wandering);
				}
			}
			else
			{
				agent.SetDestination(transform.position);
				agent.isStopped = true;
				SetState(AIState.Wandering);
			}
		}
	}
	bool IsPlayerInFieldOfView()
	{
		Vector3 directionToPlayer = target.transform.position - transform.position;
		float angle = Vector3.Angle(transform.forward, directionToPlayer);
		return angle < fieldOfView * 0.5f;
	}
}
