using UnityEngine;
using System.Collections;

// A singleton Game Manager, which manages the game.
public class GameManager : MonoBehaviour 
{
	public Transform _MyShipTransform;
	public int _Lives = 3;
	public float _GamePlayWidth = 50f;	// Width is defined in precentage of screen width.
	public ObjectPool _EnemyPool;
	public ObjectPool _EnemyBulletPool;
	public ObjectPool _MyBulletPool;
	public GameObject _MainMenu;
	public GameObject _GameHUD;
	public GameObject _UiGameOver;


	public delegate void GameOverEvent();
	public GameOverEvent gameOver;

	// Used to define the game area
	private Rect mGameRect = new Rect ();
	public Rect pGameRect 
	{ 
		get { return mGameRect; }
	}

	// Number of lives the player has
	private int mLives;
	public int pLives
	{
		get { return mLives; }
	}

	private int mScore = 0;
	public int pScore
	{
		get { return mScore; }
	}

	private bool mIsGamePaused = true;
	public bool pIsGamePaused
	{
		get { return mIsGamePaused; }
	}

	private static GameManager mInstance;
	public static GameManager pInstance
	{
		get { return mInstance; }
	}
	
	void Awake () 
	{
		//  Singleton implementation of game manager.
		if (mInstance == null) 
		{
			mInstance = this;
		} 
		else 
		{
			Destroy (gameObject);
		}
	}

	void Start()
	{
		UpdateGameRect ();
	}
	
	private void UpdateGameRect()
	{
		_GamePlayWidth = _GamePlayWidth / 100f;
		_GamePlayWidth = Mathf.Clamp (_GamePlayWidth, 0f, 1f);
		
		mGameRect.x = (Screen.width - _GamePlayWidth * Screen.width) / 2f;
		mGameRect.width = Screen.width * _GamePlayWidth;
		mGameRect.y = 0;
		mGameRect.height = Screen.height;
	}

	public void StartNewGame()
	{
		ResetGame ();
		mIsGamePaused = false;
	}

	public void PauseGame(bool pause)
	{
		mIsGamePaused = pause;
	}

	void ResetGame()
	{
		mLives = _Lives;
		mScore = 0;
	}

	public void AddScore(int score)
	{
		mScore += score;
	}

	// Deduct lives on being hit and trigger game over once lives reaches 0.
	public void ShipHit()
	{
		mLives--;
		if (mLives <= 0) 
		{
			GameOver();
		}
	}

	private void GameOver()
	{
		mIsGamePaused = true;
		if(_GameHUD != null)
			_GameHUD.SetActive (false);
		if(_UiGameOver != null)
			_UiGameOver.SetActive (true);

		if(gameOver != null)
			gameOver();
	}
}
