using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    void Start()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
