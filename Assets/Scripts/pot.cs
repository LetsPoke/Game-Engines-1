using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pot : MonoBehaviour
{
    private Animator anim;
    public UpgradeSpawner upgrade;

    //public int score = 0;
    //public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //scoreText.text = "Score: " + score;
        
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Score: " + score;
    }

    public void Smash() 
    {
        anim.SetBool("smash", true);
        PlayerMovement.score = PlayerMovement.score +1;
        StartCoroutine(breakCo());

        int random = Random.Range(1, 10);
        Debug.Log(random);

        if(random <= 10){
            Vector3 currentPos = transform.position;
            upgrade.SpawnHealth(currentPos);
        }
        
    }

    IEnumerator breakCo() 
    {
        
        yield return new WaitForSeconds(.3f);
        //score = score +1;
        this.gameObject.SetActive(false);
        
    }
}
