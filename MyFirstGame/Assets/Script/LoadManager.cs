using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject[] neverDestroy;
    private bool isLoaded = false;

    public void LoadNextScene()
    {

        if (!isLoaded) 
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void searchTag(Transform game) 
    {
        if (game.tag == "MoveToScene")
        {
            game.SetParent(null);
            game.tag = "Untagged";
            SceneManager.MoveGameObjectToScene(game.gameObject, SceneManager.GetSceneAt(1));
            if (game.GetComponentInChildren<ScriptDoor>() != null)
                game.GetComponentInChildren<ScriptDoor>().Use();
        }

        if (game.childCount != 0)
        {
            foreach (Transform i in game.transform)
            {
                searchTag(i);
            }
        }
    }

    public void Remove() 
    {
        if (isLoaded) 
        {
            foreach (GameObject i in neverDestroy)
            {
                i.transform.SetParent(null);
                SceneManager.MoveGameObjectToScene(i, SceneManager.GetSceneAt(1));
            }

            GameObject[] allGame = SceneManager.GetActiveScene().GetRootGameObjects();

            foreach (GameObject gameObject in allGame)
            {
                searchTag(gameObject.transform);
            }

            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            isLoaded = false;
        }
    }

    public void FirstLoad() 
    {
        SceneManager.LoadScene(0);
    }
}
