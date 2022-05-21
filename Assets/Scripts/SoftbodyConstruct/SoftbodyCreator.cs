using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftbodyCreator : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> _rbs = new List<Rigidbody2D>();

    [SerializeField] private Rigidbody2D _center = null;

    [SerializeField] private float _frenq = 5, _frenqCenter = 3;

    private void SpringsFormation()
    {
    }
}
