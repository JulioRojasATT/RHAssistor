using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour {

    [SerializeField] private FloatScriptableValue loadProgress;

    public void LoadSceneAdditive(string sceneName) {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Additive);
    }

    public void ReloadCurrentScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void LoadSceneByIndex(int index) {
        SceneManager.LoadScene(index);
    }
    
    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAsync(string sceneName) {
        StartCoroutine(LoadSceneAsyncCoroutine(SceneManager.LoadSceneAsync(sceneName)));
    }

    public IEnumerator LoadSceneAsyncCoroutine(AsyncOperation sceneLoadOperation) {
        loadProgress.SetValue(0);
        while (!sceneLoadOperation.isDone) {
            yield return new WaitForEndOfFrame();
            loadProgress.SetValue(sceneLoadOperation.progress);
        }
    }
}
