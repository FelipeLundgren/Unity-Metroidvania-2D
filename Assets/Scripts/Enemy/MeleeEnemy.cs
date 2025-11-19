using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    [SerializeField] private Rigidbody2D enemyRb;
    
    [SerializeField] private int direction = 1;
    [Header("Attack properties")]
    [SerializeField] private Transform detectPosition;
    [SerializeField] private Vector2 detectBoxSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCooldown;
    [Header("Audio properties")] 
    [SerializeField] private AudioClip[] audioClips;
    
    private float cooldownTimer;
    protected override void Awake()
    {
        base.Awake();
        base.health.OnHurt += PlayHurtAudio;
        base.health.OnDead += PlayDeadAudio;
        enemyRb =  GetComponent<Rigidbody2D>();
    }
    protected override void Update()
    {
        VerifyCanAttack();
        cooldownTimer += Time.deltaTime;
        Patroll();
    } 

    private void VerifyCanAttack()
    {
        if (cooldownTimer < attackCooldown) return;
        if (PlayerInSight() && canAttack)
        {
            animator.SetTrigger("attack");
            AttackPlayer();
        }
    }
    private void AttackPlayer()
    {
        
        cooldownTimer = 0;
        if (CheckPlayerInDetectArea().TryGetComponent(out Health playerHealth))
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            playerHealth.TakeDamage();
        }
    }

    private Collider2D CheckPlayerInDetectArea()
    {
        return Physics2D.OverlapBox(detectPosition.position, detectBoxSize, 0f, playerLayer);
    }

    private bool PlayerInSight()
    {
        Collider2D playerCollider = CheckPlayerInDetectArea();
        return playerCollider != null;
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

 

    private void OnDrawGizmos()
    {
        if (detectPosition == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPosition.position, detectBoxSize);
    }

    private void Patroll()
    {
        if (direction > 0)
        {
            enemyRb.linearVelocity = Vector2.left * speed; 
        }
        else
        {
            enemyRb.linearVelocity = Vector2.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            direction *= -1;
            transform.localScale = new Vector3(direction*-1, 1, 1);
        }
    }
    
    
}