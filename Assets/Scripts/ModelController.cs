using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ModelController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private readonly int ForwardId = Animator.StringToHash("Forward");

    public List<Transform> points;
    public int index = 0;

    public Vector3 startingPos;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.SetDestination(points[0].position);

        startingPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameRunning)
        {
            anim.SetFloat(ForwardId, agent.velocity.magnitude);

            if ((transform.position - points[index].position).magnitude < 0.25f)
            {
                if (++index >= points.Count)
                    index = 0;
                agent.SetDestination(points[index].position);

            }
        }
        
    }
}
