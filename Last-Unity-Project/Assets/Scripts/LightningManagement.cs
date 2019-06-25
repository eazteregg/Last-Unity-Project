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

    // Start is called before the first frame update
    void Start()
    {
        lightning = transform.GetChild(0).gameObject;
        lightning.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = wandPivot.position + (wandPivot.forward * OFFSET);
        transform.rotation = wandPivot.rotation;
    }

    public bool LightningActive()
    {
        return lightning.active;
    }

    public void ActivateLightning()
    {
        lightning.SetActive(true);
    }

    public void DeactivateLightning()
    {
        lightning.SetActive(false);
    }
}
