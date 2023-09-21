using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState {Idle, Chase, Flee, Patrol, Attack, Scan, Turn};
    public AIState currentState = AIState.Chase;
    private float lastStateChangeTime = 0f;
    public GameObject target = null;
    public float hearingDistance;
    public List<Transform> waypoints;
    public int currentWaypoint = 0;
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

    public virtual void MakeDecisions()
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
                if (IsTimePassed(3))
                {
                    ChangeAIState(AIState.Attack);
                }
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
                ChangeAIState(AIState.Flee);
                //do state behavior
                //check for transitions
                break;
            case AIState.Scan:
                DoScanState();
                ChangeAIState(AIState.Chase);
                //do state behavior
                //check for transitions
                break;
            case AIState.Turn:
                DoTurnState();
                //do state behavior
                //check for transitions
                break;
            default:
                Debug.LogWarning("What am I even trying to do here?");
                break;
        }
    }

    public void DoTurnState()
    {
        pawn.RotateTowards(target.transform.position);
    }

    public void DoScanState()
    {
        //if the GameManager exists
        if (GameManager.instance != null)
        {
            //if the list of players is real
            if (GameManager.instance.players != null)
            {
                //if there's players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //get nearest player
                    target = GetNearestPlayer();
                }
            }
        }
    }

    public void DoAttackState()
    {
        pawn.Shoot();
    }

    public void DoPatrolState()
    {
        //Get the point's location
        Vector3 tempTargetLocation = waypoints[currentWaypoint].position;
        tempTargetLocation = new Vector3(tempTargetLocation.x, pawn.transform.position.y, tempTargetLocation.z);
        //move there
        pawn.RotateTowards(tempTargetLocation);
        pawn.MoveForward();
        //once we're there, move on to the next
        if (Vector3.Distance(pawn.transform.position, tempTargetLocation) <= 1)
        {
            currentWaypoint++;
        }
        //once we reach the end of the list, loop to the beginning
        if (currentWaypoint >= waypoints.Count)
        {
            currentWaypoint = 0;
        }

    }

    public void DoFleeState()
    {
        if (target != null)
        {
            //turn to target
            pawn.RotateAway(target.transform.position);
            //charge forward
            pawn.MoveForward();

        }
    }

    public void DoChaseState()
    {
        if (target != null)
        {
            //turn to target
            pawn.RotateTowards(target.transform.position);
            //charge forward
            pawn.MoveForward();

        }
        
    }

    public void DoIdleState()
    {
        //i'm too good at this state irl lol
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }

    public virtual GameObject GetNearestPlayer()
    {
        //Assume that p1 is closest to begin with
        GameObject nearestPlayer = GameManager.instance.players[0].pawn.gameObject;
        //get distance
        float nearestPlayerDistance = Vector3.Distance(pawn.transform.position, nearestPlayer.transform.position);
        //check to see if any other player is closer
        for (int index = 1; index <GameManager.instance.players.Count; index++)
        {
            float tempDistance = Vector3.Distance(pawn.transform.position, GameManager.instance.players[index].transform.position);
            if (tempDistance < nearestPlayerDistance)
            {
                //if the next player is closer, set them as the closest
                nearestPlayer = GameManager.instance.players[index].pawn.gameObject;
                nearestPlayerDistance = tempDistance;
            }
        }
        return nearestPlayer;

    }

    public virtual bool IsTimePassed(float amountOfTime)
    {
        if (Time.time - lastStateChangeTime >= amountOfTime)
        {
            return true;
        }
        return false;
    }

    public void OnDestroy()
    {
        GameManager.instance.enemies.Remove(this);
    }

    public bool IsCanHear(GameObject target)
    {
        //get target's noisemaker
        Noisemaker noiseMaker = target.GetComponent<Noisemaker>();
        if (noiseMaker == null)
        {
            //if no noise maker, no noise being made, can't hear noise if noise isn't being made
            return false;
        }
        if (noiseMaker.noiseDistance <= 0)
        {
            //we can't hear silence, that's why it's silence
            return false;
        }
        //if they are making noise, add up the noise and the hearing distance
        float totalDistance = noiseMaker.noiseDistance + hearingDistance;
        //if we're closer to the target than this, we can hear them.
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }
        else
        {
            // if we're further than this, we can't hear them
            return false;
        }
    }
}
