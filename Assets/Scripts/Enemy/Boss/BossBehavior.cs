using System;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform playerPosition;
    private Health health;
    private Animator animator;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private ParticleSystem hitParticle;


    [Header("Attack properties")]
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private float attackSize = 1f;
    private Vector3 attackPosition;

    private bool canAttack = false;
    private bool isFlipped = true;
    
    
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
        health.OnHurt += PlayHurtAudio;
        health.OnDead += PlayDeadAudio;
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPosition = GameManager.Instance.GetPlayer().transform;
    }


    public void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        GetComponent<Rigidbody2D>().MovePosition(newPos);
        LookAtPlayer();
        CheckPositionFromPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > playerPosition.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPosition.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void CheckPositionFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(rb.position, playerPosition.position);
        if (distanceFromPlayer <= attackDistance)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    private void Attack()
    {
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;
        audioSource.clip = audioClips[0];
        audioSource.Play();

        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackSize, attackMask);
        if (collisionInfo != null)
        {
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
    }

    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition, attackSize);
    }

    private void PlayHurtAnim()
    {
        audioSource.Play();
        animator.SetTrigger("hurt");
        PlayHitParticle();
    }

    private void HandleDeath()
    {
        //audioSource.clip = audioClips[2];
        //audioSource.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetTrigger("dead");
        PlayHitParticle();
    }
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
    
    private void PlayHurtAudio()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    private void PlayDeadAudio()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }
    
}