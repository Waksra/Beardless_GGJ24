using UnityEngine;

namespace Combat
{
 public struct HitData
    {
        public Vector3 Force;

        public HitData(Vector3 force)
        {
            Force = force;
        }
    }    
}