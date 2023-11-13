using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class CollisionController : MonoBehaviour
{
    private PlayerController player;

    private TMP_Text collisionField;

    private void Awake()
    {
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        collisionField = gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        collisionField.text = $"Collision:\n{player.Collision}";
    }
}
