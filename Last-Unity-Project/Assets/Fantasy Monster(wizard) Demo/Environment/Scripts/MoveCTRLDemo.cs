using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MoveCTRLDemo : MonoBehaviour {

    public int Xpos;
    public int Zpos;
    public GameObject NPCDest;

    public float health = 3;

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
    void Start () 
	{
        
        Xpos = UnityEngine.Random.Range(713, 844);
        Zpos = UnityEngine.Random.Range(782, 904);
        NPCDest.transform.position = new Vector3(Xpos, 4.61f, Zpos);
        StartCoroutine(RunRandomWalk());

        r = overlayImage.color.r;
        g = overlayImage.color.g;
        b = overlayImage.color.b;
        a = 0;

        Color c1 = new Color(r, g, b, a);
        overlayImage.color = c1;

        if ( null != GetComponent<Animation>() )
		{
			hasAniComp = true;
		}

	}

	void Move ()
	{ 
		float speed =0.0f;
		float add =0.0f;

		if ( hasAniComp == true )
		{	
			if ( Input.GetKey(KeyCode.UpArrow) )
			{  	
				move *= 1.015F;

				if ( move>250 && CheckAniClip( "move_forward_fast" )==true )
				{
					{
						GetComponent<Animation>().CrossFade("move_forward_fast");
						add = 20*AddRunSpeed;
					}
				}
				else
				{
					GetComponent<Animation>().Play("move_forward");
					add = 5*AddWalkSpeed;
				}

				speed = Time.deltaTime*add;

				transform.Translate( 0,0,speed );
			}


			if ( Input.GetKeyUp(KeyCode.UpArrow))
			{
				if ( GetComponent<Animation>().IsPlaying("move_forward"))
				{	GetComponent<Animation>().CrossFade("idle_normal",0.3f); }
				if ( GetComponent<Animation>().IsPlaying("move_forward_fast"))
				{	
					GetComponent<Animation>().CrossFade("idle_normal",0.5f);
					stop = true;
				}	
				move = 20;
			}

			if (stop == true)
			{	
				float max = Time.deltaTime*20*AddRunSpeed;
				blend = Mathf.Lerp(max,0,delay);

				if ( blend > 0 )
				{	
					transform.Translate( 0,0,blend );
					delay += 0.025f; 
				}	
				else 
				{	
					stop = false;
					delay = 0.0f;
				}
			}
		}
		else
		{
			if ( Input.GetKey(KeyCode.UpArrow) )
			{  	
				add = 5*AddWalkSpeed;
				speed = Time.deltaTime*add;
				transform.Translate( 0,0,speed );
			}

		}
	}

	bool CheckAniClip ( string clipname )
	{	
		if(TheEnemy.GetComponent<Animation>().GetClip(clipname) == null ) 
			return false;
		else if (TheEnemy.GetComponent<Animation>().GetClip(clipname) != null ) 
			return true;

		return false;
	}

    // Update is called once per frame
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
            TheEnemy.GetComponent<Animation>().Play("move_forward_fast");
            transform.position = Vector3.MoveTowards(transform.position, NPCDest.transform.position, 0.07f);

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


                    EnemySpeed = 0.09f;
                    if (AttackTrigger == 0)
                    {
                        a = a - 0.003f;
                        if (health < 3)
                        {
                            health = health + 1;
                        }

                        TheEnemy.GetComponent<Animation>().CrossFade("move_forward_fast");
                        transform.position = Vector3.MoveTowards(transform.position, ThePlayer.transform.position, EnemySpeed);
                    }
                }
                else
                {
                    EnemySpeed = 0;
                    TheEnemy.GetComponent<Animation>().CrossFade("idle_normal");
                    //GetComponent<Animation>().Play("idle_normal");
                }
            }
            if (AttackTrigger == 1)
            {

                EnemySpeed = 0;
                if (CheckAniClip("attack_short_001") == false) return;


                TheEnemy.GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
                float length = TheEnemy.GetComponent<Animation>().GetClip("attack_short_001").length;
                float time = TheEnemy.GetComponent<Animation>()["attack_short_001"].time;
                //Debug.Log(length + "asdasdas"+Math.Round(time,1));
                if (Math.Round(time, 1) == Math.Round(length, 1))
                {
                    health = health - 1;
                }
                TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_combat");


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

            if (Input.GetKey(KeyCode.V))
            {
                if (CheckAniClip("dead") == false) return;
                Debug.Log("hellobabyi");
                TheEnemy.GetComponent<Animation>().Play("dead");
                //TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_normal");
                //					animation.CrossFadeQueued("idle_normal");
            }
            if (hasAniComp == true)
            {
                Debug.Log("hi");
                if (Input.GetKey(KeyCode.V))
                {
                    if (CheckAniClip("dead") == false) return;
                    Debug.Log("i");
                    TheEnemy.GetComponent<Animation>().CrossFade("dead", 0.2f);
                    TheEnemy.SetActive(false);
                    TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_normal");
                    //					animation
                }



                if (Input.GetKey(KeyCode.Q))
                {
                    if (CheckAniClip("attack_short_001") == false) return;

                    TheEnemy.GetComponent<Animation>().CrossFade("attack_short_001", 0.0f);
                    TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_combat");
                }



                if (Input.GetKey(KeyCode.Z))
                {
                    if (CheckAniClip("damage_001") == false) return;

                    TheEnemy.GetComponent<Animation>().CrossFade("damage_001", 0.0f);
                    TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_combat");
                }



                if (Input.GetKey(KeyCode.D))
                {
                    if (CheckAniClip("idle_normal") == false) return;

                    TheEnemy.GetComponent<Animation>().CrossFade("idle_normal", 0.0f);
                    TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_normal");
                }

                if (Input.GetKey(KeyCode.F))
                {
                    if (CheckAniClip("idle_combat") == false) return;

                    TheEnemy.GetComponent<Animation>().CrossFade("idle_combat", 0.0f);
                    TheEnemy.GetComponent<Animation>().CrossFadeQueued("idle_normal");
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, Time.deltaTime * -100, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, Time.deltaTime * 100, 0);
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
      
            yield return new WaitForSeconds(5);
            Xpos = UnityEngine.Random.Range(713, 844);
            Zpos = UnityEngine.Random.Range(782, 904);
            NPCDest.transform.position = new Vector3(Xpos, 4.61f, Zpos);
            StartCoroutine(RunRandomWalk());
        
    }

}
