using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Behaviors;

public class AreaColisor : MonoBehaviour
{
    Transform _transform;

    Collider2D myCollider;
    int numColliders = 1;

    bool _beingHeld = false;


    PressGesture _press;
    ReleaseGesture _release;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        myCollider = gameObject.GetComponent<Collider2D>();

        _press = GetComponent<PressGesture>();
        _press.OnPress.AddListener(StartDrag);

        _release = GetComponent<ReleaseGesture>();
        _release.OnRelease.AddListener(StopDrag);
    }

    void StartDrag(Gesture gesture)
    {
        Debug.Log("Drag Start");
        _beingHeld = true;
    }

    void StopDrag(Gesture gesture)
    {
        _beingHeld = false;

        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(1 << LayerMask.NameToLayer("DropArea"));

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);
        Debug.Log(colliderCount);
    }

    private void Update()
    {
        if(_beingHeld)
        {
            Vector2 mousePos = Input.mousePosition;

            _transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        }
    }
}