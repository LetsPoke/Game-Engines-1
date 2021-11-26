using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    private AudioSource sound;
    private GameObject player;
    private GameObject health;
    private Collider2D co2;

    //private GameObject scoreobjekt;
    //public int score = 0;
    //[SerializeField] public Text scoreText;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GameObject.FindGameObjectWithTag("healthItem");
        //scoreobjekt = GameObject.FindGameObjectWithTag("Score");

        sound = player.GetComponent<AudioSource>();
        co2 = GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(co2, player.GetComponent<Collider2D>(), true);
        //Physics2D.IgnoreCollision(co2, health.GetComponent<Collider2D>(), true);

        //scoreText.text = "Score: " + score;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("breakable")) {
            collision.GetComponent<pot>().Smash();
            //score++;
            //scoreText.text = "Score: " + score;
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
