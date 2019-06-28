using UnityEngine;
using System.Collections;

// This is a basic enemy generator. It spawns enemies at the top of the screen at random intervals within a time range.
public class EnemyManager : MonoBehaviour 
{
	public Vector2 _SpawnTimeRange;
	public float _SpawnHeight = 10f;

	private float mTimer;

	private static EnemyManager mInstance;
	public static EnemyManager pInstance
	{
		get { return mInstance; }
	}

	void Awake()
	{
		// Singleton implementation for EnemyManager
		if (mInstance == null) 
		{
			mInstance = this;
		} 
		else 
		{
			Destroy(gameObject);
		}
	}

	void Start()
	{
		GameManager.pInstance.gameOver += GameOver;
		ResetTimer ();
	}

	void Update()
	{
		if (!GameManager.pInstance.pIsGamePaused) 
		{
			mTimer -= Time.deltaTime;
			if (mTimer <= 0) 
			{
				SpawnEnemy ();
			}
		}
	}

	void SpawnEnemy()
	{
		if (GameManager.pInstance._EnemyPool != null) {
			GameObject enemy = GameManager.pInstance._EnemyPool.GetPooledObject ();
			if (enemy != null) {
				// randomly spawn at different positions along the horizontal axis on screen. The width of the enemy object is taken into account to spawn it with in the game area.
				BoxCollider collider = enemy.GetComponent<BoxCollider> ();
				float x = Random.Range (GameManager.pInstance.pGameRect.xMin + collider.size.x * 0.5f, GameManager.pInstance.pGameRect.xMax - collider.size.x * 0.5f);

				Vector3 spawnPosition = Camera.main.ScreenToWorldPoint (new Vector3 (x, Screen.height - _SpawnHeight, 100));

				enemy.transform.position = spawnPosition;
				enemy.SetActive (true);
			}
			ResetTimer ();
		} else
			Debug.LogError ("Enemy pool not found.");
	}

	void ResetTimer()
	{
		mTimer = Random.Range (_SpawnTimeRange.x, _SpawnTimeRange.y);
	}

	void GameOver()
	{
		GameManager.pInstance._EnemyPool.ResetPool ();
		GameManager.pInstance._EnemyBulletPool.ResetPool ();
	}

	void OnDestroy()
	{
		GameManager.pInstance.gameOver -= GameOver;
	}
}
