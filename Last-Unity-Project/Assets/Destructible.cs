using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject destroyedVersion;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Spell")
        {
            Destroy(gameObject);
            Instantiate(destroyedVersion, transform.position, transform.rotation);
        }
    }
}
