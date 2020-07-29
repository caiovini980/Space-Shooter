using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to text
    [SerializeField] private Text _scoreText;
    [SerializeField] private Sprite[] _liveSprites;
    [SerializeField] private Image _livesImage;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _shoot;
    [SerializeField] private GameObject _move;
    [SerializeField] private GameObject _restartText;
    [SerializeField] private GameObject _returnMenuText;
    private bool _restartGame = true;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //assign text component to the handle
        _scoreText.text = "Score: " + 0;
        _gameOver.SetActive(false);
        _shoot.SetActive(true);
        _move.SetActive(true);
        _restartText.SetActive(false);
        _returnMenuText.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        //access the display image sprite
        //give it a new one based on the currentLives index

        _livesImage.sprite = _liveSprites[currentLives];
        
        if(currentLives <= 0)
        {
            GameOverSequence();
        }
    }

    public void UpdateInstructions()
    {
        _shoot.SetActive(false);
        _move.SetActive(false);
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _restartGame = false;
        _restartText.SetActive(true);
        _returnMenuText.SetActive(true);
        StartCoroutine(GameOverScreen());  
    }

    IEnumerator GameOverScreen()
    {
        while (_restartGame == false)
        {
            _gameOver.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOver.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

}
