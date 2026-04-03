using System.Collections.Generic;
using UnityEngine;

public abstract class PhaseStateBase
{
    protected GameManager gameManager;

    public PhaseStateBase(GameManager gManager)
    {
        gameManager = gManager;
    }

    public abstract void EnterPhase();
    public abstract void PhaseTick(float deltaTime);
    public abstract void ExitPhase();
}

public enum Phase
{
    MoveTower,
    PlayerMove,
    EnemiesMove,
}

public class GameManager : MonoBehaviour
{
    Dictionary<Phase, PhaseStateBase> phaseData;

    public Phase CurrentPhase = Phase.MoveTower;


    public GridSystem GridSystem;

    public TowerMovemnet TowerMovemnet; // can simplify by having the phase system consume / store these.

    public PlayerMovement PlayerMovement;

    public CameraTarget CameraTarget;

    public EnemyManager EnemyManager;

    public const float MIN_MOVE_DIST = 0.01f;

    bool phaseChangeOccured = false;

    void Awake()
    {
        InitPhases();
    }

    void Start()
    {
        CallEnterPhase(CurrentPhase); // so it can init.
    }

    void Update()
    {
        if (phaseChangeOccured)
        {
            phaseChangeOccured = false; // dunno if this will work but this is to prevent a update call when the phase is exiting.
            return;
        }


        CallPhaseTick(CurrentPhase);
    }

    void InitPhases()
    {
        phaseData = new Dictionary<Phase, PhaseStateBase>()
        {
            {Phase.MoveTower, new TowerPhase(this)},
            {Phase.PlayerMove, new PlayerPhase(this)},
            {Phase.EnemiesMove, new EnemyPhase(this)},
        };
    }

    public void ChangePhase(Phase newPhase)
    {
        phaseChangeOccured = true;
        CallExitPhase(CurrentPhase);
        CallEnterPhase(newPhase);

        // print($"Phase has changed! {CurrentPhase} to {newPhase}");
        CurrentPhase = newPhase;
    }

    bool CallEnterPhase(Phase phase)
    {
        if (!phaseData.ContainsKey(phase)) return false;

        phaseData[phase].EnterPhase();

        return true;
    }

    bool CallPhaseTick(Phase phase)
    {
        if (!phaseData.ContainsKey(phase)) return false;

        phaseData[phase].PhaseTick(Time.deltaTime);

        return true;
    }

    bool CallExitPhase(Phase phase)
    {
        if (!phaseData.ContainsKey(phase)) return false;

        phaseData[phase].ExitPhase();

        return true;
    }
}
