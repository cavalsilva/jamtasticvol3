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

    TransformGesture _gesture;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
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
        _beingHeld = true;
    }

    void StopDrag()
    {
        Debug.Log("Drag Stop: " + gameObject.name);
        _beingHeld = false;

        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(1 << LayerMask.NameToLayer("DropArea"));

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);
        Debug.Log(colliderCount);
    }
}