using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickUp : MonoBehaviour
{
    [SerializeField] protected float _TimeToUse = 1f;
    [SerializeField] protected CircleCollider2D _Trigger = null;

    private OnVisibleObject _visibleObject = null;
    private bool _visible = false;
    
    private Coroutine _timer = null;

    private void Start()
    {
        if (_visibleObject != null)
        {
            _visibleObject.Visible += VisibleStatus;
        }
        _Trigger.isTrigger = false;

        //Invoke("ActivateTrigger", 1f);
        _timer = StartCoroutine(Timer(_TimeToUse));
    }

    private void VisibleStatus(bool status)
    {
        _visible = status;
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        ActivateTrigger();

        if (_timer != null)
        {
            StopCoroutine(_timer);
        }

        _timer = null;
    }

    protected virtual void ActivateTrigger()
    {
        _Trigger.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Interactive");
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponentInParent<Player>();

        if (player)
        {
            //Destroy(gameObject, 0.1f);
            GoToUI();
        }
    }

    protected void GoToUI()
    {
        gameObject.layer = LayerMask.NameToLayer("NoInteractive");

        transform.DOMove(GameManager.Instance.PropsToUI(), 2f).OnComplete(() => Destroy(gameObject));
    }
}