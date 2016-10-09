using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
public class GazeEventHandler : MonoBehaviour,
IGvrGazeResponder {
    float timeSelected = 0;
    [SerializeField]
    float waitTime = 2;
    [SerializeField]
    Image Progress;
    [SerializeField]
    Text textUI;
    [SerializeField]
    bool isSelected;
    bool Clicked;
    [SerializeField]
    UnityEvent onClick;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Clicked)
            return;

        if (isSelected) {
            timeSelected += Time.deltaTime;
            Progress.fillAmount = timeSelected / waitTime;
        }
        else if(!isSelected && timeSelected >= 0){
            timeSelected -= Time.deltaTime;
            Progress.fillAmount = timeSelected / waitTime;
        }
        if (timeSelected >= waitTime) {
            textUI.text = "Reading Story...";
            if(onClick!=null)
            onClick.Invoke();
            Clicked = true;
        }
	}
    public void OnGazeTrigger() { }

    public void OnGazeEnter() {
        isSelected = true;
    }

    public void OnGazeExit() {
        isSelected = false;
        timeSelected = 0;
    }
}
