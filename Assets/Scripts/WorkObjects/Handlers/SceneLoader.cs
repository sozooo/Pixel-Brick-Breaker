using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private bool _sceneLoaded = false;
    private Coroutine _asyncSceneLoad;

    public void LoadScene(int sceneId)
    {
        if(_asyncSceneLoad == null)
            _asyncSceneLoad = StartCoroutine(LoadSceneAsync(sceneId));
    }

    private IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneId);

        load.completed += operation => _sceneLoaded = true;

        yield return new WaitUntil(() => _sceneLoaded == true);

        _asyncSceneLoad = null;
    }
}
