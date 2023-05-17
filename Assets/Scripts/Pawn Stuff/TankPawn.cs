using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private const float forwardDirection = 1f;
    private const float backwardDirection = -1f;
    public float forwardSpeed = 5f;
    public float backwardSpeed = 3f;
    
    // Start is called before the first frame update
    public override void Start()
    {
        mover = GetComponent<TankMover>();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void MoveForward()
    {
        base.MoveForward();
        mover.Move(forwardSpeed, forwardDirection);
    }

    public override void MoveBackward()
    {
        base.MoveBackward();
        mover.Move(backwardSpeed, backwardDirection);
    }

    public override void Rotate(float direction)
    {
        base.Rotate(direction);
        Debug.Log("Reorient!");
    }
}
