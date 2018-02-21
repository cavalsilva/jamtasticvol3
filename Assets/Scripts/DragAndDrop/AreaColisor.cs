using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaColisor : MonoBehaviour {

    void Update () {
        Collider2D myCollider = gameObject.GetComponent<Collider2D>();

        //Quantidade máxima de colisores que podem colidir com o objeto
        int numColliders = 10;

        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);
        Debug.Log(colliderCount);
    }
}
