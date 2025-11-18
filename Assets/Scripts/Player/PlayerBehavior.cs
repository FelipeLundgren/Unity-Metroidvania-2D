using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce = 2.5f;
    [SerializeField] private float moveSpeed = 5;

    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;
    
    [SerializeField] private ParticleSystem hitParticle;

    private Rigidbody2D rigidbodyPlayer;
    private isGroundChecker isGroundChecker;
    private float moveDirection;
    private bool hasPowerUp = false;
    [SerializeField] private Health health;

    private void Awake()
    {
        UpdateLives(health.GetLives());
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        isGroundChecker = GetComponent<isGroundChecker>();
        health.OnDead += HandlePlayerDeath;
        health.OnHurt += HandleHurt; 
    }

    private void Start()
    {
        GameManager.Instance.InputManager.onJump += HandleJump;
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float moveDirection = GameManager.Instance.InputManager.Movement;
        Vector2 directionToMove = new Vector2(moveDirection * moveSpeed, rigidbodyPlayer.linearVelocity.y );
        rigidbodyPlayer.linearVelocity = directionToMove;
        
        
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void HandleJump()
    {
        float powerJumpForce = jumpForce;
        if (hasPowerUp)
        {
            powerJumpForce*= 1.5f;
        }
        
        if (isGroundChecker.isGrounded() == false)
            return;
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerJump);
        rigidbodyPlayer.linearVelocity += Vector2.up * powerJumpForce;
    }

    private void HandlePlayerDeath()
    {
        UpdateLives(health.GetLives());
        PlayHitParticle();
        GetComponent<Collider2D>().enabled = false;
        rigidbodyPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
        GameManager.Instance.InputManager.DisablePlayerInput();
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerDeath);
        
    }

    private void Attack()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerAttack);
        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage();
            }
        }
    }

    private void HandleHurt() 
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerHurt);
        PlayHitParticle();
        UpdateLives(health.GetLives());
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.PlayerWalk);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void UpdateLives(int amount)
    {
        GameManager.Instance.UpdateLives(amount);
    }
    
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }

    public void PowerUpCollected(string text)
    {
        hasPowerUp = true;
        GameManager.Instance.SetPowerUpText(text);

    }



}