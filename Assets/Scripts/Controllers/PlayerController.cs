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
        GameManager.instance.p1Score = score;
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
        if (GameManager.instance != null)
        {
            if (GameManager.instance.enemies.Count == 0)
            {
                GameManager.instance.ActivateGameOverState(true);
            }
        }
    }

    public override void AddToScore(int points)
    {
        score += points;
        GameManager.instance.TryGameOver();
    }

    public void OnDestroy()
    {   
        //removes life
        GameManager.instance.p1Lives = GameManager.instance.p1Lives - 1;
        //checks for game over
        GameManager.instance.TryGameOver();
        //if the player is dead, they shouldn't be in the list of alive players
        GameManager.instance.players.Remove(this);
    }
}
