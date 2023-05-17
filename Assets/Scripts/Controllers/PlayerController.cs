using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankPawn))]
public class PlayerController : Controller
{
    private TankPawn playerPawn;
    public KeyCode forwardKey;
    public KeyCode backwardKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    // Start is called before the first frame update
    public override void Start()
    {
        playerPawn = GetComponent<TankPawn>();
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
            playerPawn.MoveForward();
        }
        if (Input.GetKey(backwardKey))
        {
            playerPawn.MoveBackward();
        }
        if (Input.GetKey(leftKey))
        {
            playerPawn.Rotate(-1f);
        }
        if (Input.GetKey(rightKey))
        {
            playerPawn.Rotate(1f);
        }
    }
}
