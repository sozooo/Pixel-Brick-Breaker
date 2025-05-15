using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(TrailRenderer))]
public class Voxel : MonoBehaviour
{
    [SerializeField] private float _timeToDisapear = 3;
    [SerializeField] private float _animationLength = 0.5f;
    
    [SerializeField] private Audio _audio;
    // [SerializeField] private Rigidbody _rigidbody;

    private const string DisapearAnimation = "Disapear";
    private const string FallingVoxelLayer = "Falling Voxel";

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

    private void OnDisable()
    {
        Fell?.Invoke(this);
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

        _audio.PlayOneShot();
        
        _trail.enabled = true;
        gameObject.layer = LayerMask.NameToLayer(FallingVoxelLayer);

        _falling ??= StartCoroutine(Falling());

        Fell?.Invoke(this);
    }

    private IEnumerator Falling()
    {
        WaitForSeconds wait = new(_timeToDisapear);
        WaitForSeconds waitForAnimationEnd = new(_animationLength);

        yield return wait;

        _animator.SetBool(DisapearAnimation, true);

        yield return waitForAnimationEnd;

        gameObject.SetActive(false);

        _falling = null;
    }
}
