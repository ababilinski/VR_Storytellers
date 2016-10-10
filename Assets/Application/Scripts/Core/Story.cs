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
        mood = ConvertMood(mood);

        this.discription = discription;
        this.time = time;
        this.mood = mood;
        this.location = location;
        this.sceneObjects = sceneObjects;
        this.setting = setting;
        this.PositionOffset = Vector3.zero;
    }
   
    public static string ConvertMood(string mood) {
        if (mood.Equals("Joy", StringComparison.CurrentCultureIgnoreCase))
            mood = "Happy";
        if (mood.Equals("Anger", StringComparison.CurrentCultureIgnoreCase))
            mood = "Angry";
        if (mood.Equals("Sadness", StringComparison.CurrentCultureIgnoreCase))
            mood = "Sad";
        if (mood.Equals("Fear", StringComparison.CurrentCultureIgnoreCase) || mood.Equals("Disgust", StringComparison.CurrentCultureIgnoreCase))
            mood = "Sad";

        return mood;
    }



}
