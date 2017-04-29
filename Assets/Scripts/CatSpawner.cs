using UnityEngine;
using UnityEngine.SceneManagement;

public class CatSpawner : MonoBehaviour {

    public static CatSpawner instance;

    public GameObject catPrefab;
    public AudioClip titleScreenClip;
    public AudioClip gamePlayClip;

    AudioSource source;

	void Awake () {
        source = GetComponent<AudioSource> ();
        if (instance == null) {
            DontDestroyOnLoad (this);
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }
    }

    void OnEnable () {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable () {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading (Scene scene, LoadSceneMode mode) {
        if (scene.buildIndex != 0) {
            InstantiateCat ();
            source.clip = gamePlayClip;
            source.Play ();
            FindObjectOfType<UserInterface> ().pauseEvent += PauseAudio;
        } else if (scene.buildIndex == 0) {
            source.clip = titleScreenClip;
            source.Play ();
        }
    }

    void PauseAudio () {
        if (source.isPlaying) {
            source.Pause ();
        } else {
            source.UnPause ();
        }
    }

    public void SetCatPrefab (GameObject prefab) {
        catPrefab = prefab;
    }

    public void InstantiateCat () {
        Instantiate (catPrefab, transform.position, transform.rotation);
    }
}
