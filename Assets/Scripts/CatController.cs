using UnityEngine;
using System;

public class CatController : MonoBehaviour {

    public event Action deathEvent;
    public event Action lifeEvent;
    public float speed;
    public float reloadSpeed;
    public int spawnAmount;
    public bool idle;
    public bool run;
    public GameObject projectile;

    SpriteRenderer ren;
    Animator animator;
    float originalSpeed;
    int lives;
    bool fire;
    bool rapidFire;
    bool letGo;

    void Awake () {
        lives = 9;
    }

    // Use this for initialization
    void Start () {
        ren = GetComponent<SpriteRenderer> ();
        animator = GetComponent<Animator> ();
        idle = true;
        run = false;
        fire = true;
        rapidFire = false;
        letGo = true;
        originalSpeed = speed;
        PoolManager.instance.CreateNewPool (projectile, spawnAmount);
        SetAnimationParameters ();
    }

	void Update () {
        Walk ();
        if (Input.GetKeyDown (KeyCode.Space) && fire) {
            Fire ();
        }
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);

            if (touch.phase == TouchPhase.Began && letGo && fire) {
                Fire ();
                letGo = false;
            } else if (touch.phase == TouchPhase.Ended) {
                letGo = true;
            }
        }
	}

    public int GetLives () {
        return lives;
    }

    void Walk () {

#if UNITY_STANDALONE_WIN
        if (Input.GetKey (KeyCode.LeftArrow)) {
            WalkWindows (false, -speed);
        } else if (Input.GetKey (KeyCode.RightArrow)) {
            WalkWindows (true, speed);
        } else {
            run = false;
            idle = true;
            SetAnimationParameters ();
        }
#endif

#if UNITY_ANDROID
        if (Input.acceleration.x < 0) {
            WalkAndroid (false, -speed);
        } else if (Input.acceleration.x > 0) {
            WalkAndroid (true, speed);
        } else if (Input.acceleration.x == 0) {
            run = false;
            idle = true;
            SetAnimationParameters ();
        }
#endif
    }

    void WalkWindows (bool direction, float speed) {
        transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
        ren.flipX = direction;
        run = true;
        idle = false;
        SetAnimationParameters ();
    }

    void WalkAndroid (bool direction, float speed) {
        transform.Translate (new Vector3 ((Input.acceleration.x / Input.acceleration.x) * speed * Time.deltaTime, 0, 0));
        ren.flipX = direction;
        run = true;
        idle = false;
        SetAnimationParameters ();
    }

    void Fire () {
        PoolManager.instance.ReuseObject (projectile, transform.position, transform.rotation);
        if (!rapidFire) {
            fire = false;
            Invoke ("Reload", reloadSpeed);
        }
    }

    void Reload () {
        fire = true;
    }

    void SetAnimationParameters () {
        animator.SetBool ("Run", run);
        animator.SetBool ("Idle", idle);
    }

    void RemoveBuff () {
        speed = originalSpeed;
        rapidFire = false;
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag ("Strawberry")) {
            rapidFire = true;
            Invoke ("RemoveBuff", 2f);
        } else if (col.CompareTag ("Orange")) {
            lives += 1;
            if (lifeEvent != null) {
                lifeEvent ();
            }
        } else if (col.CompareTag ("Grape")) {
            speed += 3f;
            Invoke ("RemoveBuff", 5f);
        } else if (col.CompareTag ("Olive")) {
            lives -= 1;
            if (lives <= 0) {
                lives = 0;
                EndGame ();
            }
            if (lifeEvent != null) {
                lifeEvent ();
            }
        } else if (col.CompareTag ("Enemy")) {
            lives = 0;
            EndGame ();
        }
    }

    void EndGame () {
        idle = true;
        run = false;
        SetAnimationParameters ();
        if (deathEvent != null) {
            deathEvent ();
        }
        ren.flipY = true;
        if (gameObject.GetComponent<CatController> () != null) {
            gameObject.GetComponent<CatController> ().enabled = false;
        }
    }
}
