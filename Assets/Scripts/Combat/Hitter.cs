using UnityEngine;

namespace Combat
{
    public class Hitter : MonoBehaviour
    {
        [SerializeField] private float hitForce = 10f;
        
        public float HitForce
        {
            get => hitForce;
            set => hitForce = value;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Hittable hittable))
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                hittable.Hit(new HitData(direction * hitForce));
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out Hittable hittable))
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                hittable.Hit(new HitData(direction * hitForce));
            }
        }
    }
}