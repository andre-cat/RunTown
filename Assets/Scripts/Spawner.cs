using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnables;
    [SerializeField] private Transform parent;
    [SerializeField] private float startDelay = 10;
    [SerializeField] private float timeBetween = 5;

    private void Start()
    {
        if (spawnables.Length > 0)
        {
            InvokeRepeating(nameof(Spawn), startDelay, timeBetween);
        }
    }

    private void Spawn()
    {
        if (!GameManager.GameOver)
        {
            GameObject spawnable = spawnables[Random.Range(0, spawnables.Length)];
            Instantiate(spawnable,parent.position, parent.rotation, parent);
        }
    }
}