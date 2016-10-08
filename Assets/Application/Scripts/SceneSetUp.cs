using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class SceneSetUp : MonoBehaviour {
    [SerializeField]
    Transform playerSpawnPoint;
    [SerializeField]
    Transform characterSpawnPoint;
    [SerializeField]
    AudioSource audioFocus;
    [SerializeField]
    Camera[] playerCamera;
    [SerializeField]
    float clippingMax;
    public UnityEvent onDoneLoadingScene;
    int camCount;
	// Use this for initialization
	void Start () {
        CameraInit();


    }

    void CameraInit() {
        playerCamera = FindObjectsOfType<Camera>();
        camCount = playerCamera.Length;
        SetCameraClipping(1);
    }

    void SetCameraClipping(float value) {
        for (int i = 0; i < camCount; i++) {
            playerCamera[i].farClipPlane =value;
        }
        StartCoroutine(IncreaseClippingPlain());
    }

    IEnumerator IncreaseClippingPlain(float size = 300) {
        float timer = 0;
        while (playerCamera[playerCamera.Length-1].farClipPlane < size) {
            timer += Time.deltaTime/20;
            for (int i = 0; i < camCount; i++) {
                playerCamera[i].farClipPlane = Mathf.Lerp(1, size, timer);
            }
            yield return null;
        }
        if (playerCamera[0].farClipPlane >= size) {
            onDoneLoadingScene.Invoke();
            yield return 0;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
