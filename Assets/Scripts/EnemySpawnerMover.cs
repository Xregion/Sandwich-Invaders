using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerMover : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int x = (Random.Range (0, 2));
        if (x == 0) {
            transform.position = new Vector3 (2, transform.position.y, 0);
        } else if (x == 1) {
            transform.position = new Vector3 (-2, transform.position.y, 0);
        }
	}
}
