using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SniperAIController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        //Starts by idling
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
        else //if we get outsniped
        {
            Destroy(this);
        }
    }

    public override void MakeDecisions()
    { //Sniper will flee from the player, and then turn and shoot.
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
                            ChangeAIState(AIState.Flee);
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(pawn.transform.position, homeBase.position) <= 3)
                        {
                            ChangeAIState(AIState.Idle);
                        }
                        else
                        {
                            ChangeAIState(AIState.Return);
                        }
                        
                    }
                }
                else
                {
                    ChangeAIState(AIState.Idle);
                }

                break;

            case AIState.Flee:
                DoFleeState();
                if (IsTimePassed(5))
                {
                    ChangeAIState(AIState.Turn);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                ChangeAIState(AIState.Scan);
                break;
            case AIState.Turn:
                DoTurnState();
                if(IsTimePassed(5))
                {
                    ChangeAIState(AIState.Attack);
                }
                break;
            case AIState.Return:
                DoReturnState();
                if (IsTimePassed(2))
                {
                    ChangeAIState(AIState.Scan);

                }
                break;
        }
    }
}