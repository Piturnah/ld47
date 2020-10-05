using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image deathScreen;
    public static int score;
    bool playerDied;

    private void Start() {
        score = 0;
        deathScreen.gameObject.SetActive(false);
        PlayerController.playerDied += OnPlayerDeath;
    }

    private void Update() {
        scoreText.text = score.ToString();
        if (playerDied) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                SceneManager.LoadScene(1);
            }
        }
    }

    void OnPlayerDeath() {
        PlayerController.playerDied -= OnPlayerDeath;
        deathScreen.gameObject.SetActive(true);
        playerDied = true;
    }
}
