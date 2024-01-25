using UnityEngine;

namespace Combat
{
    public class Hittable : MonoBehaviour
    {
        private Rigidbody body;
        
        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }
        
        public void Hit(HitData hitData)
        {
            body.AddForce(hitData.Force, ForceMode.Impulse);
        }
    }
}