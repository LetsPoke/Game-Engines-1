using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

using System.IO;
using System.Linq;
using System.Net.Mime;
using UnityEditor.VersionControl;

public enum PlayerState{
    walk,
    attack,
    interact,
    attackBow
}

public class PlayerMovement : MonoBehaviour
{
    private GameObject heart0;
    private GameObject heart1;
    private GameObject heart2;
    private GameObject heart3;
    private GameObject heart4;


    private float moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerState currentState;

    public int health;
    private int Toepfe;

    public static int score; // bad coding style but ok for 1 variable
    public Text scoreText;

    public string highscore;
    public Text highscoreText;
    private string path;
    
    public Text ToepfeCount;

    Vector2 movement;

    GameObject youDiedUI;
    public Text becauseText;
    public Text scoreT;
    private AudioSource dmgsound;
    private GameObject cam;

    private GameObject swordSoundObj;
    private AudioSource swordAttack;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        dmgsound = cam.GetComponent<AudioSource>();

        path = "Assets/Scenes/HighScore.txt";

        heart0 = GameObject.Find("Player");
        heart1 = GameObject.Find("heart (1)");
        heart2 = GameObject.Find("heart (2)");
        heart3 = GameObject.Find("heart (3)");
        heart4 = GameObject.Find("heart (4)");

        heart0.SetActive(true);
        heart1.SetActive(true);
        heart2.SetActive(true);
        heart3.SetActive(true);
        heart4.SetActive(true);

        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();

        youDiedUI = GameObject.FindGameObjectWithTag("dead");
        youDiedUI.SetActive(false);

        Toepfe = GameObject.FindGameObjectsWithTag("breakable").Length;

        score = 0;
        scoreText.text = "Score: " + score;
        DisplayHighscore();

        ToepfeCount.text = "Toepfe uebrig: " + Toepfe;

        swordSoundObj = GameObject.Find("swordAttack Sound");
        swordAttack = swordSoundObj.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        switch(health) {
            case 0: 
            heart0.SetActive(false);
            break;
            case 1: 
            heart0.SetActive(true);
            heart1.SetActive(false);
            break;
            case 2: 
            heart1.SetActive(true);
            heart2.SetActive(false);
            break;
            case 3: 
            heart2.SetActive(true);
            heart3.SetActive(false);
            break;
            case 4: 
            heart3.SetActive(true);
            heart4.SetActive(false);
            break;
            case 5: 
            heart4.SetActive(true);
            break;
        }

        scoreText.text = "Score: " + score;

        Toepfe = GameObject.FindGameObjectsWithTag("breakable").Length;
        ToepfeCount.text = "Toepfe uebrig: " + Toepfe;
        
        movement.x = Input.GetAxisRaw("Horizontal"); //input
        movement.y = Input.GetAxisRaw("Vertical");
        
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.attackBow)
        { 
            StartCoroutine(AttackCo());
        } else if(currentState == PlayerState.walk){
            UpdateAnimationAndMove();
        }

        if (Input.GetButtonDown("attackBow") && currentState != PlayerState.attack && currentState != PlayerState.attackBow)
        {
            StartCoroutine(AttackBowCo());
            moveSpeed = 0f; 
        }
        else if (currentState == PlayerState.walk)
        {
            moveSpeed = 3.75f;
            UpdateAnimationAndMove();
        }
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove() {
        if(movement != Vector2.zero) {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            //animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("moving", true);
        } 
        else {
            animator.SetBool("moving", false);
        }
    }

    private IEnumerator AttackCo() {    
        animator.SetBool("attacking", true);
        swordAttack.Play();
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f);
        currentState = PlayerState.walk;
    }

    private IEnumerator AttackBowCo() {
        animator.SetBool("attackBow", true);
        currentState = PlayerState.attackBow;
        yield return null;
        animator.SetBool("attackBow", false);
        yield return new WaitForSeconds(0.6f);
        currentState = PlayerState.walk;
    }

    void FixedUpdate() {            //Movement            
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(1.0f);     
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void HitPlayer() {
        health--;
        dmgsound.Play();
        Debug.Log("HealthPlayer subtracted, total: " + health);
        if (health == 0) {
            becauseText.text = "because: Health Over";
            HighscoreUpdate();
            Debug.Log("Player Dead");
            Time.timeScale = 0f;
            Die();
        }
    }

    public void HealPlayer() {
        if (health < 5) {
            health++;
        }
        Debug.Log("HealthPlayer added, total: " + health);
    }

    public int getHealth() {
        return health;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////

    private void DisplayHighscore() {     
        var line = File.ReadAllLines(path);         //Read the text from directly from the .txt file
        highscoreText.text = "Highscore: " + line[0];
    }

    public void HighscoreUpdate() {
        //Read the text from directly from the .txt file
        var line = File.ReadAllLines(path);
        List<string> scores = line.ToList();

        var p = score.ToString();
        if (!scores.Contains(p)) {
            scores.Add(p);
        }
            
        string[] strings = scores.ToArray();
        int[] ints = Array.ConvertAll(strings, int.Parse);
            
        Array.Sort(ints);
        Array.Reverse(ints);
            
        line = new string[3];
        for (int i = 0; i < 3; i++) {
            line[i] = ints[i].ToString();
        }
        
        File.WriteAllLines(path, line);
    }

    // Eine Methode für das beenden des Games
    public void Die() {
        scoreT.text = "score: " + score;
        youDiedUI.SetActive(true);
    }
}
