using System.Collections;
using UnityEngine;

public class DespawnObject : MonoBehaviour {

    SpriteRenderer ren;
    Color color;

	// Use this for initialization
	void Start () {
        ren = GetComponent<SpriteRenderer> ();
        color = ren.color;
	}

    void OnEnable () {
        StartCoroutine (Despawn ());
    }

    IEnumerator Despawn () {
        yield return new WaitForSeconds (7f);
        Blink (0);
        yield return new WaitForSeconds (0.2f);
        Blink (1);
        yield return new WaitForSeconds (0.2f);
        Blink (0);
        yield return new WaitForSeconds (0.2f);
        Blink (1);
        yield return new WaitForSeconds (0.2f);
        Blink (0);
        yield return new WaitForSeconds (0.2f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.1f);
        Blink (0);
        yield return new WaitForSeconds (0.1f);
        Blink (1);
        yield return new WaitForSeconds (0.05f);
        Blink (0);
        yield return new WaitForSeconds (0.05f);
        Blink (1);
        gameObject.SetActive (false);
    }

    void Blink (int alpha) {
        color.a = alpha;
        ren.color = color;
    }
}
