using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
    public class Hittable : MonoBehaviour
    {
        private Rigidbody body;
        
        [SerializeField] private UnityEvent onHit;
        [SerializeField] private UnityEvent<HitData> onHitWithData;
        
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        public void Hit(HitData hitData)
        {
            body.AddForce(hitData.Force, ForceMode.Impulse);
            onHit?.Invoke();
            onHitWithData?.Invoke(hitData);
        }
    }
}