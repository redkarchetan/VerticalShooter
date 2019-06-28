using UnityEngine;
using System.Collections;

// The base enemy class, which also has the implementation of the basic enemy. 
// This class can be extended to implement different enemy behaviours.
// This class implements a basic state machine.
public class Enemy : MonoBehaviour 
{
	public enum States
	{
		NONE,
		INIT,
		IDLE,
		FIRE,
		TAKE_HIT,
		DIE
	}

	public int _HealthPoints = 1;
	public Vector2 _IdleTimeRange;
	public float _Speed = 5f;
	public float _LifeTime = 5f;
	public int _Score = 30;

	protected States mCurrentState;
	protected float mIdleTimer = 0;
	protected float mLifeTimer;
	protected Transform mTransform;

	void Start()
	{
		mTransform = transform;
	}

	void OnEnable()
	{
		SetState (States.INIT);
		mLifeTimer = _LifeTime;
	}

	public void SetState(States s)
	{
		ExitState (mCurrentState);
		mCurrentState = s;
		EnterState (s);
	}

	public virtual void EnterState(States s)
	{

	}

	public virtual void ExitState(States s)
	{
		switch (s) {
		case States.IDLE:
			mIdleTimer = 0;
			break;

		default:
			break;
		}
	}

	void Update()
	{
		if (GameManager.pInstance.pIsGamePaused)
			return;

		switch (mCurrentState) {

		case States.INIT:
			Init();
			break;

		case States.IDLE:
			DoIdle();
			break;

		case States.FIRE:
			Fire ();
			break;

		case States.TAKE_HIT:
			TakeHit(1);
			break;

		case States.DIE:
			Die ();
			break;

		default:
			break;
		}

		mLifeTimer -= Time.deltaTime;
		if (mLifeTimer <= 0)
			SetState (States.DIE);
		else 
		{
			States nextState = States.NONE;
			if (CanMoveToNextState (ref nextState))
				SetState (nextState);
		}
	}

	public virtual void Init()
	{
		mIdleTimer = Random.Range (_IdleTimeRange.x, _IdleTimeRange.y);
	}

	public virtual void DoIdle()
	{
		mTransform.position -= new Vector3(0, 0, _Speed) * Time.deltaTime;
		mIdleTimer -= Time.deltaTime;
	}

	public virtual void Fire()
	{
		if(GameManager.pInstance._EnemyBulletPool != null)
		{
			GameObject bullet = GameManager.pInstance._EnemyBulletPool.GetPooledObject ();
			bullet.transform.position = mTransform.position;
			bullet.SetActive(true);
			if(bullet != null)
			{
				Bullet bulletScript = bullet.GetComponent<Bullet>();
				Vector3 direction = (GameManager.pInstance._MyShipTransform.position - mTransform.position) / Vector3.Magnitude(GameManager.pInstance._MyShipTransform.position - mTransform.position);

				bulletScript.SetDirection(direction);
			}

		}
	}

	public virtual void TakeHit(int damage)
	{
		_HealthPoints -= damage;
	}

	public virtual void Die()
	{
		gameObject.SetActive (false);
		GameManager.pInstance.AddScore (_Score);
	}

	public virtual bool CanMoveToNextState(ref States nextState)
	{
		bool canChangeState = false;
		switch (mCurrentState) {

		case States.INIT:
			nextState = States.IDLE;
			canChangeState = true;
			break;
		
		case States.IDLE:
			if(mIdleTimer <= 0)
			{
				nextState = States.FIRE;
				canChangeState = true;
			}
		break;
		
		case States.FIRE:
			nextState = States.INIT;
			canChangeState = true;
		break;
		
		case States.TAKE_HIT:
			if(_HealthPoints <= 0)
			{
				nextState = States.DIE;
				canChangeState = true;
			}
			break;
		
		case States.DIE:
		break;
		
		default:
		break;
		}

		return canChangeState;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("Player")) 
		{
			SetState (States.TAKE_HIT);
		}
	}
}
