using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(HealthManager))]
public class FlyEnemy : EnemyBase
{
    [SerializeField] private FireBall _fireBall = null;

    private AudioSource _audioSource = null;

    private AudioSource _audio
    {
        get => _audioSource = _audioSource ?? GetComponent<AudioSource>();
    }
    
    private bool _idle = true;

    protected override void Update()
    {
        if (_VisibleObject)
        {
            _TimerReload -= Time.deltaTime;
            if (_TimerReload <= 0)
            {
                _FaceAnimator.SetBool("Attack", true);
                _idle = false;

                Vector2 dir = (_Player.transform.position - transform.position).normalized;

                float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, rot - _Angel);

                _TimerToShoot -= Time.deltaTime;

                if (_TimerToShoot <= 0)
                {
                    ResetTimer();
                    Attack(transform.rotation = Quaternion.Euler(0f, 0f, rot - _Angel/2));
                }
            }
        }
    }

    protected override void Attack(Quaternion q)
    {
        _audio.clip = AudioManager.Instance.GetSound("FireBallShoot");
        _audio.Play();
        
        _FaceAnimator.SetBool("Attack", false);
        _idle = true;
        
        var fireBall = Instantiate(_fireBall, transform.position, q);
        
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Fly()
    {
        _Rb.velocity = Vector2.up * _Speed;

        if (_VisibleStatus && Vector2.Distance(transform.position, _Player.transform.position) > 3f && _idle)
        {
            Vector2 direction = (_Player.transform.position - transform.position).normalized;
            _Rb.AddForce(direction * _Speed, ForceMode2D.Impulse);
        }
    }
}