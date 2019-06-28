using UnityEngine;
using System.Collections;

// Infinite terrain generator. We create new terrain objects on the fly and destroy the older ones.
public class TerrainGenerator : MonoBehaviour 
{
	// Each terrain object must ideally cover the screen.
	public GameObject[] _Meshes;
	public GameObject _StartMesh;
	public float _TerrainHeight = -0.5f;

	private GameObject mCurrentMesh;
	private GameObject mMeshToBeDestroyed;

	private Bounds mCurrentMeshBoundingBox;
	private Bounds mNextMeshBoundingBox;

	private Transform mCameraTransform;
	
	void Start () 
	{
		mCurrentMesh = GameObject.Instantiate(_StartMesh) as GameObject;
		mCurrentMesh.transform.position += new Vector3 (0, _TerrainHeight, 0);
		mCurrentMeshBoundingBox = mCurrentMesh.GetComponent<BoxCollider>().bounds;
		mCameraTransform = Camera.main.transform;
	}


	void Update () 
	{
		if(mCameraTransform.position.z > mCurrentMesh.transform.position.z)
		{
			// This implementation intantiates and destroys objects as we move.
			// We can also use a pool of instantited objects to avoid dynamically instantiate and destroy objects.

			// Cleanup objects which are off camera.
			if(mMeshToBeDestroyed != null)
				Destroy(mMeshToBeDestroyed);

			mMeshToBeDestroyed = mCurrentMesh;

			GameObject nextmesh = GameObject.Instantiate(_Meshes[Random.Range(0, _Meshes.Length)]) as GameObject;

			// Reposition the next mesh at the end of the previous one.
			mNextMeshBoundingBox = nextmesh.GetComponent<BoxCollider>().bounds;
			nextmesh.transform.position = mCurrentMesh.transform.position + new Vector3(0, 0, mCurrentMeshBoundingBox.extents.z + mNextMeshBoundingBox.extents.z);
			mCurrentMesh = nextmesh;
			mCurrentMeshBoundingBox = mNextMeshBoundingBox;
		}
	}
}