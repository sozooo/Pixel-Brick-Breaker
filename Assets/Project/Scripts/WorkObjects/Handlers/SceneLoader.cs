using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public async UniTaskVoid LoadScene(int sceneId)
    {
        await SceneManager.LoadSceneAsync(sceneId);
    }
}
