using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : MonoBehaviour
{
    public int EnemyHealth = 10;
    public GameObject Wizardenemy;
    public int WizardStatus;
    public MoveCTRLDemo WizardCtrlScript;
    public SpiderAI sp;



    // Start is called before the first frame update
    void Start()
    {
        WizardCtrlScript = GetComponent<MoveCTRLDemo>();
        //sp = GetComponent<SpiderAI>();
        

    }

  
    void DeductPoints(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
    }

    void Update()
    {
       
            if (Input.GetKey(KeyCode.V))
            {
                StartCoroutine(DeathSpider());
            }
           
        
    }

    IEnumerator DeathSpider()
    {
        WizardCtrlScript.enabled = false;
        yield return new WaitForSeconds(0.5f);
        Wizardenemy.GetComponent<Animation>().Play("dead");
    }
}
