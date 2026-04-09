using UnityEngine;
using YG;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class AdvPauseAndSoundWatcher : MonoBehaviour
    {
        private void OnEnable()
        {
            YG2.onOpenAnyAdv += Mute;
            YG2.onCloseAnyAdv += Unmute;
        }

        private void OnDisable()
        {
            YG2.onOpenAnyAdv -= Mute;
            YG2.onCloseAnyAdv -= Unmute;
        }

        private void Mute()
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }

        private void Unmute()
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
        }
    }
}