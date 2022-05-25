using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FixedJoint2D))]
public class SoftbodyCreator : MonoBehaviour
{
    [SerializeField] private List<Rigidbody2D> _rbs = new List<Rigidbody2D>();

    [SerializeField] private Rigidbody2D _center = null;

    [SerializeField] private float _mass = 1f, _gravitScale = 1f, _freq = 5, _freqCenter = 3, _dampingRation = 0.05f;

    private Dictionary<Rigidbody2D, SpringJoint2D[]> _bones = new Dictionary<Rigidbody2D, SpringJoint2D[]>();
    private List<SpringJoint2D> _centerSprings = new List<SpringJoint2D>();

    /*private void Awake()
    {
        SpringsFormation();
        SpringsCreate();
    }*/

    [ContextMenu("Create Springs")]
    private void CreateFormation()
    {
        SpringsFormation();
        SpringsCreate();
        CenterSprings();
    }

    [ContextMenu("Update Springs")]
    private void UpdateSprings()
    {
        SpringsCreate();
        UpdateCenterSprings();
    }
    
    private void SpringsFormation()
    {
        foreach (var rb in _rbs)
        {
            _bones.Add(rb, new SpringJoint2D[3]);
            rb.gameObject.AddComponent<RigidbodyInfo>();

            for (int i = 0; i < 3; i++)
            {
                var spring = rb.gameObject.AddComponent<SpringJoint2D>();

                _bones[rb][i] = spring;
            }
        }
    }

    private void SpringsCreate()
    {
        for (int i = 0; i < _bones.Keys.Count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (j + 1 != 3)
                {
                    _bones[_rbs[i]][j] = SpringSetting(_bones[_rbs[i]][j], _freq, _dampingRation);
                }
                else
                {
                    _bones[_rbs[i]][j] = SpringSetting(_bones[_rbs[i]][j], _freqCenter, _dampingRation);
                }
            }

            if (i - 1 > 0)
            {
                _bones[_rbs[i]][0].connectedBody = _rbs[i - 1];
            }
            else
            {
                _bones[_rbs[i]][0].connectedBody = _rbs[_rbs.Count - 1];
            }

            if (i + 1 < _bones.Keys.Count)
            {
                _bones[_rbs[i]][1].connectedBody = _rbs[i + 1];
            }
            else
            {
                _bones[_rbs[i]][1].connectedBody = _rbs[0];
            }

            _bones[_rbs[i]][2].connectedBody = _center;
        }
    }

    private void CenterSprings()
    {
        int springsCount = _rbs.Count / 4;
        
        for (int i = 0; i < springsCount; i++)
        {
            var spring = _center.gameObject.AddComponent<SpringJoint2D>();
            
            _centerSprings.Add(spring);
        }

        UpdateCenterSprings();
    }

    private void UpdateCenterSprings()
    {
        int springsCount = _rbs.Count / 4;
        
        for (int i = 0; i < _centerSprings.Count; i++)
        {
            _centerSprings[i].connectedBody = _rbs[i + springsCount];
            _centerSprings[i] = SpringSetting(_centerSprings[i], _freqCenter, _dampingRation);
        }
    }

    private SpringJoint2D SpringSetting(SpringJoint2D spring, float freq, float dampingRation)
    {
        spring.autoConfigureConnectedAnchor = true;
        
        spring.frequency = freq;
        spring.dampingRatio = dampingRation;

        return spring;
    }
}