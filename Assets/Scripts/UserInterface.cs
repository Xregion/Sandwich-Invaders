using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UserInterface : MonoBehaviour {

    public event Action pauseEvent;
    public Button mainMenu;
    public Button retry;
    public Text lives;
    public Text scoreText;
    public Text gameOverText;
    public Text highscoreText;

    CatController catInstance;
    EnemyMovement[] enemies;
    int score;
    bool paused;
    bool gameOver;

    // Use this for initialization
    void Start () {
        score = 0;
        paused = false;
        gameOver = false;
        catInstance = FindObjectOfType<CatController> ();
        UpdateLives ();
        catInstance.lifeEvent += UpdateLives;
        catInstance.deathEvent += GameOver;
        enemies = FindObjectsOfType<EnemyMovement> ();
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].hitEvent += UpdateScore;
        }
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            Pause ();
        }
    }

    void OnDisable () {
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].hitEvent -= UpdateScore;
        }
        catInstance.lifeEvent -= UpdateLives;
        catInstance.deathEvent -= GameOver;
    }

    public void StartGame () {
        Time.timeScale = 1;
        SceneManager.LoadScene (1);
        EnableAndDisableMenu (false);
        highscoreText.gameObject.SetActive (false);
    }

    public void GoBackToMainMenu () {
        SceneManager.LoadScene (0);
    }

    public void Pause () {
        if (!gameOver) {
            if (pauseEvent != null) {
                pauseEvent ();
            }
            paused = !paused;
            if (paused) {
                gameOverText.text = "Paused";
                EnableAndDisableMenu (true);
                Time.timeScale = 0;
            } else if (!paused) {
                gameOverText.text = "Game Over";
                EnableAndDisableMenu (false);
                Time.timeScale = 1;
            }
        }
    }

    void UpdateScore () {
        score += 100;
        scoreText.text = "Score: " + score;
    }

    void UpdateLives () {
        lives.text = "Lives: " + catInstance.GetLives ().ToString ();
    }

    void GameOver () {
        gameOver = true;
        EnableAndDisableMenu (true);
        int oldHighscore = PlayerPrefs.GetInt ("highscore");
        int newHighscore = score;
        if (newHighscore > oldHighscore) {
            PlayerPrefs.SetInt ("highscore", newHighscore);
            highscoreText.gameObject.SetActive (true);
        }
    }

    void EnableAndDisableMenu (bool on) {
        mainMenu.gameObject.SetActive (on);
        gameOverText.gameObject.SetActive (on);
        retry.gameObject.SetActive (on);
    }
}
