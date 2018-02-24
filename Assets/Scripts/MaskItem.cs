using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jamtasticvol3
{
    public class MaskItem : MonoBehaviour
    {
        public List<GameManager.Features> features;

        public void Start()
        {
            Draggable draggable = GetComponent<Draggable>();

            draggable.OnCollisionDetected += () =>
            {
                GameManager.Instance.AddMaskFeatures(gameObject.name, features);
            };

            draggable.OnNoCollisionDetected += () =>
            {
                GameManager.Instance.RemoveMaskFeatures(gameObject.name);
            };
        }
    }
}