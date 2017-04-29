using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {

	}

    void Update () {
        transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));  
    }
}
