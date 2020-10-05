using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleRenderer))]
public class PlayerCircle : MonoBehaviour
{
    Transform player;
    PlayerController playerScript;

    CircleRenderer rend;

    bool playerDead;

    private void Start() {
        player = transform.GetChild(0);
        playerScript = player.GetComponent<PlayerController>();
        rend = GetComponent<CircleRenderer>();

        PlayerController.playerDied += OnPlayerDied;
    }
    private void Update() {
        if (!playerDead) {
            float radius = (transform.position - player.position).magnitude;
            rend.RenderCircle(radius, 0.05f, 100, transform.position, Mathf.Lerp(0.2f, 0, playerScript.storedShots / 5f) / radius);
        }
    }

    void OnPlayerDied() {
        playerDead = true;
    }
}
