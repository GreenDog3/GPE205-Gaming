using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TankMover : Mover
{

    private Rigidbody tankRigidbody;

    private void Start()
    {
        tankRigidbody = GetComponent<Rigidbody>();
    }
    public override void Move(float moveSpeed, float direction)
    {
        base.Move(moveSpeed, direction);
        Vector3 currentPosition = transform.position;
        tankRigidbody.MovePosition(currentPosition + (transform.forward * direction * moveSpeed * Time.deltaTime));
    }

    public override void Rotate(float turnSpeed)
    {                                     //take note, lego island devs, THIS is how you unlock a turn speed from the framerate
        tankRigidbody.transform.Rotate(0, turnSpeed*Time.deltaTime, 0);
    }
}
