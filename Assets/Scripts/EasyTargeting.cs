using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EasyTargeting : MonoBehaviour
{

    [Header("AI")]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
	}

    void Start()
    {
		agent.SetAreaCost(1, 10f);
	}

    void Update()
    {
		agent.SetDestination(target.position);
		//agent.SetDestination(target.transform.position);
	}
}
