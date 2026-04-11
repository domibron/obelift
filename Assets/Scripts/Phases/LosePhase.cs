using UnityEngine;

public class LosePhase : PhaseStateBase
{
    public LosePhase(GameManager gManager) : base(gManager)
    {
    }

    public override void EnterPhase()
    {
        gameManager.InGameUI.ShowLoseScreen();

    }

    public override void ExitPhase()
    {

    }

    public override void PhaseTick(float deltaTime)
    {

    }
}
