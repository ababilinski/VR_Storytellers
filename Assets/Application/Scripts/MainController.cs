using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

   public  void Init(Story story) {
        StartCoroutine(LoadCorrectLevel(story.location));
    }
    IEnumerator LoadCorrectLevel(string level) {
        yield return new  WaitForSeconds(2f);
        yield return SceneController.Loader.LoadScene(level);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
