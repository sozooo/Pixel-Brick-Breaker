using System;
using System.Collections;
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
    
    // [SerializeField] private Rigidbody _rigidbody;
    
    private readonly int Disapear = Animator.StringToHash("Disapear");
    private Vector3 _position;

    private Coroutine _falling;
    private Animator _animator;
    private TrailRenderer _trail;

    public event Action<Voxel> Fell;

    public Vector3 Position => _position;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _trail = GetComponent<TrailRenderer>();
        
        // _rigidbody.isKinematic = true;
    }

    public void SetPosition(Vector3 position)
    {
        _position = position;
    }

    public void RemoveRigidbody()
    {
        // _animator.SetBool(DisapearAnimation, false);
        // _trail.enabled = false;
        //
        // if (TryGetComponent(out Rigidbody rigidbody))
        //     Destroy(rigidbody);
    }

    public void Fall()
    {
        if (TryGetComponent(out Rigidbody rigidbody) == false)
            gameObject.AddComponent<Rigidbody>();
        
        _trail.enabled = true;
        
        gameObject.layer = Mathf.RoundToInt(Mathf.Log(_layer.value, 2));;

        _falling ??= StartCoroutine(Falling());

        Fell?.Invoke(this);
        
        MessageBrokerHolder.Figure.Publish(new M_VoxelFell());
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
}
