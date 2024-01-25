using System;
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


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Hittable hittable))
            {
                Vector3 direction = GetDirection(other);
                hittable.Hit(new HitData(direction * hitForce));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Hittable hittable))
            {
                Vector3 direction = GetDirection(other);

                hittable.Hit(new HitData(direction * hitForce));
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