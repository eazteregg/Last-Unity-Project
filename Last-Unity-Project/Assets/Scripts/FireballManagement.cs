using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManagement : MonoBehaviour
{
    Stack<GameObject> fireballs;
    public float fireBallLifetime;
    public float fireBallsPerSecond;
    private float coolDown;
    GameObject origFireball;

    // Start is called before the first frame update
    void Start()
    {
        fireballs = new Stack<GameObject>();
        coolDown = 0.0f;
        GameObject origFireball = GetComponentInChildren<Spawn>(includeInactive:true).gameObject;
        int maxFireballs = Mathf.CeilToInt(fireBallLifetime * fireBallsPerSecond);
        Debug.Log(maxFireballs.ToString());
        fireballs.Push(origFireball);

        for (int i=1; i < maxFireballs; i++)
        {
            
            fireballs.Push(Instantiate(origFireball, parent:transform));
        }

    }

    private void Update()
    {
        coolDown -= 1 * Time.deltaTime;
    }

    public void Push(GameObject fireball)
    {
        fireballs.Push(fireball);
    }

    public void SpawnFireball()
    {
        if (coolDown <= 0)
        {
            GameObject fireball = fireballs.Pop();
            fireball.GetComponent<Spawn>().SpawnFireball();
            coolDown = 1 / fireBallsPerSecond;
        }
        
    }
    private void OnApplicationQuit()
    {
        foreach(GameObject fireball in fireballs)
        {
            if (fireball != origFireball)
            {
                Destroy(fireball);
            }
        }
    }
}
