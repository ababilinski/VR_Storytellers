using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {
    [SerializeField]
    SceneSetUp sceneSetUp;
    Story story;
    [SerializeField]
    string IntroSong;
    void Start() {
        SoundController.Instance.Play(IntroSong);
    }
   public  void Init(Story data) {
        story = data;
        StartCoroutine(LoadCorrectLevel(story.location));
        
    }
    IEnumerator LoadCorrectLevel(string level) {
        yield return new  WaitForSeconds(10f);
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
