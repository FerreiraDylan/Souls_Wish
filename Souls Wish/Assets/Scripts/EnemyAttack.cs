using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyManager enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerAttack(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemy.Enemy_Attack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
