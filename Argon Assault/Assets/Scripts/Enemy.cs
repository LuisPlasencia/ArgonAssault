using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    GameObject parentGameObject;    
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 4;

    ScoreBoard scoreBoard;

    void Awake()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();    // we only have 1 scoreboard (findobjectsoftype for multiple)
        parentGameObject =  GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log($"{name} has been hit by {other.gameObject.name}");
        ProcessHit();
        if(hitPoints < 1)
        {
            KillEnemy();
        }

    }

    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);  // quaternion identity = no rotation
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);  // quaternion identity = no rotation
        fx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
