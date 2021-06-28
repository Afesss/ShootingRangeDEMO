using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject restart;
    [SerializeField] private GameObject stik;
    [SerializeField] private GameObject shot;

    [Inject] private GameOver gameOver;
    private void Start()
    {
        restart.SetActive(false);
        gameOver.OnGameOver += GameOver_OnGameOver;
    }

    private void GameOver_OnGameOver()
    {
        gameOver.OnGameOver -= GameOver_OnGameOver;
        restart.SetActive(true);
        shot.SetActive(false);
        stik.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
