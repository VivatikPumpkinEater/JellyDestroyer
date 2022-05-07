using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float _Speed = 3f;
    [SerializeField] protected float _ReloadTimeAVG = 3f;
    [SerializeField] protected float _ToShootTime = 3f;
    [SerializeField] protected Animator _FaceAnimator = null;
    [SerializeField] protected LayerMask _EnemyTargetLayer;

    [SerializeField] protected float _Angel = 0f;

    protected Player _Player = null;
    protected bool _VisibleStatus = false;

    protected float _TimerReload = float.MinValue, _TimerToShoot = float.MinValue;
    
    private Animator _animator = null;

    protected Animator _Anim
    {
        get => _animator = _animator ?? GetComponent<Animator>();
    }
    
    private OnVisibleObject _onVisibleObject = null;

    protected OnVisibleObject _VisibleObject
    {
        get => _onVisibleObject = _onVisibleObject ?? GetComponentInChildren<OnVisibleObject>();
    }

    private HealthManager _healthManager = null;

    private HealthManager _hp
    {
        get => _healthManager = _healthManager ?? GetComponent<HealthManager>();
    }

    private Rigidbody2D _rigidbody2D = null;

    protected Rigidbody2D _Rb
    {
        get => _rigidbody2D = _rigidbody2D ?? GetComponent<Rigidbody2D>();
    }
    

    protected virtual void Awake()
    {
        _VisibleObject.Visible += ChangeStatus;
        
        ResetTimer();
    }

    protected void Start()
    {
        _Player = Player.Instance;
    }

    private void ChangeStatus(bool visible)
    {
        _VisibleStatus = visible;
    }

    protected virtual void Update()
    {

    }

    protected virtual void Movement()
    {
        throw new NotImplementedException();
    }

    protected virtual void Attack(Quaternion q)
    {

    }

    public virtual void EndAttack()
    {
        Vector2 direction = (transform.position - _Player.transform.position).normalized;
        
        _Rb.AddForce(direction * 1f * _Speed, ForceMode2D.Impulse);
    }

    protected virtual void ResetTimer()
    {
        _TimerReload = Random.Range(_ReloadTimeAVG - 1f, _ReloadTimeAVG + 1.1f);
        _TimerToShoot = _ToShootTime;
    }
}
