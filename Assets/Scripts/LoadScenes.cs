using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {
    float delayInsSeconds = 2f;

    public void SceneLoad(string scene)
    {
        SceneManager.LoadScene(scene);
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameoverScene()
    {
        StartCoroutine(GameOverDelay());
       
    }
    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(delayInsSeconds);
        SceneManager.LoadScene("Lose");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
