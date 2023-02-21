using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

	public void LoadGame()
    {
        SceneManager.LoadScene("Level1");
        FindObjectOfType<GameSession>().ResetGame();
    }
      
    public void LoadGameOver()
    {
        StartCoroutine(gameOverDelay());
        
    }
    
    IEnumerator gameOverDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
