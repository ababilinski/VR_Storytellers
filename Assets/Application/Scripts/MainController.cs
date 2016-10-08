using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using UnityEngine.Events;
public class MainController : MonoBehaviour {
   
    public Story currentStory { get; private set; }

    [SerializeField]
    private bool isVerbose;
    private static bool IsVerbose;
    [SerializeField]
    private List<GameObject> sceneObjects = new List<GameObject>();
    [SerializeField]
    private string jsonURL;

    public UnityEvent onJsonSuccessful;
    public UnityEvent onCreatedStory;
	// Use this for initialization
	void Start () {
        IsVerbose = isVerbose;
        StartCoroutine(RequestURL(jsonURL));
	}

    IEnumerator RequestURL(string url) {
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
        if(IsVerbose)
        print(jsonResponse);
        onJsonSuccessful.Invoke();
        var dict = Json.Deserialize(jsonResponse) as Dictionary<string, object>;
        var story = (Dictionary<string, object>) dict["Story"];
        var storyObjects = (List<string>) story["ObjectArray"];

        if (storyObjects == null && IsVerbose)
            Debug.LogWarning("No Story Objects found");
        if(storyObjects != null && sceneObjects == null && IsVerbose)
            Debug.LogWarning("No Scene Objects found in Main Controller");

        if (storyObjects != null && sceneObjects != null) {
            List<GameObject> objectList = new List<GameObject>();
            foreach (string objectName in storyObjects) {
                var item = sceneObjects.Find(e => e.name == objectName);
                if (item != null)
                    objectList.Add(item);
            }
            currentStory = new Story(story["Description"] as string,
                float.Parse(story["Time"] as string),
                story["Mood"] as string,
                story["Location"] as string, objectList);
            }
        onCreatedStory.Invoke();


        }


    


    // Update is called once per frame
    void Update () {
	
	}
}
