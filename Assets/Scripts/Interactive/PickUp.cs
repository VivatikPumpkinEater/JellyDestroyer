using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected CircleCollider2D _trigger = null;

    private void Start()
    {
        _trigger.isTrigger = false;

        Invoke("ActivateTrigger", 1f);
    }

    protected virtual void ActivateTrigger()
    {
        _trigger.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponentInParent<Player>();

        if (player)
        {
            Destroy(gameObject);
        }
    }
}
