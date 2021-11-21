using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : EnemyBase
{
    private Animator anim;
    public Transform player;
    private Rigidbody2D rb;

    public float chaseRadius;
    public float attackRadius; 

    private string stateMachine;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialSpawnPoint = gameObject.transform.position;
        
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 spawnDirection = initialSpawnPoint - transform.position;
        direction.Normalize();
        spawnDirection.Normalize();

        if(Vector3.Distance(player.position, transform.position) <= chaseRadius) {
            stateMachine = "chasing";
        } else if (
            (transform.position.x >= initialSpawnPoint.x + 0.5) ||
            (transform.position.x <= initialSpawnPoint.x - 0.5) ||
            (transform.position.y >= initialSpawnPoint.y + 0.5) ||
            (transform.position.y <= initialSpawnPoint.y - 0.5) ) 
        {
            stateMachine = "returning";
        } else {
            stateMachine = "sleeping";
        }

        switch(stateMachine){
            case "sleeping":
                //Anim for sleeping 
                break;
            case "chasing": 
                rb.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
                // Anim for walking and following the Player here
                break;
            case "returning":
                rb.MovePosition(transform.position + (spawnDirection * moveSpeed * Time.deltaTime));
                // Anim for going back to the spawn position here
                break;
        }

    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}