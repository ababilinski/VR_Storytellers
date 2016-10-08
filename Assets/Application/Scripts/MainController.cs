using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
    SceneSetUp sceneSetUp;
    Story story;
   public  void Init(Story data) {
        story = data;
        StartCoroutine(LoadCorrectLevel(story.location));
        
    }
    IEnumerator LoadCorrectLevel(string level) {
        yield return new  WaitForSeconds(3f);
        yield return SceneController.Loader.LoadScene(level);
       

    }
    void LoadOtherObjects() {
        sceneSetUp = FindObjectOfType<SceneSetUp>();
        sceneSetUp.SetUp(story);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
