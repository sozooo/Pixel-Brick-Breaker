using System.Globalization;
using Project.Scripts.CannonSystem;
using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Cannon
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class BulletCountDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
    
        private Shooter _shooter;
    
        private void OnDisable()
        {
            _shooter.BulletCountChanged -= SetNumber;
        }
    
        public void Initialize(Shooter shooter)
        {
            _shooter = shooter;
        
            _shooter.BulletCountChanged += SetNumber;
        }

        private void SetNumber(float number)
        {
            _text.text = number.ToString(CultureInfo.InvariantCulture);
        }
    }
}
