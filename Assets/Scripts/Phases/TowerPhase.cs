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
