using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
 public GameObject destroyedVersion;

 void OnMouseDown()
 {
   //spawn shattered Object
   Instantiate(destroyedVersion, transform.position, transform.rotation);
   //Remove whole object
   Destroy(gameObject);
 }
}
