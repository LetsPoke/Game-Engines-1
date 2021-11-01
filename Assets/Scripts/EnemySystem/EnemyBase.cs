using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int health;
    public int baseAttack;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // WARUM FUNKTIONIERT UPDATE NICHT???????
    }

    void OnCollisionEnter2D(Collision2D collision) {
        health--;
        Debug.Log("Ouchie " + health);
        
        if (health == 0) {
            Destroy(gameObject);
            Debug.Log("Dead");
        }

    }
}