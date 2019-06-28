using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This is a  generic object pool, which is used to reduce the number of GameObject.Instantiate() and Destroy() calls, since they are expensive.
// Instead we reuse objects. The pool is expandable, i.e. if there is no available object in the pool, a new one is instantiated and returned.
// If required, we can have a maxCount and not let the pool grow beyond that.
public class ObjectPool : MonoBehaviour 
{
	public GameObject _TemplateObject;
	public int _StartSize = 0;

	private List<GameObject> mObjectPool;

	void Start () 
	{
		mObjectPool = new List<GameObject>();
		GameObject obj = null;

		if(_TemplateObject != null)
		{
			for(int i = 0; i < _StartSize; i++)
			{
				obj = GameObject.Instantiate(_TemplateObject) as GameObject;
				obj.SetActive(false);
				mObjectPool.Add(obj);
			}
		}
	}

	public GameObject GetPooledObject()
	{
		GameObject pooledObject = null;
		if(mObjectPool != null)
		{
			for(int i = 0; i < mObjectPool.Count; i++)
			{
				if(!mObjectPool[i].activeInHierarchy)
				{
					return mObjectPool[i];
				}
			}
			pooledObject = GameObject.Instantiate(_TemplateObject) as GameObject;
			pooledObject.SetActive(false);
			mObjectPool.Add(pooledObject);
		}
		return pooledObject;
	}

	public void ResetPool()
	{
		for(int i = 0; i < mObjectPool.Count; i++)
		{
			mObjectPool[i].SetActive(false);
		}
	}
}
