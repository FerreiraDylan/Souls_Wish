using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyManager enemy;
    private float timedelay = 2f;
    private float waitTime = 0f;
    private float DownTime = 0f;
    private bool isReady = false;



    // Start is called before the first frame update
    void Start()
    {
        DownTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        waitTime = DownTime + timedelay;
        
        if (other.gameObject.name == "Player")
        {
            if (Time.time >= waitTime && isReady == false)
                isReady = true;
            if (isReady == true)
            {
                enemy.EnemyAnim_Manager.EnemyAttack();
                if (PlayerManager.instance.Shield_isActive == false)
                    PlayerManager.instance.CurrentHealth -= enemy.Damage;
                isReady = false;
            }
        }
    }

    // private void OnTriggerStay(Collider other)
    // { 
    //     DownTime = Time.time;
    //     waitTime = DownTime + timedelay;
        
    //     if (other.gameObject.name == "Player")
    //     {
    //         if (Time.time <= waitTime && isReady == false)
    //             isReady = true;
    //         if (isReady == true)
    //         {
    //             enemy.EnemyAnim_Manager.EnemyAttack();
    //             if (PlayerManager.instance.Shield_isActive == false)
    //                 PlayerManager.instance.CurrentHealth -= enemy.Damage;
    //             isReady = false;
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
