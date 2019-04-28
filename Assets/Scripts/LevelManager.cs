using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Transform respawnLocation = null;

    public string[] initialConversation;
    private bool _conversationHappening = true;
    private int _conversationLevel = 0;

    public string nextLevel = "";
    private string gameOverLevel = "GameOver";

    private Player _player = null;
    private GameManager _gameManager = null;
    private Overlay _overlay = null;
    
    private Image _playerImage;
    private Image _deathImage;
    private Text _conversationText;
    private Canvas _talkingCanvas = null;
    
    public Player PlayerObject
    {
        get { return _player; }
    }

    public Overlay GameOverlay
    {
        get { return _overlay; }
    }

    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = FindObjectOfType<RespawnLocation>().transform;
        _player = FindObjectOfType<Player>();
        _gameManager = FindObjectOfType<GameManager>();
        _overlay = FindObjectOfType<Overlay>();

        _playerImage = _overlay._playerImage;
        _deathImage = _overlay._deathImage;
        _conversationText = _overlay._conversationText;
        _talkingCanvas = _overlay._talkingCanvas;

        PlayerObject.PreventMovement = true;
    }

    public void ResetConversation()
    {
        _conversationLevel = 0;
        _talkingCanvas.gameObject.SetActive(true);

        _conversationHappening = true;
    }

    public int Coins
    {
        get
        {
            return _gameManager.Coins;
        }
    }

    public bool GameOver
    {
        get { return _gameManager.GameOver; }
        set { _gameManager.GameOver = true; }
    }

    public void AddCoin()
    {
        _gameManager.coinsCollected += 1;
    }

    public void AddDeath()
    {
        _gameManager.timesDied += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_conversationLevel >= initialConversation.Length) || (initialConversation.Length == 0))
        {
            _conversationHappening = false;
            _talkingCanvas.gameObject.SetActive(false);
            PlayerObject.PreventMovement = false;
        }

        if (_conversationHappening && _conversationLevel < initialConversation.Length)
        {
            string s = initialConversation[_conversationLevel];

            if (s.Substring(0,2) == "p:")
            {
                _playerImage.gameObject.SetActive(true);
                _deathImage.gameObject.SetActive(false);

                _conversationText.text = s.Substring(3);
            }
            else if (s.Substring(0, 2) == "d:")
            {
                _playerImage.gameObject.SetActive(false);
                _deathImage.gameObject.SetActive(true);

                _conversationText.text = s.Substring(3);
            }
        }

        if (_conversationHappening && Input.GetButtonDown("Jump"))
        {
            _conversationLevel++;
        }

        GameOverlay.coinDisplay.text = Coins.ToString();

        if (GameOver)
        {
            _gameManager.LoadScene(gameOverLevel);
        }
    }

    public void NextLevel()
    {
        _gameManager.LoadScene(nextLevel);
    }
}
