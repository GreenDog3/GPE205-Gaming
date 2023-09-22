using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    protected Mover mover;
    protected Shooter shooter;
    public Controller controller;
    // Start is called before the first frame update
    public abstract void Start();

    // Update is called once per frame
    public abstract void Update();

    public abstract void MoveForward();

    public abstract void MoveBackward();
    public abstract void Rotate(float direction);

    public abstract void Shoot();

    public abstract void RotateTowards(Vector3 targetPosition);

    public abstract void RotateAway(Vector3 targetPosition);
}
