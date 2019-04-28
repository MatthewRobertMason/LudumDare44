using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    private LevelManager _levelManager = null;

    // Start is called before the first frame update
    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _levelManager.AddDeath();
            player.Respawn();
        }
    }
}
