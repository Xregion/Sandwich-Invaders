using UnityEngine;

public class BarrierHealth : MonoBehaviour {

    int barrierHealth;

	// Use this for initialization
	void Start () {
        barrierHealth = 18;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag ("Olive")) {
            barrierHealth -= 1;
            if (barrierHealth <= 0) {
                gameObject.SetActive (false);
            }
        }
        if (col.CompareTag ("Projectile")) {
            barrierHealth += 1;
        }
    }
}
