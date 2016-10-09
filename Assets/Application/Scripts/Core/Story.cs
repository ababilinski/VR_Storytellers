using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Story {
    public string discription { get; private set; }
    public float time { get; private set; }
    public string mood { get; private set; }
    public string location { get; private set; }
    public List<GameObject> sceneObjects { get; private set; }
    public Vector3 PositionOffset { get; private set; }
    public Story(string discripion, float time, string mood, string location, List<GameObject> sceneObjects) {
        this.discription = discription;
        this.time = time;
        this.mood = mood;
        this.location = location;
        this.sceneObjects = sceneObjects;
        this.PositionOffset = Vector3.zero;
    }
}
