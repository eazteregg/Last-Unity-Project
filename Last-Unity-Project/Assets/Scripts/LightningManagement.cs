using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningManagement : MonoBehaviour
{
    #region Singleton

    public static LightningManagement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    public float OFFSET;
    public Transform wandPivot;
    GameObject lightning;
    [SerializeField] private float mana;
    [SerializeField] private float manaCost;
    [SerializeField] private float manaRegen;

    //OB
    private bool Sound_On;
    public AudioSource m_AudioSource;
    public AudioClip Lighting;


    // Start is called before the first frame update
    void Start()
    {
        lightning = transform.GetChild(0).gameObject;
        lightning.SetActive(false);

        //OB
        m_AudioSource = GetComponent<AudioSource>();
        Sound_On = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Sound_On == true)
        {
            m_AudioSource.PlayOneShot(Lighting, 0.3f);
            //Sound_On = false;
        }

        if (!LightningActive()) return;
        transform.position = wandPivot.position + (wandPivot.forward * OFFSET);
        transform.rotation = wandPivot.rotation;

        mana -= manaCost * Time.deltaTime;
            
        if (mana <= 0)
        {
            DeactivateLightning();
        }
        else if (mana < 100)
        {
            mana += manaRegen * Time.deltaTime;
        }
    }

    public bool LightningActive()
    {
        return lightning.active;
    }

    public void ActivateLightning()
    {
        lightning.SetActive(true);
        //OB
        Sound_On = true;
    }

    public void DeactivateLightning()
    {
        //OB
        Sound_On = false;
        m_AudioSource.Stop();


        lightning.SetActive(false);
    }
}