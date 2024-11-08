using UnityEngine;
using YG;

namespace WorkObjects.Handlers
{
    [RequireComponent(typeof(SceneLoader))]
    public class AuthorizationHandler : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        private void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            YandexGame.GetDataEvent += Authorize;
        }

        private void Authorize()
        {
            _sceneLoader.LoadScene((int)SceneNames.MainMenu);
        }
    }
}