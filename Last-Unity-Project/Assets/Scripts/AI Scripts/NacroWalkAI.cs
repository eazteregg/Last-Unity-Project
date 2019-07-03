using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NacroWalkAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int Xpos;
    public int Zpos;
    public GameObject NPCDest;

    public float health = 6;

    public GameObject ThePlayer;
    public float TargetDistance;
    public float NoticeRange = 50;
    public float AllowedRange = 40;
    public GameObject TheEnemy;
    public float EnemySpeed;
    public int AttackTrigger;
    public RaycastHit Shot;

    private float move = 20;
    private bool stop = false;
    private float blend;
    private float delay = 0;
    public float AddRunSpeed = 1;
    public float AddWalkSpeed = 1;
    private bool hasAniComp = false;

    public Image overlayImage;

    private float r;
    private float g;
    private float b;
    private float a;


    // Use this for initialization
    void Start()
    {

        Xpos = UnityEngine.Random.Range(20, -26);
        Zpos = UnityEngine.Random.Range(-44, -22);
        NPCDest.transform.position = new Vector3(Xpos, 0.83f, Zpos);
        StartCoroutine(RunRandomWalk());

        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = 0;

        Color c1 = new Color(r, g, b, a);
        overlayImage.color = c1;

        if (null != GetComponent<Animation>())
        {
            hasAniComp = true;
        }

    }

    bool CheckAniClip(string clipname)
    {
        if (TheEnemy.GetComponent<Animation>().GetClip(clipname) == null)
            return false;
        else if (TheEnemy.GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }

    void Update()
    {
        float dist = Vector3.Distance(ThePlayer.transform.position, transform.position);
        if (dist <= NoticeRange)
        {
            NPCDest.SetActive(false);
        }
        if (NPCDest.activeSelf)
        {


            transform.LookAt(NPCDest.transform);
            TheEnemy.GetComponent<Animation>().Play("Run");
            transform.position = Vector3.MoveTowards(transform.position, NPCDest.transform.position, 0.05f);

        }

        else
        {
            transform.LookAt(ThePlayer.transform);

            //transform.LookAt(ThePlayer.transform);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
            {
                TargetDistance = Shot.distance;

                if (TargetDistance <= AllowedRange)
                {


                    EnemySpeed = 0.05f;
                    if (AttackTrigger == 0)
                    {
                        a = a - 0.003f;
                        if (health < 3)
                        {
                            health = health + 1;
                        }

                        TheEnemy.GetComponent<Animation>().CrossFade("Run");
                        transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, EnemySpeed);
                    }
                }
                else
                {
                    EnemySpeed = 0;
                    TheEnemy.GetComponent<Animation>().CrossFade("Idle1");
                    //GetComponent<Animation>().Play("idle_normal");
                }
            }
            if (AttackTrigger == 1)
            {

                EnemySpeed = 0;
                if (CheckAniClip("Magic Attack") == false) return;


                TheEnemy.GetComponent<Animation>().CrossFade("Attack", 0.0f);
                float length = TheEnemy.GetComponent<Animation>().GetClip("Attack").length;
                float time = TheEnemy.GetComponent<Animation>()["Attack"].time;
                //Debug.Log(length + "asdasdas"+Math.Round(time,1));
                if (Math.Round(time, 1) == Math.Round(length, 1))
                {
                    health = health - 1;
                }
                TheEnemy.GetComponent<Animation>().CrossFadeQueued("Idle2");


                a = a + 0.005f;

                //
            }

            a = Mathf.Clamp(a, 0, 1f);
            AdjustColor();

            Debug.Log(health);
            if (health == 0)
            {
                SceneManager.LoadScene(1);
            }
            //Move();

            if (Input.GetKey(KeyCode.M))
            {
                if (CheckAniClip("Death1") == false) return;
                Debug.Log("hellobabyi");
                TheEnemy.GetComponent<Animation>().Play("Death1");
                //TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_normal");
                //					animation.CrossFadeQueued("idle_normal");
            }
          

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

    private void AdjustColor()
    {
        Color c = new Color(r, g, b, a);
        overlayImage.color = c;
    }

    IEnumerator RunRandomWalk()
    {
        yield return new WaitForSeconds(2);
        Xpos = UnityEngine.Random.Range(20, -26);
        Zpos = UnityEngine.Random.Range(-44, -22);
        NPCDest.transform.position = new Vector3(Xpos, 0.83f, Zpos);
        StartCoroutine(RunRandomWalk());
    }
}
