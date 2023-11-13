using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{

    [Header("Parameters")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip crashSound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem explotionParticles;
    [SerializeField] private ParticleSystem splatterParticles;

    #region privatecomponents
    private Animator animator;
    private Rigidbody body;
    private AudioSource audioSource;


    #endregion privatecomponents

    #region input
    private bool jumpInput;
    private bool dashInput;
    #endregion input

    private readonly int jumpLimit = 2;
    private int jumps;

    private float score;
    private string collision;

    private void Awake()
    {
        body = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        jumpInput = false;

        jumps = 0;

        Physics.gravity *= gravity;
    }

    private void Update()
    {
        if (!GameManager.GameOver)
        {
            TrackInput();
            Move();
        }
    }

    private void Move()
    {
        if (jumpInput && jumps < jumpLimit)
        {
            splatterParticles.Stop();
            animator.SetTrigger("Jump_trig");
            audioSource.PlayOneShot(jumpSound, 1f);
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumps++;
        }

        if (dashInput)
        {
            score += Time.deltaTime * 2;
            GameManager.DuplicateSpeed();
            animator.SetFloat("Speed", 2f);
            body.AddForce(Vector3.down * gravity, ForceMode.Force);
        }
        else
        {
            score += Time.deltaTime;
            GameManager.NormalizeSpeed();
            animator.SetFloat("Speed", 1f);
        }
    }

    private void TrackInput()
    {
        jumpInput = Input.GetButtonDown("Jump");
        dashInput = Input.GetButton("Dash");
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.collision = collision.gameObject.tag;

        if (collision.gameObject.CompareTag("Ground"))
        {
            splatterParticles.Play();
            jumps = 0;
        }

        /* for obstacles using rigidbody
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.GameOver = true;
            audioSource.PlayOneShot(crashSound, 0.5f);
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            explotionParticles.Play();
            Debug.Log("GameOver");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
        */
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            GameManager.GameOver = true;
            audioSource.PlayOneShot(crashSound, 0.5f);
            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
            explotionParticles.Play();
            Debug.Log("GameOver");
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    public float Score
    {
        get => score;
    }

    public string Collision
    {
        get => collision;
    }

    public Animator Animator
    {
        get => animator;
    }

    public ParticleSystem SplatterParticles
    {
        get => splatterParticles;
    }
}
