using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPhase : PhaseStateBase
{
    CameraTarget cameraTarget;
    PlayerMovement playerMovement;
    GridSystem gridSystem;

    bool hasSelected = false;


    public PlayerPhase(GameManager gManager) : base(gManager)
    {
        cameraTarget = gameManager.CameraTarget;
        playerMovement = gameManager.PlayerMovement;
        gridSystem = gameManager.GridSystem;
    }



    public override void EnterPhase()
    {
        hasSelected = false;
        cameraTarget.Target = playerMovement.transform;
        gridSystem.GenerateSelectableGrid(playerMovement.transform.position, playerMovement.MoveRange);
    }

    public override void ExitPhase()
    {

    }

    public override void PhaseTick(float deltaTime)
    {
        if (!hasSelected)
        {
            if (InputSystem.actions.FindAction("Attack").IsPressed())
            {
                if (gridSystem.CurrentSelectedTile.HasValue)
                {
                    // Debug.Log(Vector3.Distance(gridSystem.CurrentSelectedTile.Value, gridSystem.GetPosAsGridWorld(playerMovement.transform.position)));

                    if (Vector3.Distance(gridSystem.CurrentSelectedTile.Value, gridSystem.GetPosAsGridWorld(playerMovement.transform.position)) < GameManager.MIN_MOVE_DIST)
                    {
                        gameManager.ChangePhase(Phase.EnemiesMove);
                        return;
                    }
                    else if (!gridSystem.CheckGridIsEmpty(gridSystem.CurrentSelectedTile.Value, Constants.GRID_BLOCKER_LAYER))
                    {
                        Debug.Log("Failed to move, its blocked");
                        return;
                    }

                    playerMovement.MoveTo(gridSystem.CurrentSelectedTile.Value);
                    gridSystem.ClearSelectableGrid();
                    hasSelected = true;
                }
            }
        }
        else
        {
            if (playerMovement.HasReachedDestination())
            {
                gameManager.ChangePhase(Phase.EnemiesMove);

            }
        }
    }
}
