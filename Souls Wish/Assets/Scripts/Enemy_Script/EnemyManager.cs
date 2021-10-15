using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    [Header("Enemy Animation")]
    public EnemyAnimManager EnemyAnim_Manager;
    public EnemyAudioManager EnemyAudio_Manager;

    [Header("Enemy UI")]
    public Slider UI_Health_Enemy;
    public GameObject UI_Canvas;

    [Header("Enemy Characteristics")]
    protected NavMeshAgent enemyMesh;
    public float Speed = 6f;
    public float MaxHealth;
    public float CurrentHealth;
    public float Damage = 50f;
    public float GiveExp = 5f;
    public int GiveMoney = 0;

    [Header("Gravity & Jump")]
    Vector3 velocity;
    public float gravity = -9.81f;

    public int _speed = 0;


    public static EnemyManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMesh = GetComponent<NavMeshAgent>();
        Starter_Enemy();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_Ui();
        Enemy_Animation();
    }

    /// Mouvement
    public void Enemy_Movement()
    {
        EnemyAnim_Manager.EnemyWalk();
        Debug.Log("Enemy = " + transform.position);
        Debug.Log("Player = " + PlayerManager.instance.gameObject.transform.position);
        enemyMesh.SetDestination(PlayerManager.instance.gameObject.transform.position);
        
    }

    public void Enemy_Animation()
    {
        EnemyAnim_Manager.Anim_Enemy_Speed();
    }

    public void EnemyAttackHealth()
    {
        PlayerManager.instance.CurrentHealth -= Damage;
    }


    /// Health
    public void Enemy_Ui()
    {
        UI_Health_Enemy.value = GetHealthEnemy();
        if (CurrentHealth <= 0)
        {
            PlayerManager.instance.CurrentExp += GiveExp;
            PlayerManager.instance.CurrentMoney += GiveMoney;
            Destroy_Enemy();
        }
    }
    public void Starter_Enemy()
    {
        CurrentHealth = MaxHealth;
        UI_Health_Enemy.value = GetHealthEnemy();
        EnemyAnim_Manager.SetSpeedActive();
    }

    public void Destroy_Enemy()
    {
        EnemyAnim_Manager.EnemyDeath();
        EnemyAudio_Manager.Enemy_Adio_Death();
        Destroy(gameObject);
    }
    public float GetHealthEnemy()
    {
        return (CurrentHealth / MaxHealth);
    }
    
    /// Animation

    public void EnemyTakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void EnemyTakeSlow(bool slow)
    {
        Speed = (slow ? 3 : 6);
    }
}
