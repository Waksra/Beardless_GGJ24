using Combat;
using UnityEngine;

namespace General
{
    public class RagdollController : MonoBehaviour
    {
        private Rigidbody[] bodies;
        private Collider[] colliders;

        private bool isRagdollEnabled = true;

        private void Awake()
        {
            bodies = GetComponentsInChildren<Rigidbody>();
            colliders = GetComponentsInChildren<Collider>();

            DisableRagdoll();
        }

        public void EnableRagdoll()
        {
            if (isRagdollEnabled)
                return;

            foreach (var body in bodies)
            {
                body.isKinematic = false;
            }

            foreach (var coll in colliders)
            {
                coll.enabled = true;
            }

            isRagdollEnabled = true;
        }


        public void DisableRagdoll()
        {
            if (!isRagdollEnabled)
                return;

            foreach (var body in bodies)
            {
                body.velocity = Vector3.zero;
                body.isKinematic = true;
            }

            foreach (var coll in colliders)
            {
                coll.enabled = false;
            }

            isRagdollEnabled = false;
        }

        public void ApplyForce(Vector3 force)
        {
            if (!isRagdollEnabled)
                return;

            foreach (var body in bodies)
            {
                body.AddForce(force, ForceMode.Impulse);
            }
        }
        
        public void ApplyForce(Vector3 force, Vector3 position)
        {
            if (!isRagdollEnabled)
                return;

            foreach (var body in bodies)
            {
                body.AddForceAtPosition(force, position, ForceMode.Impulse);
            }
        }
        
        public void HitResponse(HitData hitData)
        {
            EnableRagdoll();
            ApplyForce(hitData.Force);
        }
        
        public void Recover()
        {
            DisableRagdoll();
        }
    }
}