using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneLoader : MonoBehaviour
{
    public async UniTaskVoid LoadScene(int sceneId)
    {
        if(YG2.saves.IsAdRemoved == false)
            YG2.InterstitialAdvShow();
        
        await SceneManager.LoadSceneAsync(sceneId);
    }
}
