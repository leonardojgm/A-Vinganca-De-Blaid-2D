using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource))]
public abstract class BaseEnemy : MonoBehaviour
{
    protected Animator animator;
    protected Health health;
    protected AudioSource audioSource;
    protected bool canAttack = true;
    [SerializeField] private ParticleSystem hitParticle;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();

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
}
