using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimManager : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float EnemyAttackAnimDuration = 1.25f;
    [SerializeField] private float EnemyDeathAnimDuration = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Anim_Enemy_Speed()
    {
        animator.SetFloat("Speed", 6f);
    }

    public void EnemyDeath()
    {
        animator.SetBool("IsDead", true);
        StartCoroutine(EnemyAttackAnim_Duration());
    }

    public void EnemyIdle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Attack", false);
    }

    public void EnemyAttack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Walk", false);
        StartCoroutine(EnemyAttackAnim_Duration());
    }

    public void EnemyWalk()
    {
        animator.SetBool("Walk", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack", false);
    }

    public void SetSpeedActive()
    {
        animator.SetBool("SpeedActive", true);
    }

    public IEnumerator EnemyDeathAnim_Duration()
    {
        yield return new WaitForSeconds(EnemyDeathAnimDuration);
        animator.SetBool("Death", false);

    }

    public IEnumerator EnemyAttackAnim_Duration()
    {
        yield return new WaitForSeconds(EnemyAttackAnimDuration);
        animator.SetBool("Attack", false);
    }
}
