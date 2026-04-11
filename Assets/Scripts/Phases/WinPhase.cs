using UnityEngine;

public class WinPhase : PhaseStateBase
{
    public WinPhase(GameManager gManager) : base(gManager)
    {
    }

    public override void EnterPhase()
    {
        gameManager.InGameUI.ShowWinScreen();
    }

    public override void ExitPhase()
    {

    }

    public override void PhaseTick(float deltaTime)
    {

    }
}
