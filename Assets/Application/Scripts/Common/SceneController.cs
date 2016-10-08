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
        return StartCoroutine(LoadSceneCoroutine(sceneName));
       
    }


    IEnumerator LoadSceneCoroutine(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!operation.isDone) {
            yield return new WaitForEndOfFrame();
        }
    }
}
