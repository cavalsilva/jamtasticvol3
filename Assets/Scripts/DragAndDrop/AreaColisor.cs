using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Behaviors;
using DG.Tweening;

public class AreaColisor : MonoBehaviour
{
    Transform _transform;
    SpriteRenderer _spriteRenderer;
    Collider2D myCollider;
    int numColliders = 1;

    TransformGesture _gesture;

    bool _canSpawn = true;

    public GameManager.AreaColisor areaColisor;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<Collider2D>();

        _gesture = GetComponent<TransformGesture>();
        _gesture.StateChanged += OnGestureChange;
    }

    void OnGestureChange(object sender, GestureStateChangeEventArgs e)
    {
        switch(e.State)
        {
            case Gesture.GestureState.Began:
                StartDrag();
                break;
            case Gesture.GestureState.Ended:
                StopDrag();
                break;
            default:
                break;
        }
    }

    void StartDrag()
    {
        Debug.Log("Drag Start: " + gameObject.name);

        if (_canSpawn)
        {
            _canSpawn = false;
            Instantiate(gameObject);

            _spriteRenderer.enabled = true;
        }

        _transform.DOScale(Vector3.one * 1.2f, 0.5f).SetEase(Ease.OutElastic).Play();
    }

    void StopDrag()
    {
        Debug.Log("Drag Stop: " + gameObject.name);

        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(1 << LayerMask.NameToLayer("DropArea"));

        if (myCollider.OverlapCollider(contactFilter, colliders) < 1)
            _transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => GameObject.Destroy(gameObject)).Play();
    }
}