using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Behaviors;
using DG.Tweening;
using jamtasticvol3;
using jamtasticvol3.Utils;

namespace jamtasticvol3
{
    public class Draggable : MonoBehaviour
    {
        public event Delegates.SimpleEvent OnCollisionDetected, OnNoCollisionDetected;

        public LayerMask layers;
        public bool snaps;
        public Vector2 snapTo;
        public Vector2 scale;

        public bool scaleCollider = false;
        public Vector2 colliderOffset;
        public Vector2 colliderScale;

        Transform _transform;
        Collider2D _collider;
        Image _image;

        int numColliders = 1;
        bool _canSpawn = true;

        RectTransform maskContainer;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _collider = gameObject.GetComponent<Collider2D>();
            _image = GetComponent<Image>();

            maskContainer = GameObject.Find("MaskContainer").transform as RectTransform;

            GetComponent<TransformGesture>().StateChanged += OnGestureChange;
        }

        void OnGestureChange(object sender, GestureStateChangeEventArgs e)
        {
            switch (e.State)
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
            if (_canSpawn)
            {
                _canSpawn = false;
                Instantiate(gameObject, gameObject.transform.parent);

                _transform.SetParent(maskContainer);
            }

            if (SFXController.Instance != null)
                SFXController.Instance.PlaySoundDrag();

            if (scaleCollider)
            {
                ((BoxCollider2D)_collider).offset = colliderOffset;
                ((BoxCollider2D)_collider).size = colliderScale;
            }

            ((RectTransform)_transform).DOSizeDelta(scale, 0.5f).SetEase(Ease.OutElastic).Play();
        }

        void StopDrag()
        {
            if(SFXController.Instance != null)
                SFXController.Instance.PlaySoundClick();

            Collider2D[] colliders = new Collider2D[numColliders];
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(layers);

            if (_collider.OverlapCollider(contactFilter, colliders) < 1)
            {
                _transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => GameObject.Destroy(gameObject)).Play();

                if (OnNoCollisionDetected != null)
                    OnNoCollisionDetected();
            }
            else
            {
                if (OnCollisionDetected != null)
                    OnCollisionDetected();

                _transform.SetParent(colliders[0].transform);
                if(snaps) _transform.localPosition = snapTo;
            }
        }
    }
}