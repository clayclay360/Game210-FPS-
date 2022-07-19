using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Game Varaibles")]
    public int wave;
    public int enemyCount;
    public bool gameStarted, roundOver;

    public GameObject enemyPrefab;
    public Transform[] enemySpawns;
    
    private UIController uiController;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(enemyCount == 0 && gameStarted && roundOver)
        {
            StartCoroutine(GameLoop());
            roundOver = false;
        }
    }

    IEnumerator GameLoop()
    {
        if (enemyCount == 0 && gameStarted)
        {
            yield return new WaitForSeconds(0.75f);
            uiController.waveText.text = "Wave " + wave.ToString();
            playerController.isHealing = true;

            yield return new WaitForSeconds(1.25f);
            uiController.waveText.text = "";
            enemyCount += 2 + wave;
            playerController.isHealing = false;
            playerController.lives = 10;

            for (int i = 0; i < enemyCount; i++)
            {
                int randomNumber = Random.Range(0, enemySpawns.Length);
                Instantiate(enemyPrefab, enemySpawns[randomNumber].position, Quaternion.identity);
                yield return new WaitForSeconds(1);
            }

            roundOver = true;
            wave++;
        }
    }
}
