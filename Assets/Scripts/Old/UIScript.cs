using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] BotMovement bot;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject failedPanel;
    [SerializeField] GameObject joystick;
    [SerializeField] Text winCoins;
    [SerializeField] Text faildCoins;
    [SerializeField] Text coins;
    [SerializeField] List<GameObject> liguesLevel = new List<GameObject>();
    [SerializeField] List<GameObject> ligues = new List<GameObject>();
    [SerializeField] GameObject vibrationOn;
    [SerializeField] GameObject vibrationOff;
    public int levelNumber = 0;
    public int countCoins = 0;
    public int countLevels = 1;
    public int loadLevelLigue = 0;
    public int loadLigue = 0;
    public int vibration = 1;
    
    
    void Start()
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex;
        if ((PlayerPrefs.HasKey("Vibration") || PlayerPrefs.HasKey("Coins") || PlayerPrefs.HasKey("Levels")) && levelNumber == 0)
        {
            coins.text = PlayerPrefs.GetInt("Coins").ToString();
            vibration = PlayerPrefs.GetInt("Vibration");
            countLevels = PlayerPrefs.GetInt("Levels");

            loadLevelLigue = countLevels % 5;
            loadLigue = countLevels / 6;
            foreach (var level in liguesLevel)
            {
                level.SetActive(false);
            }
            foreach (var ligue in ligues)
            {
                ligue.SetActive(false);
            }
            if (loadLevelLigue == 0)
            {
                liguesLevel[4].SetActive(true);
            }
            else
            {
                liguesLevel[loadLevelLigue - 1].SetActive(true);
            }
            
            ligues[loadLigue].SetActive(true);

            if (vibration == 0)
            {
                vibrationOff.SetActive(true);
                vibrationOn.SetActive(false);
            }

            if (vibration == 1)
            {
                vibrationOff.SetActive(false);
                vibrationOn.SetActive(true);
            }
        }

        if (PlayerPrefs.HasKey("Vibration") && levelNumber != 0)
        {
            
            vibration = PlayerPrefs.GetInt("Vibration");
        }

        if (PlayerPrefs.HasKey("Levels") && levelNumber != 0)
        {
            countLevels = PlayerPrefs.GetInt("Levels");
        }

        if (PlayerPrefs.HasKey("Coins") && levelNumber != 0)
        {
            countCoins = PlayerPrefs.GetInt("Coins");
        }

        else
        {
            vibration = 1;
            countLevels = 1;
        }
    }

    
    void Update()
    {
      // if (player.failed && !bot.win)
      // {
      //     failedPanel.SetActive(true);
      //     joystick.SetActive(false);
      //     faildCoins.text = "10";
      // }
      //
      // if (bot.win && !player.failed)
      // {
      //     winPanel.SetActive(true);
      //     joystick.SetActive(false);
      //     winCoins.text = "100";
      // }
    }

    public void NextLevel()
    {
        countCoins += 100;
        countLevels++;
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.SetInt("Levels", countLevels);
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelNumber);
    }

    public void RetryLevel()
    {
        countCoins += 10;
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.Save();
        SceneManager.LoadScene(levelNumber);
    }

    public void StartGame()
    {
        countLevels = PlayerPrefs.GetInt("Levels");
        SceneManager.LoadScene(levelNumber + 1);
    }

    public void VibrationOn()
    {
        vibration = 0;
        PlayerPrefs.SetInt("Vibration", vibration);
        PlayerPrefs.Save();
    }

    public void VibrationOff()
    {
        vibration = 1;
        PlayerPrefs.SetInt("Vibration", vibration);
        PlayerPrefs.Save();
    }

    public void ToMenuRetry()
    {
        countCoins += 10;
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void ToMenuNext()
    {
        countCoins += 100;
        countLevels++;
        PlayerPrefs.SetInt("Coins", countCoins);
        PlayerPrefs.SetInt("Levels", countLevels);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }
}
