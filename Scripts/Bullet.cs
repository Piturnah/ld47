using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocity;
    WorldBoundary boundary;

    private void Start() {
        boundary = FindObjectOfType<WorldBoundary>();
        StartCoroutine(SelfDestruct());
    }

    private void Update() {
        transform.Translate(Vector2.up * velocity * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 0.5f * boundary.dimensions.x) {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
        if (Mathf.Abs(transform.position.y) > 0.5f * boundary.dimensions.y) {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
    }

    IEnumerator SelfDestruct() {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
