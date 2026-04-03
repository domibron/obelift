using System;
using System.Collections;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public event Action OnMoveFinished;

    protected GameManager gameManager;

    // protected Coroutine attackCoroutine;

    public abstract void StartMove();
    // {
    //     if (attackCoroutine == null)
    //     {
    //         attackCoroutine = StartCoroutine(MoveAttack());
    //     }
    // }

    // protected IEnumerator MoveAttack()
    // {
    //     yield return null;

    //     AttackEnd();
    // }

    protected virtual void AttackEnd()
    {
        OnMoveFinished?.Invoke();
    }
    // {
    //     attackCoroutine = null;
    //     OnMoveFinished?.Invoke();
    // }

    public virtual void SetUpEnemy(GameManager gManager)
    {
        gameManager = gManager;
    }
}
