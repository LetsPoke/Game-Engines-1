using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private AudioSource sound;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sound = player.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("breakable")) {
            other.GetComponent<pot>().Smash();
            sound.Play();
        }

        if (other.CompareTag("Enemy")) {
            other.GetComponent<EnemyBase>().HitEnemy();
        }
    }
}
