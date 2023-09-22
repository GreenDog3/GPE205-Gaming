using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAIController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        //starts by idling
        ChangeAIState(AIState.Idle);
        GameManager.instance.enemies.Add(this);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (pawn != null) //if we are alive
        {
            MakeDecisions();
        }
        else //if we let our guard down
        {
            Destroy(this);
        }
    }

    public override void MakeDecisions()
    {
        //The Guard patrols the waypoints and turns and shoots the player if it hears them

        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsTimePassed(1))
                {
                    ChangeAIState(AIState.Scan);
                    
                }
                break;
            case AIState.Scan:
                DoScanState();
                if (target != null)
                {
                    if (IsCanHear(target))
                    {
                        if (target != null)
                        {
                            ChangeAIState(AIState.Turn);
                        }
                    }
                    else
                    {
                        ChangeAIState(AIState.Patrol);
                    }
                }
                else
                {
                    ChangeAIState(AIState.Idle);
                }
                break;
            case AIState.Patrol:
                DoPatrolState();
                if (IsTimePassed(1))
                {
                    if (target != null)
                    {
                        if (IsCanHear(target))
                        {
                            if (target != null)
                            {
                                ChangeAIState(AIState.Turn);
                            }
                        }
                        else
                        {
                            ChangeAIState(AIState.Scan);
                        }
                    }
                    else
                    {
                        ChangeAIState(AIState.Idle);
                    }
                    
                    
                }
                break;
            case AIState.Turn: 
                DoTurnState();
                if (IsTimePassed(3))
                {
                    ChangeAIState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                ChangeAIState(AIState.Patrol);
                break;
        }
    }
}

