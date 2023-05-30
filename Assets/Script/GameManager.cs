using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject gameOver;
    private void Awake()
    {
        Instance = this;
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
    }
}
