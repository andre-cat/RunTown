using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("INITIAL ANIMATION")]
    [SerializeField] private Transform iniTransform;
    [SerializeField] private Transform endTransform;
    [SerializeField] private float animationSeconds;

    [Header("PARAMETERS")]
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text collision;

    private static readonly float normalSpeed = 15;

    private static bool gameOver;
    private static float speed;

    private float secondsElapsed;

    private PlayerController player;

    private void Awake()
    {
        speed = normalSpeed;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        StartCoroutine(RunInitialAnimation());
    }

    public static bool GameOver
    {
        get => gameOver; set => gameOver = value;
    }

    public static float Speed
    {
        get => speed;
    }

    public static void NormalizeSpeed() { speed = normalSpeed; }

    public static void DuplicateSpeed() { speed = normalSpeed * 2; }

    private IEnumerator RunInitialAnimation()
    {
        gameOver = true;
        score.gameObject.SetActive(false);
        collision.gameObject.SetActive(false);
        
        player.Animator.SetFloat("Speed_f", 0.3f);
        player.SplatterParticles.gameObject.SetActive(false);

        secondsElapsed = 0;
        
        while (secondsElapsed < animationSeconds)
        {
            iniTransform.position = Vector3.Lerp(iniTransform.position, endTransform.position, secondsElapsed / animationSeconds * Time.deltaTime);
            secondsElapsed += Time.deltaTime;
            yield return null;
        }
        
        player.Animator.SetFloat("Speed_f", 1f);
        player.SplatterParticles.gameObject.SetActive(true);

        score.gameObject.SetActive(true);
        collision.gameObject.SetActive(true);
        gameOver = false;
        
        yield break;
    }
}
