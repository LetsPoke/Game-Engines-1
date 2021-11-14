using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private AudioSource sound;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sound = player.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("breakable")) {
            collision.GetComponent<pot>().Smash();
            sound.Play();
        }
        if (collision.CompareTag("Enemy")) {
            collision.GetComponent<EnemyBase>().HitEnemy();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
