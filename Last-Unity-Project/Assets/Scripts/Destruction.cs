using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Destruction : MonoBehaviour
{

    public float hitpoints;
    public Animator anim;
    public BigSpider WizarScript;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        //anim = Enemy.GetComponent<Animation>();
        WizarScript = GetComponent<BigSpider>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
// Debug.Log("Hitpoints:" + hitpoints.ToString());

        if (hitpoints <= 0.0f)
        {
            WizarScript.enabled = false;
            //anim.CrossFade("dead", 0.2f);
            anim.Play("dead");
            //float length = anim.GetCurrentAnimatorStateInfo(0).length;
            //float time =anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            // Debug.Log(length + "asdasdas" + time);

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            {
                float time = anim.GetCurrentAnimatorClipInfo(0).Length;
                float length = anim.GetCurrentAnimatorStateInfo(0).length;
                float length1 = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

                Debug.Log(time + "time  " + length + "  lenght  " + length1);

                if (length1 >= 1)
                {
                    gameObject.SetActive(false);
                }
            }




        //Destroy(gameObject);
        }
    }
 

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Spell")
        {
            hitpoints -= other.gameObject.GetComponent<Damage>().getDamage() * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spell")
        {
            hitpoints -= other.gameObject.GetComponent<Damage>().getDamage();
        }
    }
}
