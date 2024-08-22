using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] GameObject panel_credits;

    void Start()
    {
        Time.timeScale = 1;
        score.text = PlayerPrefs.GetInt("Score").ToString();
    }
    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        panel_credits.SetActive(true);
    }
    public void CloseCredits()
    {
        panel_credits.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
