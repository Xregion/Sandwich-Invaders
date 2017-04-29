using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenUI : MonoBehaviour {

    public Text highscore;
    public Text instructions;
    public Image muteImage;
    public Sprite muteSprite;
    public Sprite unmuteSprite;

    void Start () {
        if (CatSpawner.instance.GetComponent<AudioSource> ().mute == true) {
            muteImage.sprite = muteSprite;
        }
        highscore.text = "Your Highscore: \n" + PlayerPrefs.GetInt ("highscore");
#if UNITY_STANDALONE_WIN
        instructions.text = "How to play: Use the left and right arrow keys to move your cat. " + 
        "Use the spacebar to fire a projectile into the air and try to hit the Sandwich Invaders. " +
        "The Invaders will fire back, though, so be careful. " +
        "If you get hit by an olive you will lose one life, lose all your lives and it's Game Over. " +  
        "Remember to take cover under barriers, but watch out! " +
        "Each barrier has a limited amount of hits before it breaks. " +  
        "You can heal the barriers by firing your own projectiles into them, " +
        "but don't waste too much time or the Invaders will reach Earth. " +
        "Pick up fruits that spawn throughout the field, each fruit gives you a different power up. " +
        "Defeat all the Invaders and...more arrive! See how long you can retard their invasion!";
#endif

#if UNITY_ANDROID
        instructions.text = "How to play: Tilt your screen left or right to move your cat. " + 
        "Tap the screen to fire a projectile into the air and try to hit the Sandwich Invaders. " +
        "The Invaders will fire back, though, so be careful. " +
        "If you get hit by an olive you will lose one life, lose all your lives and it's Game Over. " +  
        "Remember to take cover under barriers, but watch out! " +
        "Each barrier has a limited amount of hits before it breaks. " +  
        "You can heal the barriers by firing your own projectiles into them, " +
        "but don't waste too much time or the Invaders will reach Earth. " +
        "Pick up fruits that spawn throughout the field, each fruit gives you a different power up. " +
        "Defeat all the Invaders and...more arrive! See how long you can retard their invasion!";
#endif
    }

    public void StartGame () {
        Time.timeScale = 1;
        SceneManager.LoadScene (1);
    }

    public void SetCatPrefab (GameObject prefab) {
        CatSpawner.instance.SetCatPrefab (prefab);
    }

    public void AdjustVolume () {
        if (CatSpawner.instance.GetComponent<AudioSource> ().mute == false) {
            CatSpawner.instance.GetComponent<AudioSource> ().mute = true;
            muteImage.sprite = muteSprite;
        } else {
            CatSpawner.instance.GetComponent<AudioSource> ().mute = false;
            muteImage.sprite = unmuteSprite;
        }
    }

    public void ExitGame () {
        Application.Quit ();
    }
}
