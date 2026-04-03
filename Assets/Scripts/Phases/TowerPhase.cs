using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPhase : PhaseStateBase
{
    TowerMovemnet towerMovemnet;
    GridSystem gridSystem;
    CameraTarget cameraTarget;

    bool hasSelected = false;



    public TowerPhase(GameManager gManager) : base(gManager)
    {
        towerMovemnet = gameManager.TowerMovemnet;
        gridSystem = gameManager.GridSystem;
        cameraTarget = gameManager.CameraTarget;
    }


    public override void EnterPhase()
    {
        hasSelected = false;
        cameraTarget.Target = towerMovemnet.transform;
        gridSystem.GenerateSelectableGrid(towerMovemnet.transform.position, towerMovemnet.TowerRange);
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
                    if (Vector3.Distance(gridSystem.CurrentSelectedTile.Value, gridSystem.GetPosAsGridWorld(towerMovemnet.transform.position)) < GameManager.MIN_MOVE_DIST)
                    {
                        gameManager.ChangePhase(Phase.PlayerMove);
                        return;
                    }
                    else if (!gridSystem.CheckGridIsEmpty(gridSystem.CurrentSelectedTile.Value, Constants.GRID_BLOCKER_LAYER))
                    {
                        // Debug.DrawLine(gridSystem.CurrentSelectedTile.Value, gridSystem.CurrentSelectedTile.Value + Vector3.up, Color.cyan, 10f);
                        Debug.Log("Failed to move, its blocked");
                        return;
                    }

                    towerMovemnet.MoveTo(gridSystem.CurrentSelectedTile.Value);
                    gridSystem.ClearSelectableGrid();
                    hasSelected = true;
                }
            }
        }
        else
        {
            if (towerMovemnet.HasReachedDestination())
            {
                gameManager.ChangePhase(Phase.PlayerMove);

            }
        }
    }


}
