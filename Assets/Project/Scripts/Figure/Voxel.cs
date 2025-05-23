using System;
using System.Collections;
using com.cyborgAssets.inspectorButtonPro;
using Project.Scripts.WorkObjects.MessageBrokers;
using Project.Scripts.WorkObjects.MessageBrokers.Figure;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(TrailRenderer))]
public class Voxel : MonoBehaviour
{
    [SerializeField] private float _timeToDisapear = 3;
    [SerializeField] private float _animationLength = 0.5f;
    [SerializeField] private LayerMask _layer;
    
    private readonly int Disapear = Animator.StringToHash("Disapear");

    private Coroutine _falling;
    private Animator _animator;
    private TrailRenderer _trail;

    public event Action<Voxel> Fell;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _trail = GetComponent<TrailRenderer>();
    }

    private void OnDisable()
    {
        InvokeFell();
    }

    [ProPlayButton]
    public void Fall()
    {
        if (TryGetComponent(out Rigidbody rigidbody) == false)
            gameObject.AddComponent<Rigidbody>();
        
        _trail.enabled = true;
        
        gameObject.layer = Mathf.RoundToInt(Mathf.Log(_layer.value, 2));;

        _falling ??= StartCoroutine(Falling());

        InvokeFell();
    }

    private IEnumerator Falling()
    {
        WaitForSeconds wait = new(_timeToDisapear);
        WaitForSeconds waitForAnimationEnd = new(_animationLength);

        yield return wait;

        _animator.SetBool(Disapear, true);

        yield return waitForAnimationEnd;

        gameObject.SetActive(false);

        _falling = null;
    }

    private void InvokeFell()
    {
        Fell?.Invoke(this);
        
        MessageBrokerHolder.Figure.Publish(new M_VoxelFell());
    }
}
