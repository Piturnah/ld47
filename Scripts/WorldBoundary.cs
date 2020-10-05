using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBoundary : MonoBehaviour
{
    public Vector2 dimensions;

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, dimensions);
    }
}
