using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int hitPoint = 2;
    [SerializeField] int scoreAmountOfEnemy = 10;

    [Header("Explosion Effect Settings")]
    [SerializeField] GameObject enemyExplosionFX;
    [SerializeField] GameObject hitExplosionVFX;
    [SerializeField] float timeTillDestroy = 2f;

    ScoreBoard scoreBoard;
    GameObject parent;

    void Awake()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindWithTag("SpawnAtRuntime");
    }

    void Start()
    {
        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoint < 1)
        {
            KillEnemy();
        }
    }

    void ProcessHit()
    {
        hitPoint--;
        GameObject hitVFX = Instantiate(hitExplosionVFX, transform.position,
                                        Quaternion.identity, parent.transform);
        Destroy(hitVFX.gameObject, timeTillDestroy);
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scoreAmountOfEnemy);
        GameObject expVFX = Instantiate(enemyExplosionFX, transform.position,
                                        Quaternion.identity, parent.transform);
        Destroy(expVFX.gameObject, timeTillDestroy);
        Destroy(gameObject);
    }
}
