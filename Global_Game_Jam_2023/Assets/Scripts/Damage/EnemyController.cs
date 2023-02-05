using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] SpriteRenderer spriteRenderer;
    NavMeshAgent _agent;
    BehaviorTree _behaviorTree;
    private void Start()
    {
        _behaviorTree = GetComponent<BehaviorTree>();
        _behaviorTree.SetVariableValue("Path", path.PointList);

        _agent = GetComponent<NavMeshAgent>();
        _behaviorTree.SetVariableValue("Speed", _agent.speed);
        _behaviorTree.SetVariableValue("AngularSpeed", _agent.angularSpeed);
        _behaviorTree.SetVariableValue("Acceleration", _agent.acceleration);
        _behaviorTree.SetVariableValue("StoppingDistance", _agent.stoppingDistance);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }

    private void Update()
    {
        if(_agent.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(_agent.velocity.x < 0)
        {
            spriteRenderer.flipX = true;

        }
    }

}
