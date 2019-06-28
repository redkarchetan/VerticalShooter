using UnityEngine;
using System.Collections;

// My ship is the implementation for player's ship.
public class MyShip : MonoBehaviour 
{
	public float _Speed = 5f;

	private Transform mTransform;
	private float mWidth;
	private float mHeight;

	void Start () 
	{
		// Cache the transform and other values for optimization.
		mTransform = transform;
		BoxCollider collider = gameObject.GetComponent<BoxCollider> ();
		if (collider != null) 
		{
			mWidth = collider.size.x;
			mHeight = collider.size.z;
		}

		// Event listeners for controller events.
		Controller.shoot += Shoot;
		Controller.moveHorizontal += MoveHorizontally;
		Controller.moveVertical += MoveVertically;

		GameManager.pInstance.gameOver += GameOver;
	}

	// Instantiate a bullet and send it upwards in the screen.
	void Shoot()
	{
		if(GameManager.pInstance._MyBulletPool != null && !GameManager.pInstance.pIsGamePaused)
		{
			GameObject bullet = GameManager.pInstance._MyBulletPool.GetPooledObject();
			if(bullet != null)
			{
				bullet.SetActive(true);
				bullet.transform.position = mTransform.position;
				Bullet bulletScript = bullet.GetComponent<Bullet>();
				if(bulletScript != null)
					bulletScript.SetDirection(Vector3.forward);
				else
					Debug.LogError("No bullet script attached to bullet");
			}
			else
			{
				Debug.LogError("Bullet pool returned no bullet");
			}
		}
	}

	// Check if the ship can move horizontally within the game area and move.
	void MoveHorizontally(float displacement)
	{
		if (!GameManager.pInstance.pIsGamePaused) 
		{
			Vector3 newPosition = new Vector3 (mTransform.position.x + displacement * _Speed * Time.deltaTime, mTransform.position.y, mTransform.position.z);
			if(IsinHorizontalScreenSpace(newPosition))
			{
				mTransform.position = newPosition;
			}
		}
	}

	// Check if the ship can move vertically within the game area and move.
	void MoveVertically(float displacement)
	{
		if (!GameManager.pInstance.pIsGamePaused) 
		{
			Vector3 newPosition = new Vector3 (mTransform.position.x, mTransform.position.y, mTransform.position.z + displacement * _Speed * Time.deltaTime);
			if(IsInVerticalScreenSpace(newPosition))
			{
				mTransform.position = newPosition;
			}
		}
	}

	// If the ship collides with an enemy bullet or enemy, it takes hit.
	// The ship blinks twice to provide feedback to the user that it has taken hit.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Enemy")) 
		{
			GameManager.pInstance.ShipHit();
			StartCoroutine(Blink (4, 0.1f));
		}
	}
	
	IEnumerator Blink(int count, float duration)
	{
		while (count > 0) 
		{
			gameObject.GetComponent<Renderer>().enabled = !gameObject.GetComponent<Renderer>().enabled;
			count--;
			yield return new WaitForSeconds(duration);
		}
	}

	void GameOver()
	{
		GameManager.pInstance._MyBulletPool.ResetPool ();
	}


	// The ship's width is taken into account to check if the ship can move horizontally.
	bool IsinHorizontalScreenSpace(Vector3 point)
	{
		Vector3 screenPointMin = Camera.main.WorldToScreenPoint(point - new Vector3(mWidth * 0.5f, 0, 0));
		Vector3 screenPointMax = Camera.main.WorldToScreenPoint(point + new Vector3(mWidth * 0.5f, 0, 0));
		if(screenPointMin.x > GameManager.pInstance.pGameRect.xMin && screenPointMax.x < GameManager.pInstance.pGameRect.xMax)
		{
			return true;
		}
		
		return false;
	}

	// The ship's height is taken into account to check if the ship can move horizontally.
	bool IsInVerticalScreenSpace(Vector3 point)
	{
		Vector3 screenPointMin = Camera.main.WorldToScreenPoint(point - new Vector3(0, 0, mHeight * 0.5f));
		Vector3 screenPointMax = Camera.main.WorldToScreenPoint(point + new Vector3(0, 0, mHeight * 0.5f));
		if(screenPointMin.y > GameManager.pInstance.pGameRect.yMin && screenPointMax.y < GameManager.pInstance.pGameRect.yMax)
		{
			return true;
		}

		return false;
	}

	void OnDestroy()
	{
		Controller.shoot -= Shoot;
		Controller.moveHorizontal -= MoveHorizontally;
		GameManager.pInstance.gameOver -= GameOver;
	}
}
