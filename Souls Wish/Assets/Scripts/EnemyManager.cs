using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{

    [Header("Enemy Animation")]
    public Animator animator;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float HorizontalMove = 0f;

    [Header("Enemy_Zone")]
    private bool IsAggro;
    private bool IsAttacking;

    [Header("Enemy UI")]
    public Slider UI_Health_Enemy;
    public GameObject UI_Canvas;

    [Header("Enemy Characteristics")]
    public float Speed = 6f;
    public float MaxHealth;
    public float CurrentHealth;
    public float Damage = 50f;
    public float Level = 0f;
    public float GiveExp = 5f;
    public int GiveMoney = 0;
    public GameObject Player;

    [Header("Gravity & Jump")]
    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private float _speed = 0.0f;

    public bool jump = false;
    public float jumpHeight = 3f;

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
        Starter_Enemy();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_Movement();
        Enemy_Ui();
        Enemy_Attack();
    }

    /// Mouvement
    public void Enemy_Movement()
    {
        EnemyWalk();

        float x = 0f;
        float z = 0f;
        Vector3 direction = new Vector3(x, 0f, z).normalized;

        

        if ((x != 0 || z != 0) || (x != 0 && z != 0))
            _speed = 1;
        else
            _speed = 0;
    }

    /// Attack

    public void Enemy_Attack()
    {
        EnemyAttackHealth();    
    }

    public void EnemyAttackHealth()
    {
        animator.SetBool("Attack", true);
        Player.GetComponent<PlayerManager>().CurrentHealth -= Damage;
    }


    /// Health
    public void Enemy_Ui()
    {
        UI_Health_Enemy.value = GetHealthEnemy();
        if (CurrentHealth == 0)
        {
            Player.GetComponent<PlayerManager>().CurrentMoney += GiveMoney;
            Player.GetComponent<PlayerManager>().CurrentExp += GiveExp;
        }
    }
    public void Starter_Enemy()
        {
            CurrentHealth = MaxHealth;
            UI_Health_Enemy.value = GetHealthEnemy();
        }
    public float GetHealthEnemy()
    {
        return (CurrentHealth / MaxHealth);
    }
    
    /// Animation

    public void EnemyDeath()
    {
        animator.SetBool("Death", true);
    }

    public void EnemyIdle()
    {
        animator.SetBool("Idle", true);
    }

    public void EnemyWalk()
    {
        animator.SetBool("Walk", true);
    }

    public void EnemyTakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void EnemyTakeSlow(bool slow)
    {
        Speed = (slow ? 3 : 6);
    }
}
