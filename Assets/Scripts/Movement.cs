using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _face = null;
    [SerializeField] private List<Rigidbody2D> _rb = new List<Rigidbody2D>();

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpPower = 5f;

    private Vector2 _screenSize;
    private void Start()
    {
        _screenSize = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump(Vector2.up * 2 + Vector2.left);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Jump(Vector2.up * 2 + Vector2.right);
        }

        if (Input.touchCount > 0)
        {
            var t = Input.touches[Input.touchCount - 1];

            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x < (_screenSize.x / 2))
                {
                    Jump(Vector2.up * 2 + Vector2.left);
                }
                else
                {
                    Jump(Vector2.up * 2 + Vector2.right);
                }
            }
        }
    }

    private void Jump(Vector2 dir)
    {
        _face.velocity = Vector2.zero;
        _face.angularVelocity = 0;

        _face.velocity = new Vector2(_face.velocity.x, 0);
        _face.velocity += dir * _jumpPower;

        /*List<Rigidbody2D> rbs = new List<Rigidbody2D>();*/

        for (int i = 0; i < _rb.Count; i++)
        {
            /*Debug.Log(_face.position);
            Debug.Log(Vector2.Distance(_face.position * dir, _rb[i].position) + " " + i);*/
            
            _rb[i].velocity = Vector2.zero;
            _rb[i].angularVelocity = 0;

            _rb[i].velocity = new Vector2(_rb[i].velocity.x, 0);
            _rb[i].velocity += dir * (_jumpPower / 2);
        }
    }

    private void SaveTest()
    {
    }
}