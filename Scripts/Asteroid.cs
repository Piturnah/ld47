using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleRenderer))]
public class Asteroid : MonoBehaviour
{
    WorldBoundary boundary;
    CircleRenderer rend;
    Vector2 velocity;
    public int iteration = 0;
    int velocityMultiplier = 2;

    private void Start() {
        
        boundary = FindObjectOfType<WorldBoundary>();
        rend = GetComponent<CircleRenderer>();
        rend.radius = rend.radius / (iteration + 2);

        float velocityMagnitude = velocityMultiplier * (iteration + 1);
        velocity = new Vector2(Random.Range(-velocityMagnitude, velocityMagnitude), Random.Range(-velocityMagnitude, velocityMagnitude));

        gameObject.GetComponent<MeshCollider>().sharedMesh = MeshGenerator.GeneratePolygonMesh(rend.circlePoints);
    }

    private void Update() {
        CalculateMovement();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Bullet") {
            Destroy(collision.collider.gameObject);
            if (iteration < 2) {
                for (int i = 0; i < 2; i++) {
                    GameObject childAsteroid = Instantiate(gameObject, transform.position, Quaternion.identity);
                    Asteroid childScript = childAsteroid.GetComponent<Asteroid>();
                    childScript.iteration = iteration + 1;

                }

            }
            UIManager.score++;
            Destroy(gameObject);
        }
    }

    void CalculateMovement() {
        transform.Translate(velocity * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 0.5f * boundary.dimensions.x) {
            transform.position = new Vector2(-transform.position.x, transform.position.y);
        }
        if (Mathf.Abs(transform.position.y) > 0.5f * boundary.dimensions.y) {
            transform.position = new Vector2(transform.position.x, -transform.position.y);
        }
    }
}
