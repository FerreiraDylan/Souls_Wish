using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgro : MonoBehaviour
{
    public EnemyManager enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemy.Enemy_Movement();
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.name == "Player")
    //     {
    //         EnemyManager.instance.Enemy_Movement();
    //     }
    // }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemy.EnemyAnim_Manager.EnemyIdle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
