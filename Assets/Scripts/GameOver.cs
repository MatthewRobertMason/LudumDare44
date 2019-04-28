using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public string loadLevel = "MenuScene";
    public Text gameOverMessage = null;
    public GameManager gm = null;

    public void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (gameOverMessage != null)
        {
            switch (gm.coinsCollected)
            {
                case 10:
                    gameOverMessage.text = "As you were in life you are in death.  You are overcome with GREED, truely you have spent your life in a foolish endevor.  Perhaps you should have stoped persuing Treasure all the time and enjoyed some nature.";
                    break;

                case 9:
                case 8:
                    gameOverMessage.text = "Did you miss one coin or two, surely you'll try again.  After all, thats the goal isn't it? greed?";
                    break;

                case 7:
                case 6:
                case 5:
                    gameOverMessage.text = "You missed " + (10 - gm.coinsCollected).ToString() + " coins, surely you meant to collect them all? I thought you more greedy than that.";
                    break;

                case 4:
                case 3:
                case 2:
                    gameOverMessage.text = "So few coins, perhaps you meant to do more with your life? or maybe you've started to overcome your greed?";
                    break;

                case 1:
                    gameOverMessage.text = "Take only what you need is a good motto to live by, you're almost there.";
                    break;

                case 0:
                    gameOverMessage.text = "I see you have overcome your greed, well done.  I'll return you to the world of the living this time.  Always remember to enjoy the time you have, your life is the greatest currency you have.";
                    break;
            }

            gameOverMessage.text += "\r\n\r\nCoins Collected: " + gm.coinsCollected + "\r\nLives paid for: " + (Mathf.Max(gm.timesDied-1, 0.0f));
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(loadLevel);
    }
}
