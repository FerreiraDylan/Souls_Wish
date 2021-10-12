using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public EnemyManager enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerDamage(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemy.EnemyTakeDamage(PlayerManager.instance.Damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
