using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public enum PlayerState{
    walk,
    attack,
    interact,
    attackBow
}

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public PlayerState currentState;

    public int health;

    Vector2 movement;

    GameObject youDiedUI;

    void Start(){
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();

        youDiedUI = GameObject.FindGameObjectWithTag("dead");
        youDiedUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        health++;
        Debug.Log("HealthPlayer added, total: " + health);
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(5);
    }
}
