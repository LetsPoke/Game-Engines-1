using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{
    private Animator anim;
    public UpgradeSpawner upgrade;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash() 
    {
        anim.SetBool("smash", true);
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
        this.gameObject.SetActive(false);
    }
}
