using System;
using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected Health health;
    [SerializeField] private ParticleSystem hitParticle;
    protected bool canAttack = true;
    protected AudioSource audioSource;
    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHurt += HandleHurt;
        health.OnDead += HandleDeath;
    }
    
    protected abstract void Update();

    private void HandleHurt()
    {
        animator.SetTrigger("hurt");
        PlayHitParticle();
    }

    private void HandleDeath()
    {
        canAttack = false;
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("dead");
        PlayHitParticle();
        StartCoroutine(DestroyEnemy(1));
    }

    private IEnumerator DestroyEnemy(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
    
    
    
    
    
    
}
