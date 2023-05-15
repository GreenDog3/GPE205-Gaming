using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void MoveForward()
    {
        base.MoveForward();
        Debug.Log("Onwards!");
    }

    public override void MoveBackward()
    {
        base.MoveBackward();
        Debug.Log("Retreat!");
    }

    public override void Rotate(float direction)
    {
        base.Rotate(direction);
        Debug.Log("Reorient!");
    }
}
