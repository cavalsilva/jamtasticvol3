using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColisor : MonoBehaviour {

    Collider2D myCollider;
    int numColliders = 1;

    private void Awake()
    {
        myCollider = gameObject.GetComponent<Collider2D>();
    }

    void Update () {
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);
        Debug.Log(colliderCount);
    }
}
