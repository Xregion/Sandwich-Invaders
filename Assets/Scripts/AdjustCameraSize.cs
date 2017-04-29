using UnityEngine;

public class AdjustCameraSize : MonoBehaviour {

	float cameraSize;
	float aspectRatio;
	float currentAspectRatio;
	float adjustedCameraSize;

	// Use this for initialization
	void Start () {
		Camera camera = GetComponent<Camera> ();
		cameraSize = 2.0f;
		aspectRatio = 16.0f / 10.0f;
		currentAspectRatio = (float)Screen.width / (float)Screen.height;
		adjustedCameraSize = (aspectRatio / currentAspectRatio) * cameraSize;
		camera.orthographicSize = adjustedCameraSize;
	}
}