using System.Collections;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Transform playerPosition;
    [SerializeField] private float moveSpeed = 3f;
    [Header("Attack properties")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackSize = 1f;
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private LayerMask attackMask;
    [SerializeField] private ParticleSystem hitParticle;
    private Vector3 attackPosition;
    private bool canAttack = false;
    private bool isFlipped = true;
    private Animator animator;
    private Health health;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHurt += PlayHurtAnim;
        health.OnDead += HandleDeath;
    }

    private void Start()
    {
        playerPosition = GameManager.Instance.GetPlayer().transform;
    }
    private void PlayHurtAnim()
    {
        animator.SetTrigger("hurt");
    }

    private void HandleDeath()
    {
        canAttack = false;

        GetComponent<BoxCollider2D>().enabled = false;

        animator.SetTrigger("dead");

        PlayHitParticle();
        StartCoroutine(DestroyEnemy(2));
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

    public void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);

        rigidbody.MovePosition(newPos);

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

    public void CheckPositionFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);

        if (distanceFromPlayer <= attackRange)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }

    public void StartChasing()
    {
        animator.SetBool("canChase", true);
    }

    private void Attack()
    {
        attackPosition = transform.position;
        attackPosition += transform.right * attackOffset.x;
        attackPosition += transform.up * attackOffset.y;

        Collider2D collisionInfo = Physics2D.OverlapCircle(attackPosition, attackSize, attackMask);

        if (collisionInfo != null)
        {
            collisionInfo.GetComponent<Health>().TakeDamage();
        }
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
}
