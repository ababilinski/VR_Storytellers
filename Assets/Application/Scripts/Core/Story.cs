using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Story {
    public string discription { get; private set; }
    public float time { get; private set; }
    public string mood { get; private set; }
    public string location { get; private set; }
    public string setting { get; private set; }
    public List<GameObject> sceneObjects { get; private set; }
    public Vector3 PositionOffset { get; private set; }
    public Story(string discripion = "", float time = 0, string mood="", string location="", string setting="", List<GameObject> sceneObjects = null) {
        if (mood.Equals("Joy"))
            mood = "Happy";
        if (mood.Equals("Anger"))
            mood = "Angry";
        if (mood.Equals("Sadness"))
            mood = "Sad";
        if (mood.Equals("Fear"))
            mood = "Sad";

        this.discription = discription;
        this.time = time;
        this.mood = mood;
        this.location = location;
        this.sceneObjects = sceneObjects;
        this.setting = setting;
        this.PositionOffset = Vector3.zero;
    }



}
