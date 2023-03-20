using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUi : MonoBehaviour
{
    #region Exposed

    [SerializeField] GameObject gameLoseUI;
    [SerializeField] GameObject gameWinUI;

    [SerializeField] Guard _guard;
    [SerializeField] PlayerMotor _pm;

    #endregion

    #region Unity Lifecycle

    void Start()
    {
        //Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        //FindObjectOfType<Player>().OnReachedEndOfLevel += ShowGameWinUI;
    }

    void Update()
    {
        if (_guard._lose)
        {
            OnGameOver(gameLoseUI);
            Time.timeScale = 0f;
        }
        if (_pm._win)
        {
            OnGameOver(gameWinUI);
            Time.timeScale = 0f;
        }
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
            }
        }
    }

    #endregion

    #region Methods

    void ShowGameWinUI()
    {
        OnGameOver(gameWinUI);
    }

    void ShowGameLoseUI()
    {
        OnGameOver(gameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        //Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        //FindObjectOfType<Player>().OnReachedEndOfLevel -= ShowGameWinUI;
    }

    #endregion

    #region Private & Protected

    private bool gameIsOver;

    #endregion
}
