using UnityEngine;

public class EnemyPhase : PhaseStateBase
{

    EnemyManager enemyManager;

    public EnemyPhase(GameManager gManager) : base(gManager)
    {
        enemyManager = gManager.EnemyManager;
        enemyManager.OnAllAttacksFinished += AttackedFinished;
    }

    public override void EnterPhase()
    {
        enemyManager.StartAttacks();
    }

    public override void ExitPhase()
    {

    }

    public override void PhaseTick(float deltaTime)
    {

    }

    private void AttackedFinished()
    {
        gameManager.ChangePhase(Phase.MoveTower);
    }
}
