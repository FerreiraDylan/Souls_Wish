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

    private void OnTriggerWalk(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemy.Enemy_Movement();
        } else {
            enemy.EnemyIdle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
