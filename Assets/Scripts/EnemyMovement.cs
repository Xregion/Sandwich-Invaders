using UnityEngine;
using System;

public class EnemyMovement : MonoBehaviour {

    public event Action hitEvent;
    public float speed;
    public float increaseSpeed;
    public float increaseSpeedMultiplier;
    public float maximumIncreaseSpeed;
    public float repeatTime;
    public float decreaseRepeatTime;
    public float minimumRepeatTime;
    public float fireRate;
    public int spawnAmount;
    public LayerMask ignoredLayer;
    public GameObject projectile;

    SpriteRenderer ren;
    bool hit;
    float lowerAmount;
    float originalSpeed;
    Vector3 origin;
    Vector3 spawnPosition;
    Vector3 respawnPosition;

    void Start () {
        lowerAmount = 0.5f;
        ren = GetComponent<SpriteRenderer> ();
        PoolManager.instance.CreateNewPool (projectile, spawnAmount);
        InvokeRepeating ("CheckBelow", fireRate, fireRate);
        InvokeRepeating ("LowerToGround", repeatTime, repeatTime);
        spawnPosition = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
        respawnPosition = transform.position;
        FindObjectOfType<CatController> ().deathEvent += GameOver;
        GetComponentInParent<EnemyRespawn> ().respawnEvent += Respawn;
        originalSpeed = speed;
    }

	void Update () {
        if (ren.flipX) {
            transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
        } else {
            transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
        }
	}

    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag ("Wall")) {
            ren.flipX = !ren.flipX;
        }
        if (col.CompareTag ("Projectile")) {
            if (hitEvent != null) {
                hitEvent ();
            }
            gameObject.SetActive (false);
            CancelInvoke ();
        }
    }

    void Fire () {
        spawnPosition = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
        PoolManager.instance.ReuseObject (projectile, spawnPosition, transform.rotation);
    }

    void CheckBelow () {
        origin = new Vector3 (transform.position.x, transform.position.y - 0.6f, transform.position.z);
        Debug.DrawRay (origin, -transform.up * 0.3f, Color.red, 2f);
        hit = Physics2D.Raycast (origin, -transform.up, 0.3f, ignoredLayer);
        if (!hit) {
            Fire ();
        }
    }

    void LowerToGround () {
        Vector3 lower = new Vector3 (transform.position.x, transform.position.y - lowerAmount, transform.position.z);
        transform.position = lower;
        speed += increaseSpeed;
    }

    void Respawn () {
        speed = originalSpeed;
        transform.position = respawnPosition;
        if (increaseSpeed < maximumIncreaseSpeed) {
            increaseSpeed += increaseSpeedMultiplier; 
        }
        if (increaseSpeed >= maximumIncreaseSpeed) {
            increaseSpeed = maximumIncreaseSpeed;
        }
        if (repeatTime > minimumRepeatTime) {
            repeatTime -= decreaseRepeatTime;
        }
        if (repeatTime <= minimumRepeatTime) {
            repeatTime = minimumRepeatTime;
        }
        ren.flipX = false;
        gameObject.SetActive (true);
        InvokeRepeating ("CheckBelow", fireRate, fireRate);
        InvokeRepeating ("LowerToGround", repeatTime, repeatTime);
    }

    void GameOver () {
        CancelInvoke ();
    }
}
