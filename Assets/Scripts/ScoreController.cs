using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreController : MonoBehaviour
{
    private PlayerController player;

    private TMP_Text scoreField;

    private void Awake()
    {
        player =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        scoreField = gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        scoreField.text = $"{player.Score:0} seconds";
    }
}
