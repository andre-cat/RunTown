using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    //[SerializeField] private float speed;
    [SerializeField] private Axis axis;
    [SerializeField] private Sign sign;

    private void FixedUpdate()
    {
        if (!GameManager.GameOver)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 direction = new(0, 0, 0);

        switch (axis)
        {
            case Axis.X:
                direction = Vector3.right * (int)sign;
                break;
            case Axis.Y:
                direction = Vector3.up * (int)sign;
                break;
            case Axis.Z:
                direction = Vector3.forward * (int)sign;
                break;
        }

        transform.Translate(direction * Mathf.Abs(GameManager.Speed) * Time.deltaTime);
    }

    private enum Sign
    {
        Positive = 1,
        Negative = -1
    }


    private enum Axis
    {
        X,
        Y,
        Z
    }

}
