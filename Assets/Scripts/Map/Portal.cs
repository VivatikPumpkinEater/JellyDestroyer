using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool _triggerEnter = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        int playerLayer = LayerMask.NameToLayer("Player");
        
        if (col.gameObject.layer == playerLayer && !_triggerEnter)
        {
            _triggerEnter = true;
            
            Debug.Log("Player in portal");
            UIManager.Instance.StartTransition();
        }
    }
}
