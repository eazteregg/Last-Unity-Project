using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpiderAI : MonoBehaviour
{
    public GameObject ThePlayer;
    public float TargetDistance;
    public GameObject TheEnemy;
    public float EnemySpeed;
    public int AttackTrigger;
    public int DealingDamage;
    public float AllowedRange = 80;
    public RaycastHit Shot;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThePlayer.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if (TargetDistance <= AllowedRange)
            {
                EnemySpeed = 0.05f;
                if (AttackTrigger == 0)
                {
                    TheEnemy.GetComponent<Animation>().Play("Run");
                    transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, EnemySpeed);
                }
            }
            else
            {
                EnemySpeed = 0;
                TheEnemy.GetComponent<Animation>().Play("Idle");
            }
        }
        
        if (AttackTrigger == 1)
        {
                TheEnemy.GetComponent<Animation>().Play("Attack");
        }

    }
   

    void OnTriggerEnter()
    {
        AttackTrigger = 1;
    }

    void OnTriggerExit()
    {
        AttackTrigger = 0;
    }
}
