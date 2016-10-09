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

                yield return new WaitForSeconds(10);
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


        if (currentStory != null && ( currentStory.sceneObjects.Count == storyObjects.Count && currentStory.sceneObjects[0].name.Equals(storyObjects[0]) ) && 
            currentStory.location.Equals(story["Location"] as string)) {
            StopCoroutine("RequestURL");
            yield return null;
        }
        else if (currentStory != null && currentStory.location == (story["Mood"] as string) && currentStory.sceneObjects.Count == storyObjects.Count &&
            currentStory.location.Equals(story["Location"] as string)) {
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
            currentStory = new Story(story["Description"] as string,
                  float.Parse(story["Time"] as string),
                  story["Mood"] as string,
                  story["Location"] as string, story["Setting"] as string,
                  objectList);
            onCreatedStory.Invoke(currentStory);
        }
    }

    //Story ReadStoryText(string storyText) {
    //    string setting = "";
    //    string Mood="";
    //    string location="";
    //    //Setting
    //    if (storyText.Contains("forest")) {
    //        location = "Forest";
    //    }
    //    else if (storyText.Contains("desert")) {
    //        location = "desert";
    //    }
    //    else if (storyText.Contains("house")) {
    //        location = "House";
    //    }
    //    //Mood
    //    if (storyText.Contains("sad")) {
    //        Mood = "Sad";
    //    }
    //    else if (storyText.Contains("happy"))
    //        Mood = "Happy";
    //    else if (storyText.Contains("relaxed"))
    //        Mood = "Relaxed";
    //    else if (storyText.Contains("angry") || storyText.Contains("barking"))
    //        Mood = "Angry";

    //    //Setting
    //    if (storyText.Contains("day")) {
    //        setting = "Day";
    //    }
    //    else if (storyText.Contains("night")) {
    //        setting = "Night";
    //    }
    //    List<GameObject> obj = new List<GameObject>();
    //    foreach (GameObject thing in sceneObjects) {
    //        if (Contains(storyText, thing.name, StringComparison.CurrentCultureIgnoreCase)){
    //            obj.Add(thing);
    //        }
    //    }
    //    return new Story(storyText, 1, Mood, location, setting, obj);
    //}
    //public  bool Contains(string source, string toCheck, StringComparison comp) {
    //    return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
    //}



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
       
        //UseThisToReadText
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
