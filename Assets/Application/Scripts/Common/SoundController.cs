using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

    public static SoundController Instance {
        get
        {
            if (instance == null) {
                instance = FindObjectOfType<SoundController>();
                instance.source = instance.GetComponent<AudioSource>();
            }

            if (instance == null) {
                var go = new GameObject("SoundController");
                instance = go.AddComponent<SoundController>();
                instance.source = go.AddComponent<AudioSource>();
                DontDestroyOnLoad(go);
            }

            return instance;
        }
    }
    public AudioSource source;
    private static SoundController instance;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
