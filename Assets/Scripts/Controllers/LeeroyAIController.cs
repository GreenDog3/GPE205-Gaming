using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeeroyAIController : AIController
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
        else //if the LEEROOOOOOOOOY JENKINNNNNNNS gambit doesn't quite play out
        {
            Destroy(this);
        }
    }

    public override void MakeDecisions()
    {
        //Leeroy is named after Leeroy Jenkins. I think that's all I need to say.
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
                            ChangeAIState(AIState.Chase);
                        }
                    }
                    else
                    {
                        ChangeAIState(AIState.Attack);
                    }
                }
                else
                {
                    ChangeAIState(AIState.Idle);
                }

                break;

            case AIState.Chase:
                DoChaseState();
                if (IsTimePassed(2))
                {
                    ChangeAIState(AIState.Attack);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                ChangeAIState(AIState.Scan);
                break;
        }
    }
}
