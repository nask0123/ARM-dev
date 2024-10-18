using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
	public GameObject pauseMenu;
	public static bool isPaused = false;
	
	
	void Start() 
	{
		pauseMenu.SetActive(false);
		
	}
	
	void Update() 
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			if (isPaused)	
			{
				Resume();
			}
			else 
			{
				Paused();
			}
		}
		
	}
	
	public void Resume() 
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1f;
		isPaused = false;
		
	}
	
	void Paused() 
	{
		pauseMenu.SetActive(true);
		Time.timeScale = 0f;
		isPaused = true;
	}
	
	public void LoadScene() {
		SceneManager.LoadScene("MainMenu");
	}
	
	public void QuitGame() {
		Debug.Log("QUITIING!");
		Application.Quit();
	}
}
