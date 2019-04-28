using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour
{
    public AudioClip coinSound;

    private AudioSource _audioSource;
    private LevelManager _levelManager = null;

    private bool alreadyCollected = false;

    public void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.GetComponent<Player>() != null) && (!alreadyCollected))
        {
            alreadyCollected = true;
            _levelManager.AddCoin();
            collision.gameObject.GetComponentInChildren<AudioSource>().PlayOneShot(coinSound);
            //_audioSource.PlayOneShot(coinSound);
            Destroy(this.gameObject);
        }
    }
}
