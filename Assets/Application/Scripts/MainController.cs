using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
    [SerializeField]
    SceneSetUp sceneSetUp;
    Story story;
   public  void Init(Story data) {
        story = data;
        StartCoroutine(LoadCorrectLevel(story.location));
        
    }
    IEnumerator LoadCorrectLevel(string level) {
        yield return new  WaitForSeconds(3f);
        yield return SceneController.Loader.LoadScene(level);
        LoadOtherObjects();

    }
    void LoadOtherObjects() {
        sceneSetUp = FindObjectOfType<SceneSetUp>();
        if(sceneSetUp)
        sceneSetUp.SetUp(story);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
