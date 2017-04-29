using UnityEngine;

public class DeactivateObject : MonoBehaviour {

    public GameObject[] triggerObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D col) {
        for (int i = 0; i < triggerObject.Length; i++) {
            if (col.CompareTag (triggerObject[i].tag)) {
                gameObject.SetActive (false);
            }
        }
    }
}
