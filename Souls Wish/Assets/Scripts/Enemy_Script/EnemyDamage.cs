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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Epee")
        {
            if (PlayerManager.instance.Attack_isActive)
                enemy.EnemyTakeDamage(PlayerManager.instance.Damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
