using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography.X509Certificates;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button exitButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private bool paused;
    public bool isGameActive;
    private int score;
    private int lives;
    private float spawnRate = 1.0f;

    public AudioClip buttonClickSound;    
    private AudioSource buttonSound;
    public AudioClip goodItemSound;
    public AudioClip badItemSound;

    private void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangePaused();
        }
    }

    public void StarGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            restartButton.gameObject.SetActive(true);
            exitButton.gameObject.SetActive(true);
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            restartButton.gameObject.SetActive(false);
            exitButton.gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void LivesAdd()
    {
        if(lives!=0)
        {
            lives -= 1;
            livesText.text = "Lives: " + lives;
        }
        if (lives==0)
        {
            GameOver();
        }        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        isGameActive= false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }    

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SoundClick()
    {
        buttonSound.PlayOneShot(buttonClickSound);
    }
    public void ClickOnGoodItemSound()
    {
        buttonSound.PlayOneShot(goodItemSound, 4.0f);
    }

    public void ClickOnBadItemSound()
    {
        buttonSound.PlayOneShot(badItemSound, 4.0f);
    }
}
