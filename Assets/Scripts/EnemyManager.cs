using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    List<EnemyBase> enemies = new List<EnemyBase>();

    public event Action OnAllAttacksFinished;

    [SerializeField]
    GameManager gameManager;

    Transform towerTrans;
    CameraTarget cameraTarget;

    Coroutine attackCoroutine;

    float minDist = 15f;
    float maxDist = 30f;

    float minAngle = -65f;
    float maxAngle = 65f;

    [SerializeField]
    GameObject[] enemyPrefabs;

    bool firstTime = true;

    bool enemyAttackFinished = false;

    void Start()
    {
        towerTrans = gameManager.TowerMovemnet.transform;
        cameraTarget = gameManager.CameraTarget;

    }

    public void StartAttacks()
    {
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCycle());
        }
    }

    IEnumerator AttackCycle()
    {
        // Spawn enemy

        if (firstTime)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2f);

            SpawnEnemy();
            yield return new WaitForSeconds(2f);

            SpawnEnemy();
            yield return new WaitForSeconds(2f);

            firstTime = false;
        }
        else
        {
            if (UnityEngine.Random.Range(0f, 1f) < 0.3f)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(2f);
            }

        }

        // cycle through attacks.
        // ? could we exclude the enemies that just spawned please.

        foreach (var enemy in enemies)
        {
            enemyAttackFinished = false;
            enemy.OnMoveFinished += EnemyConcludedAttack;
            cameraTarget.Target = enemy.transform;

            enemy.StartMove();
            while (!enemyAttackFinished)
            {
                yield return new WaitForEndOfFrame();
            }

            enemy.OnMoveFinished -= EnemyConcludedAttack;
        }

        yield return null;
        EndAttack();
    }

    private void EnemyConcludedAttack()
    {
        enemyAttackFinished = true;
    }

    private void SpawnEnemy()
    {
        Vector3 dir = (Vector3.forward + Vector3.right).normalized; // 0.76, 0, 0.76

        Vector3 spawnPos = towerTrans.position + ((Quaternion.AngleAxis(UnityEngine.Random.Range(minAngle, maxAngle), Vector3.up) * dir * UnityEngine.Random.Range(minDist, maxDist)));

        spawnPos = gameManager.GridSystem.GetPosAsGridWorld(spawnPos);

        GameObject enemySpawnedObject = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length - 1)], spawnPos, Quaternion.identity);

        EnemyBase eBase = enemySpawnedObject.GetComponent<EnemyBase>();

        eBase.SetUpEnemy(gameManager);

        enemies.Add(eBase);

        cameraTarget.Target = enemySpawnedObject.transform;
    }

    private void EndAttack()
    {
        attackCoroutine = null;
        OnAllAttacksFinished?.Invoke();
    }



}
