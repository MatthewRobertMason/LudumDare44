using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DeathEndLevel : MonoBehaviour
{
    public GameObject coinObject;
    public Transform locationForCoins;
    public AudioClip coinSound;

    private Player _player = null;
    private LevelManager _levelManager = null;

    private bool countCoins = false;
    private float counter = 0.0f;
    private int numberOfCoins;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            numberOfCoins = FindObjectOfType<GameManager>().Coins;

            _player.PreventMovement = true;
            

            _levelManager.initialConversation = new string[]{ "d: YOU WILL NOW BE JUDGED" };
            _levelManager.ResetConversation();
            
            countCoins = true;
        }
    }

    private void Update()
    {
        if (countCoins)
        {
            if (numberOfCoins > 0)
            {
                if (counter > 1.0f)
                {
                    counter -= 1.0f;

                    GameObject go = Instantiate(coinObject, locationForCoins);
                    Rigidbody2D rigidBody = go.AddComponent<Rigidbody2D>();
                    rigidBody.AddForce(Vector2.up * 500.0f);
                    
                    Destroy(go, 1.0f);

                    _player.GetComponentInChildren<AudioSource>().PlayOneShot(coinSound);

                    numberOfCoins--;
                }
            }

            if (numberOfCoins <= 0 && counter >= 3.0f)
            {
                _levelManager.GameOver = true;
            }

            counter += Time.deltaTime;
        }
    }
}
