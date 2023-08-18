using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankPawn))]
public class PlayerController : Controller
{
    
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode shootKey; 
    // Start is called before the first frame update
    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        GameManager.instance.players.Add(this); //adds itself to the list of players
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ProcessInputs();
        base.Update();
    }

    private void ProcessInputs()
    {
        if (Input.GetKey(forwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(backwardKey))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(leftKey))
        {
            pawn.Rotate(-1f);
        }
        if (Input.GetKey(rightKey))
        {
            pawn.Rotate(1f);
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }

    public void OnDestroy()
    {
        GameManager.instance.players.Remove(this);
    }
}
