using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject[] spawnedObject;
    public int spawnAmount;
    public float spawnTimer;

	Transform theSpawner;

	// Use this for initialization
	void Start () {
		theSpawner = gameObject.GetComponent<Transform> ();
        for (int i = 0; i < spawnedObject.Length; i++) {
            PoolManager.instance.CreateNewPool (spawnedObject[i], spawnAmount);
        }
        InvokeRepeating ("Spawn", 5f, spawnTimer);
    }

	void Spawn () {
        int x = Random.Range (0, 3);
        PoolManager.instance.ReuseObject (spawnedObject[x], theSpawner.position, theSpawner.rotation);
    }
}
