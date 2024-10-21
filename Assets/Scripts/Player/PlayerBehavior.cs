using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private ParticleSystem hitParticle;
    [Header("Propriedades de ataque")]
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private LayerMask attackLayer;
    private float moveDirection;
    private new Rigidbody2D rigidbody;
    private IsGroundedChecker isGroundedChecker;
    private Health health;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        health = GetComponent<Health>();

        health.OnDead += HandlePlayerDeath;
        health.OnHurt += HundleHurt;
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnJump += HandleJump;

        UpdateLives(health.GetLives());
    }

    private void FixedUpdate()
    {
        MovePlayer();
        FlipSpriteAccordingToMoveDirection();
    }

    private void MovePlayer()
    {
        moveDirection = GameManager.Instance.InputManager.Movement;

        //transform.Translate(moveDirection * Time.deltaTime * moveSpeed, 0, 0);

        Vector2 directionToMove = new(moveDirection * moveSpeed, rigidbody.velocity.y);

        rigidbody.velocity = directionToMove;
    }

    private void FlipSpriteAccordingToMoveDirection()
    {
        if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void HandleJump()
    {
        Debug.Log("Estou pulando!");

        if (isGroundedChecker.IsGrounded() == false) return;

        rigidbody.velocity += Vector2.up * jumpForce;

        GameManager.Instance.AudioManager.PlaySFX(SFX.playerJump);
    }

    private void HundleHurt()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.playerHurt);

        UpdateLives(health.GetLives());

        PlayHitParticle();
    }

    private void UpdateLives(int amount)
    {
        GameManager.Instance.UpdateLives(amount);
    }

    private void HandlePlayerDeath()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.playerDeath);

        GetComponent<Collider2D>().enabled = false;

        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        GameManager.Instance.InputManager.DisablePlayerInput();

        UpdateLives(health.GetLives());

        PlayHitParticle();
    }

    private void PlayWalkSound()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.playerWalk);
    }

    private void Attack()
    {
        GameManager.Instance.AudioManager.PlaySFX(SFX.playerAttack);

        Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, attackLayer);

        print("Making enemy take damage");
        print(hittedEnemies.Length);

        foreach (Collider2D hittedEnemy in hittedEnemies)
        {
            print("Checking enemy");

            if (hittedEnemy.TryGetComponent(out Health enemyHealth))
            {
                print("Getting damage");

                enemyHealth.TakeDamage();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
    }
}
