using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

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


    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerState currentState;

    public int health;
    private int Toepfe;

    public static int score; // bad coding style but ok for 1 variable
    public Text scoreText;
    
    public Text ToepfeCount;

    Vector2 movement;

    GameObject youDiedUI;

    void Start(){
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

        ToepfeCount.text = "Toepfe uebrig: " + Toepfe;
    }

    // Update is called once per frame
    void Update()
    {
        switch(health){
            case 0: break;
            heart0.SetActive(false);
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
        //input
        movement.x = Input.GetAxisRaw("Horizontal");
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
            moveSpeed = 5f;
            UpdateAnimationAndMove();
        }

        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove(){
        if(movement != Vector2.zero){
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            //animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }

    private IEnumerator AttackCo(){
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f);
        currentState = PlayerState.walk;
    }

    private IEnumerator AttackBowCo()
    {
        animator.SetBool("attackBow", true);
        currentState = PlayerState.attackBow;
        yield return null;
        animator.SetBool("attackBow", false);
        yield return new WaitForSeconds(0.6f);
        currentState = PlayerState.walk;
    }

    void FixedUpdate(){
        //Movement
        movement.Normalize();
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void HitPlayer() {
        health--;
        Debug.Log("HealthPlayer subtracted, total: " + health);
        if (health == 0) {
            Debug.Log("Player Dead");
            Time.timeScale = 0f;
            youDiedUI.SetActive(true);
            StartCoroutine(Wait());
            SceneManager.LoadScene("StartMenu");
        }
    }

    public void HealPlayer() {
        if (health < 5) {
            health++;
            }
        Debug.Log("HealthPlayer added, total: " + health);
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(1.0f);
        
    }

    public int getHealth() {
        return health;
    }
}
