using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AIController;
using static UnityEngine.GraphicsBuffer;

public class SlackerAIController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        //Starts by scanning, because his supervisor is watching him for the first second of the battle or something. Wow, I feel bad for this guy now.
        ChangeAIState(AIState.Scan);
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
    { //Slacker will do the bare minimum. Chasing the player for a second, shooting, and then idling for longer than necessary. Could be deadly if he applied himself, but doesn't have the motivation. Wait, this hits too close to home.
        switch (currentState)
        {
            case AIState.Idle:
                DoIdleState();
                if (IsTimePassed(5))
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
                            ChangeAIState(AIState.Chase);
                        }
                    }
                    else
                    {
                        ChangeAIState(AIState.Idle);
                    }
                }
                else
                {
                    ChangeAIState(AIState.Idle);
                }

                break;
            case AIState.Attack:
                DoAttackState();
                ChangeAIState(AIState.Idle);
                break;
            case AIState.Chase:
                DoChaseState();
                if (IsTimePassed(1))
                {
                    ChangeAIState(AIState.Attack);
                }
                break;
        }
    }
}
