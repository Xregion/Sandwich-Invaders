using UnityEngine;

public class SpawnerMover : MonoBehaviour {

 //   float speed;
    float variance;
    Vector3 spawnPosition;

    // Use this for initialization
    void Start () {
        spawnPosition = transform.position;
        //speed = 1;
        //InvokeRepeating ("SpeedReset", 5f, 5f);
    }

    //// Update is called once per frame
    void Update () {
        variance = Random.Range (0, 3);
        if (variance == 0) {
            spawnPosition.x = -2;
            transform.position = spawnPosition;
        } else if (variance == 1) {
            spawnPosition.x = 0;
            transform.position = spawnPosition;
        } else if (variance == 2) {
            spawnPosition.x = 2;
            transform.position = spawnPosition;
        }
        //       transform.Translate (new Vector3 (speed * Time.deltaTime, 0, 0));
        //       if (transform.position.x < -1.3) {
        //           speed = -speed;
        //           speed += variance;
        //       } else if (transform.position.x > 1.3) {
        //           speed = -speed;
        //           speed -= variance;
        //       }
    }

 //   void SpeedReset () {
 //       speed = 1;
 //   }
}
