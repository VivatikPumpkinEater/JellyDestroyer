using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fly = null;
    [SerializeField] private HealthManager _hp = null;

    [SerializeField] private float _speed = 5f;

    [SerializeField] private float _pause = 3f;  
}
