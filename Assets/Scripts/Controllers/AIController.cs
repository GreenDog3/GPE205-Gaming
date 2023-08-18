using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState {Idle, Chase, Flee, Patrol, Attack, Scan, ReturnToPost};
    public AIState currentState = AIState.Chase;
    private float lastStateChangeTime = 0f;
    public GameObject target = null;
    public override void Start()
    {
        base.Start();
        pawn = GetComponent<Pawn>();
        GameManager.instance.enemies.Add(this);
    }

    public override void Update()
    {
        MakeDecisions();
    }

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                //do state behavior
                //check for transitions
                break;
            case AIState.Chase:
                DoChaseState();
                //do state behavior
                //check for transitions
                break;
            case AIState.Flee:
                DoFleeState();
                //do state behavior
                //check for transitions
                break;
            case AIState.Patrol:
                DoPatrolState();
                //do state behavior
                //check for transitions
                break;
            case AIState.Attack:
                DoAttackState();
                //do state behavior
                //check for transitions
                break;
            case AIState.Scan:
                DoScanState();
                //do state behavior
                //check for transitions
                break;
            case AIState.ReturnToPost:
                DoReturnToPostState();
                //do state behavior
                //check for transitions
                break;
            default:
                Debug.LogWarning("What am I even trying to do here?");
                break;
        }
    }

    private void DoReturnToPostState()
    {
        //throw new NotImplementedException();
    }

    private void DoScanState()
    {
        //throw new NotImplementedException();
    }

    private void DoAttackState()
    {
        //throw new NotImplementedException();
    }

    private void DoPatrolState()
    {
        //throw new NotImplementedException();
    }

    private void DoFleeState()
    {
        //throw new NotImplementedException();
    }

    private void DoChaseState()
    {
        //turn to target
        pawn.RotateTowards(target.transform.position);
        pawn.MoveForward();
        //throw new NotImplementedException();
    }

    private void DoIdleState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}
