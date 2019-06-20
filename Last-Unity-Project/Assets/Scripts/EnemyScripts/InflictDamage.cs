using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inflict Damage Script:

public class InflictDamage : MonoBehaviour
{

    public int DamageAmount = 5;
    public float TargetDistance;
    public float AllowedRange = 2.7f;


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
            {
                TargetDistance = hit.distance;
                if (TargetDistance <= AllowedRange)
                {
                    hit.transform.SendMessage("DeductPoints", DamageAmount, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }

}


//===================

//SpiderEnemy

public class SpiderEnemy : MonoBehaviour
{

    public int EnemyHealth = 10;

    void DeductPoints(int DamageAmount)
    {
        EnemyHealth -= DamageAmount;
    }

    void Update()
    {
        if (EnemyHealth <= 0)
        {
            StartCoroutine(DeathSpider());
        }
    }

    IEnumerator DeathSpider()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}