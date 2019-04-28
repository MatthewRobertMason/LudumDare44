using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject[] initialDontDestroyOnLoad;

    private bool deleteGameObjects = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (deleteGameObjects)
        {
            foreach (GameManager go in FindObjectsOfType<GameManager>())
            {
                Destroy(go.gameObject, 0.25f);
            }
        }
    }

    public void StartGame()
    {
        deleteGameObjects = false;
        foreach (GameObject go in initialDontDestroyOnLoad)
        {
            GameObject g = Instantiate(go);
            DontDestroyOnLoad(g);
        }

        SceneManager.LoadScene("Level1");
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
