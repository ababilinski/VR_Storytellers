using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class CheckWords : MonoBehaviour {
    public string[] Moods;
    public string[] Locations;
    public string[] Settings;
    public List<GameObject> storyObject;

    private static string[] _moods;
    private static string[] _locations;
    private static string[] _settings;
    private static List<GameObject> _storyObject;
    void Start() {
        _moods = Moods;
        _locations = Locations;
        _settings = Settings;
        _storyObject = storyObject;
    }

    public static Story createStory(string text) {
        string Mood = "";
        string Location = "";
        string Settings = "";
        List<GameObject> items = new List<GameObject>();

        for (int i = 0; i < _moods.Length; i++) {
            if (string.IsNullOrEmpty(Mood) && StringContains(text, _moods[i], StringComparison.OrdinalIgnoreCase)) {
                Mood = _moods[i];
            }
        }
        if (string.IsNullOrEmpty(Mood)) {
            if (string.IsNullOrEmpty(Mood) && StringContains(text,"barking", StringComparison.OrdinalIgnoreCase)) {
                Mood = "Angry";
            }
            
        }

        for (int i = 0; i < _locations.Length; i++) {
            if (string.IsNullOrEmpty(Location) && StringContains(text, _locations[i], StringComparison.OrdinalIgnoreCase)) {
                Location = _locations[i];
            }
        }

        for (int i = 0; i < _settings.Length; i++) {
            if (string.IsNullOrEmpty(Settings) && StringContains(text, _settings[i], StringComparison.OrdinalIgnoreCase)) {
                Settings = _settings[i];
            }
        }
        if (string.IsNullOrEmpty(Settings))
            Settings = "Day";


        for (int i = 0; i < _moods.Length; i++) {
            if (string.IsNullOrEmpty(Mood) && StringContains(text, _moods[i], StringComparison.CurrentCultureIgnoreCase)) {
                Mood = _moods[i];
            }
        }

        for (int i = 0; i < _storyObject.Count; i++) {
            if (StringContains(text, _storyObject[i].name, StringComparison.CurrentCultureIgnoreCase)) {
                var newitem = _storyObject[i];
                if (_storyObject[i].name == "Rabbit" && Mood.Equals("Sad")) {
                    newitem = _storyObject.Find(e => e.name == "SadRabbit");
                }
                else if (_storyObject[i].name == "Dog" && Mood.Equals("Angry")) {
                    newitem = _storyObject.Find(e => e.name == "AngryDog");

                }
                items.Add(newitem);
            }
        }
        return new Story(text, 1, Mood, Location, Settings, items);


    }

    public static bool StringContains(string source, string toCheck, StringComparison comp) {
        return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
    }
}
