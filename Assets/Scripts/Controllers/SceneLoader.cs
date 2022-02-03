using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void LoadLevel()
    {
        var index = PlayerPrefs.GetInt("CurrentArena", 1);
        SceneManager.LoadScene(index);
    }

    public void LoadShop(int index)
    {
        SceneManager.LoadScene(index);

    }
}
