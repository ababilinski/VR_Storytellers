using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

[System.Serializable]
public class CreatedStoryEvent : UnityEngine.Events.UnityEvent<Story> { }
public class Client : MonoBehaviour {
   
    public Story currentStory { get; private set; }
    public bool startTracking { get; private set; }
    [SerializeField]
    private bool isVerbose;
    private static bool IsVerbose;
    [SerializeField]
    private List<GameObject> sceneObjects = new List<GameObject>();
    
    public string jsonURL;
    
    public string testJson;

    public UnityEvent onJsonSuccessful;
    public CreatedStoryEvent onCreatedStory;
    // Use this for initialization
    public void StartTracking(bool on) {
        startTracking = on;
    }
    void Awake() {
        IsVerbose = isVerbose;
    }
	IEnumerator Start () {

        while (true) {
            if (startTracking) {
                //TODO: have a call from the website so that we do not constantly communicate to the json
                yield return new WaitForSeconds(6);
                if (string.IsNullOrEmpty(testJson))
                    StartCoroutine(RequestURL(jsonURL));
                else
                    TestJson(testJson);
            }
            yield return 0;
        }
	}

    IEnumerator RequestURL(string url) {
        if (IsVerbose)
            Debug.Log("loading from web...");

        WWW www = new WWW(url);

        float elapsedTime = 0.0f;
        while (!www.isDone) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 10.0f)
                break;
            yield return null;
        }
        if (!www.isDone || !string.IsNullOrEmpty(www.error)) {
            Debug.LogError("Error: " + www.error);
            yield break;
        }
       var jsonResponse = www.text;
        if (IsVerbose)
            print(jsonResponse);

        onJsonSuccessful.Invoke();
        var dict = Json.Deserialize(jsonResponse) as Dictionary<string, object>;
        var story = (Dictionary<string, object>) dict["Story"];

        List<string> storyObjects = new List<string>();
        foreach (object item in (List<object>) story["ObjectArray"]) {
            storyObjects.Add(item.ToString());
        }
        string CorrectLocation = "";

        // TODO: Move to diffrent json object
        for(int i=0; i< storyObjects.Count; i++){
            if (CheckWords.StringContains(storyObjects[i], "desert", StringComparison.CurrentCultureIgnoreCase)) {
                CorrectLocation = "Desert";
                storyObjects.Remove(storyObjects[i]);
               break;
            }
            if (CheckWords.StringContains(storyObjects[i], "forest", StringComparison.CurrentCultureIgnoreCase)) { 
                CorrectLocation = "Forest";
                storyObjects.Remove(storyObjects[i]);
                break;
            }
            if (CheckWords.StringContains(storyObjects[i], "house", StringComparison.CurrentCultureIgnoreCase)) {
                CorrectLocation = "House";
                    storyObjects.Remove(storyObjects[i]);
                break;

            }
        }
        //TODO: Add more emotions and adjust current spelling to match Watson API
        string mood = story["Mood"] as string;
        if (mood.Equals("Joy"))
            mood = "Happy";
        if (mood.Equals("Anger"))
            mood = "Angry";
        if (mood.Equals("Sadness"))
            mood = "Sad";
        if (mood.Equals("Fear"))
            mood = "Sad";

        if (currentStory != null && ( currentStory.sceneObjects.Count == storyObjects.Count && currentStory.sceneObjects[0].name.Equals(storyObjects[0]) ) && 
            currentStory.location.Equals(CorrectLocation)) {
            StopCoroutine("RequestURL");
            yield return null;
        }
        else if (currentStory != null && currentStory.location == CorrectLocation  && currentStory.mood == mood) {
            StopCoroutine("RequestURL");
            yield return null;
        }
        else {

            if (storyObjects == null && IsVerbose)
                Debug.LogWarning("No Story Objects found");

            if (storyObjects != null && sceneObjects == null && IsVerbose)
                Debug.LogWarning("No Scene Objects found in Main Controller");
            List<GameObject> objectList = new List<GameObject>();
            if (storyObjects != null && sceneObjects != null) {

                foreach (string objectName in storyObjects) {

                    var item = sceneObjects.Find(e => e.name.Equals(objectName));
                    if (item != null)
                        objectList.Add(item);
                }

            }
            string setting = "";
            if (CheckWords.StringContains(story["Description"] as string, "day", StringComparison.CurrentCultureIgnoreCase))
                setting = "Day";
            else if (CheckWords.StringContains(story["Description"] as string, "night", StringComparison.CurrentCultureIgnoreCase))
                setting = "Night";
            currentStory = new Story(story["Description"] as string,
                  0,
                  story["Mood"] as string,
                  CorrectLocation, setting,
                  objectList);
            onCreatedStory.Invoke(currentStory);
        }
    }



    void TestJson(string json) {
        var jsonResponse = json;
        if (IsVerbose)
            print(jsonResponse);

        onJsonSuccessful.Invoke();
        var dict = Json.Deserialize(jsonResponse) as Dictionary<string, object>;
        var story = (Dictionary<string, object>) dict["Story"];
      
        List<string> storyObjects = new List<string>();
        foreach (object item in (List<object>)story["ObjectArray"]) {
            storyObjects.Add(item.ToString());
        }

        if (currentStory != null &&(currentStory.sceneObjects.Count == storyObjects.Count && currentStory.sceneObjects[0].name.Equals(storyObjects[0]) )) {
                return;
        }
       
        //Use This To Read Text over json
        //currentStory = CheckWords.createStory(story["Description"] as string);
        //onCreatedStory.Invoke(currentStory);
        //return;

        if (storyObjects == null && IsVerbose)
            Debug.LogWarning("No Story Objects found");

        if (storyObjects != null && sceneObjects == null && IsVerbose)
            Debug.LogWarning("No Scene Objects found in Main Controller");
        List<GameObject> objectList = new List<GameObject>();
        if (storyObjects != null && sceneObjects != null) {
           
            foreach (string objectName in storyObjects) {

                var item = sceneObjects.Find(e => e.name.Equals(objectName));
                if (item != null)
                    objectList.Add(item);
            }
          
        }
        currentStory = new Story(story["Description"] as string,
              float.Parse(story["Time"] as string),
              story["Mood"] as string,
              story["Location"] as string, story["Setting"] as string,
              objectList);
        onCreatedStory.Invoke(currentStory);

    }
    


    // Update is called once per frame
    void Update () {
	
	}
}
