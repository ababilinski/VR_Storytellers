using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public static SceneController Loader;


    void Awake() {
        if (Loader == null) {
            Loader = this;
        }
        else if (Loader != null && Loader != this) {
            Destroy(this);
        }
    }

   public Coroutine LoadScene(string sceneName) {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
            return StartCoroutine(LoadSceneCoroutine(sceneName));
        else {
            Debug.LogWarning("The scene:" + sceneName + " is not found");
            return StartCoroutine(SkipFrame());

        }


    }
    IEnumerator SkipFrame() {
        yield return new WaitForEndOfFrame();
    }

    IEnumerator LoadSceneCoroutine(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!operation.isDone) {
            yield return new WaitForEndOfFrame();
        }
    }
}
