using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : Singleton<PlayerManager>
{
    public GameObject player;
    PlayerStat playerStat;
    public GameObject gameOver;
    public GameObject healthBar;

    private void Start()
    {
        playerStat = player.GetComponent<PlayerStat>();
        playerStat.OnHealthReachedZero += Die;
    }

    // Die, die! :D
    public void Die()
    {
        Debug.Log("Game Over!");
        Cursor.lockState = CursorLockMode.None;
        gameOver.SetActive(true);
        healthBar.SetActive(false);

        StartCoroutine(ResetGame());
        
    }

    // currently we have only one scene, move the ui to another scene later
    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
   
}
