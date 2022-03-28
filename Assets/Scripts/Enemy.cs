using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int hitPoint = 2;
    [SerializeField] int scoreAmountOfEnemy = 10;

    [Header("Explosion Effect Settings")]
    [SerializeField] GameObject enemyExplosionVFX;
    [SerializeField] GameObject hitExplosionVFX;
    [SerializeField] Transform parent;
    [SerializeField] float timeTillDestroy = 2f;

    ScoreBoard scoreBoard;

    void Awake()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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
        scoreBoard.IncreaseScore(scoreAmountOfEnemy);
        hitPoint--;

        GameObject hitVFX = Instantiate(hitExplosionVFX, transform.position, Quaternion.identity, parent);
        Destroy(hitVFX.gameObject, timeTillDestroy);
    }

    void KillEnemy()
    {
        GameObject expVFX = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity, parent);
        Destroy(expVFX.gameObject, timeTillDestroy);
        Destroy(gameObject);
    }
}
