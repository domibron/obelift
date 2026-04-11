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
        enemyManager.StopAllCoroutines(); // TODO: This is to stop enemies from moving once the tower is dead.
    }

    public override void PhaseTick(float deltaTime)
    {

    }

    private void AttackedFinished()
    {
        gameManager.ChangePhase(Phase.MoveTower);
    }
}
