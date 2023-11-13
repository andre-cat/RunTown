using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RepeatBackground : MonoBehaviour
{

    //[SerializeField] private float speed;

    private Vector3 iniPosition;

    private Renderer _renderer;

    private void Awake()
    {
        iniPosition = transform.position;

        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.GameOver)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.left * GameManager.Speed * Time.deltaTime);

    }

    private void Update()
    {
        Repeat();
    }

    private void Repeat()
    {
        if (transform.position.x < iniPosition.x - _renderer.bounds.size.x / 2)
        {
            transform.position = iniPosition;
        }
    }
}
