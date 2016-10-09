using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BgmManager))]
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
    private BgmManager bgmManager;
    public AudioSource source;
    private static SoundController instance;

    public void Play(string clip) {
      
        BgmManager.Instance.GetComponent<AudioSource>().spatialBlend = 1;
        BgmManager.Instance.GetComponent<AudioSource>().spread = 360;
        BgmManager.Instance.Play(clip);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
