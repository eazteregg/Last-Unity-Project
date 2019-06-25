using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{

    public float hitpoints;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
// Debug.Log("Hitpoints:" + hitpoints.ToString());

        if (hitpoints <= 0.0f)
        {
            Destroy(gameObject);
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
