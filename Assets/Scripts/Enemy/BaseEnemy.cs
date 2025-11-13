using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected Health health;
    protected bool canAttack = true;
    protected AudioSource audioSource;
    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
    }
    
    protected abstract void Update();

    private void PlayHurtAnim() => animator.SetTrigger("hurt");
    private void HandleDeath()
    {
        canAttack = false;
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("dead");
        StartCoroutine(DestroyEnemy(1));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
    
    
    
    
}
