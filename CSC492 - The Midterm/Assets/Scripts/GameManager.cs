using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections.Generic;       //Allows us to use Lists.

public class GameManager : MonoBehaviour
{
  //Static instance of GameManager which allows it to be accessed by any other script.
  public static GameManager instance = null;

  // the text label that shows the player how they're doing as far as collecting
  private Text state_label;

  private GameObject player;
  private GameObject spawn;

  // represents the index of the current scene
	private int sceneIndex;
	//number of total scenes
	public int scenes;

  //Awake is always called before any Start functions
  void Awake()
  {
    //Check if instance already exists
    if (instance == null)
        //if not, set instance to this
        instance = this;

    //If instance already exists and it's not this:
    else if (instance != this)
        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        Destroy(gameObject);

    //Sets this to not be destroyed when reloading scene
    DontDestroyOnLoad(gameObject);

    scenes = SceneManager.sceneCountInBuildSettings;

    //Call the InitGame function to initialize the first level
    InitGame();

  }

  //Initializes the game for each level.
  void InitGame()
  {
    SceneManager.sceneLoaded += stageInit;
    sceneIndex = SceneManager.GetActiveScene().buildIndex;
  }

  // Called when a new scene is loaded
  void stageInit(Scene scene, LoadSceneMode mode)
  {
    sceneIndex = SceneManager.GetActiveScene().buildIndex;

  }

  // Attempt to move onto the next level.
  private void nextLevel()
  {
    sceneIndex++;
		loadLevel(sceneIndex);
  }

  // load a specified scene/ level
  private void loadLevel(int scene){
    if (scene < scenes && scene >= 0) {
      SceneManager.LoadScene(scene);
    }else Debug.Log("oops, scene " +scene+ " doesnt exist. Actually we have only "+scenes+ " scenes.");
  }

  //load level by scene name instead of index.
  public void loadLevel(String name){
    if(SceneManager.GetSceneByName(name) != null){
      SceneManager.LoadScene(name);
    }
  }

  // load the game over scene
  public void gameOver(){
    loadLevel("Game_Over");
  }

  public void toggleAllMats(){
    GameObject[] parentObjects = GameObject.FindGameObjectsWithTag("textured");
    if(parentObjects.Length>0){
      foreach(GameObject parent in parentObjects){
        parent.GetComponent<toggleMaterial>().toggleMat();
      }
    }
  }
}