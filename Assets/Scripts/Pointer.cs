using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private float _angel = 0f;
    [SerializeField] private GameObject _pointer = null;

    private Vector2 _target = Vector2.zero;

    private void Start()
    {
        RoomGeneratorV4.Instance.PortalPosition += SetTarget;
    }

    private void Update()
    {
        if (_target != Vector2.zero)
        {
            LookAt();
        }
    }

    private void LookAt()
    {
        Vector2 dir = ((Vector3)_target - transform.position).normalized;

        float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rot - _angel);
    }

    private void SetTarget(Vector2 position)
    {
        _target = position;
        
        _pointer.SetActive(true);
    }
}
