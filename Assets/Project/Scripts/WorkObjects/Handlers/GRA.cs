using UnityEngine;
using YG;

namespace Project.Scripts.WorkObjects.Handlers
{
    public class GRA : MonoBehaviour
    {
        private void Start() =>
            YG2.GameReadyAPI();
    }
}