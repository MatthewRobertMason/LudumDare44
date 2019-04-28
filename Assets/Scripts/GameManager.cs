using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int coinsCollected = 0;
    public int timesDied = 0;

    public bool _gameOver = false;
    private bool _initialized = false;

    public int Coins
    {
        get { return coinsCollected - timesDied; }
    }

    public bool GameOver
    {
        get { return _gameOver; }
        set { _gameOver = value; }
    }

    private void Awake()
    {
        foreach (GameManager gm in FindObjectsOfType<GameManager>())
        {
            if (gm._initialized == true)
            {
                if (gm != this)
                {
                    Debug.Log("GameManager already exists");
                    DestroyImmediate(this.gameObject);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _initialized = true;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Coins <= -1)
        {
            _gameOver = true;
        }
    }

    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}
