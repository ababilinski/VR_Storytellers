using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScreenFader : MonoBehaviour {
    public Image panel;
    public Color fade = Color.black;
    public  bool isFading;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void startFade(float value) {
        StartCoroutine(FadeTo(value));
    }
    IEnumerator FadeTo(float value = 1) {
        isFading = true;
        float time = 0;
        fade = panel.color;
       
        while (fade.a < 0) {
            time += Time.deltaTime/20;
            fade.a = Mathf.Lerp(0, value, time);
            panel.color = fade;
            Debug.Log(fade.a);
            yield return 0;
        }
        if (fade.a >= 1) {
            isFading = false;
        }
    }
}
