using UnityEngine;
using YG;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseRange = 0.5f;
    [SerializeField] private float _rangeMultiplier = 0.2f;
    [SerializeField] private float _strength;
    [SerializeField] private LayerMask _figureLayer;
    
    [SerializeField] private Ricocheter _ricocheter;

    private void OnEnable()
    {
        _ricocheter.FigureCollided += ExplodeFigure;
    }

    private void OnDisable()
    {
        _ricocheter.FigureCollided -= ExplodeFigure;
    }

    private void ExplodeFigure(ContactPoint contact, IDamageable figure)
    {
        figure.ApplyDamage(contact.point, _baseRange + _rangeMultiplier * YG2.saves.BlastRadiusLevel);
    }
}
