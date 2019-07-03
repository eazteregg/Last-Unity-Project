using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroManEnemy : MonoBehaviour
{
    public int EnemyHealth = 10;
    public GameObject NecroEnemy;
    public int WizardStatus;
    public NacroWalkAI necroManScript;
    public SpiderAI sp;



    // Start is called before the first frame update
    void Start()
    {
        necroManScript = GetComponent<NacroWalkAI>();
        //sp = GetComponent<SpiderAI>();


    }


    void DeductPoints(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.M))
        {
            StartCoroutine(DeathSpider());
        }


    }

    IEnumerator DeathSpider()
    {
        necroManScript.enabled = false;
        yield return new WaitForSeconds(0.5f);
        NecroEnemy.GetComponent<Animation>().Play("Death1");
    }
}
