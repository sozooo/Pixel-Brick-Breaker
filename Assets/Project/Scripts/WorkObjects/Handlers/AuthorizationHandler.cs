using Project.Scripts.WorkObjects.Enums;
using UnityEngine;
using YG;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class AuthorizationHandler : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;

        private void OnEnable()
        {
            YG2.onGetSDKData += Authorize;
        }
        
        private void OnDisable()
        {
            YG2.onGetSDKData -= Authorize;
        }

        private void Authorize()
        {
            _sceneLoader.LoadScene((int)SceneNames.MainMenu).Forget();
        }
    }
}