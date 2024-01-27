using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat
{
    enum HitType
    {
        HitDirection,
        Custom,
        Forward
    }

    public class Hitter : MonoBehaviour
    {
        [SerializeField] private float hitForce = 10f;
        [SerializeField] private HitType hitType = HitType.HitDirection;

        [SerializeField, EnableIf("hitType", HitType.Custom)]
        private Vector3 customHitDirection = Vector3.zero;
        
        private List<Collider> hitColliders = new List<Collider>();

        [Button, EnableIf("hitType", HitType.Custom)]
        private void NormalizeCustomHitDirection()
        {
            customHitDirection = customHitDirection.normalized;
        }

        public float HitForce
        {
            get => hitForce;
            set => hitForce = value;
        }

        private void Start()
        {
            NormalizeCustomHitDirection();
        }

        private void FixedUpdate()
        {
            hitColliders.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Hittable hittable) && !hitColliders.Contains(other))
            {
                Vector3 direction = GetDirection(other);
                hittable.Hit(new HitData(direction * hitForce));
                
                hitColliders.Add(other);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Hittable hittable) && !hitColliders.Contains(other.collider))
            {
                Vector3 direction = GetDirection(other);

                hittable.Hit(new HitData(direction * hitForce));
                
                hitColliders.Add(other.collider);
            }
        }

        private Vector3 GetDirection(Collision other)
        {
            switch (hitType)
            {
                case HitType.HitDirection:
                    return (other.transform.position - transform.position).normalized;
                case HitType.Custom:
                    return transform.localToWorldMatrix.MultiplyVector(customHitDirection);
                case HitType.Forward:
                    return transform.forward;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Vector3 GetDirection(Collider other)
        {
            switch (hitType)
            {
                case HitType.HitDirection:
                    return (other.transform.position - transform.position).normalized;
                case HitType.Custom:
                    return transform.localToWorldMatrix.MultiplyVector(customHitDirection);
                case HitType.Forward:
                    return transform.forward;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}