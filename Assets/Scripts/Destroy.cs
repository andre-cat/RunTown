using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    [SerializeField] private string boundsString;

    private float minX, maxX;
    private float minZ, maxZ;

    private void Awake()
    {
        Renderer boundary = GameObject.FindGameObjectWithTag(boundsString).GetComponent<Renderer>();

        minX = boundary.gameObject.transform.position.x - boundary.bounds.size.x / 2f;
        maxX = boundary.gameObject.transform.position.x + boundary.bounds.size.x / 2f;

        minZ = boundary.gameObject.transform.position.z - boundary.bounds.size.z / 2f;
        maxZ = boundary.gameObject.transform.position.z + boundary.bounds.size.z / 2f;
    }

    private void FixedUpdate()
    {
        if (!ImInBounds())
        {
            Destroy(gameObject);
        }
    }

    private bool ImInBounds()
    {
        return
        transform.position.x >= minX &&
        transform.position.x <= maxX &&
        transform.position.z >= minZ &&
        transform.position.z <= maxZ;
    }
}
