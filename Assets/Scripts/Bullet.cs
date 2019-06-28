using UnityEngine;
using System.Collections;

// A generic bullet script. When the bullet is instantiated, it moves along a set direction at a set speed.
// It destroys itself on collision with another object or when its lifetime is over.
public class Bullet : MonoBehaviour 
{
	public float _Speed = 5f;
	public float _Lifetime = 2f;

	private Transform mTransform;
	private Vector3 mDirection;
	private float mLifeTimer;

	void Start()
	{
		// Cache the transform for optimisation.
		mTransform = transform;
	}

	void OnEnable() 
	{
		mLifeTimer = _Lifetime;
	}

	// Return the object to the pool.
	void Destroy()
	{
		gameObject.SetActive(false);
	}

	void Update() 
	{
		if (!GameManager.pInstance.pIsGamePaused) 
		{
			mTransform.position = mTransform.position + mDirection * _Speed * Time.deltaTime;
			mLifeTimer -= Time.deltaTime;
			if(mLifeTimer <= 0)
				Destroy();
		}
	}

	public void SetDirection(Vector3 direction)
	{
		mDirection = direction;
	}

	// We check the the tag of the colliding object to prevent collision with similar objects.
	void OnTriggerEnter(Collider other)
	{
		if (!tag.Equals (other.tag)) 
		{
			Destroy ();
		}
	}
}
