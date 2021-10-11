using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Env_Effects : MonoBehaviour
{
    public bool damage;
    public bool slow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (slow)
                PlayerManager.instance.TakeSlow(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (damage)
                PlayerManager.instance.TakeDamage(15);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (slow)
                PlayerManager.instance.TakeSlow(false);
        }
    }

}
