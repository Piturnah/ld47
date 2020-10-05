using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    Vector2 acceleration;
    Vector2 velocity;
    public float accelerationMultiplier;
    public float maxVelocity;

    public GameObject bullet;

    float lastLoadSecs;
    float timeBtwShotsSecs = 0.5f;
    public int storedShots;

    public static event Action playerDied;

    private void Start() {
        velocity = Vector2.zero;
        Vector3[] points = { new Vector3(0, 0.5f, 0), new Vector3(0.5f, -1, 0), new Vector3(0, -0.5f, 0), new Vector3(0.5f, -1, 0) };
        //gameObject.GetComponent<MeshCollider>().sharedMesh = MeshGenerator.GeneratePolygonMesh(points);
    }

    private void Update() {
        UpdateMovement();
        DetectFiringInput();
    }

    private void OnCollisionEnter(Collision collision) {
        playerDied?.Invoke();
        Destroy(gameObject);
    }

    void DetectFiringInput() {
        if (Time.time >= lastLoadSecs + timeBtwShotsSecs) {
            storedShots = Mathf.Clamp(storedShots + 1, 0, 5);
            lastLoadSecs = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Space) && storedShots > 0) {
            storedShots--;
            Instantiate(bullet, transform.position, (Mathf.Sign(transform.localScale.y) == 1)?transform.rotation:Quaternion.Euler(Vector3.forward * (transform.eulerAngles.z + 180)));
        }
    }

    void UpdateMovement() {
        if (Input.GetKeyDown(KeyCode.W)) { transform.localScale = Vector3.one * 0.5f; }
        if (Input.GetKeyDown(KeyCode.S)) { transform.localScale = new Vector3(0.5f, -0.5f, 0.5f); }

        acceleration = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * accelerationMultiplier;
        velocity += acceleration;
        velocity = new Vector2(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity), Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity));

        velocity.y += (Mathf.Sign(velocity.y) == 1) ? -3 * Time.deltaTime : 3 * Time.deltaTime;
        velocity.x += (Mathf.Sign(velocity.x) == 1) ? -3 * Time.deltaTime : 3 * Time.deltaTime;

        transform.Translate(Vector2.up * velocity.y * Time.deltaTime, Space.Self);
        transform.parent.Rotate(Vector3.forward * -velocity.x * Time.deltaTime * (1 / transform.position.magnitude) * Mathf.Rad2Deg);

        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, 0.4f, 6.6f), 0);
    }
}
