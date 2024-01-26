using FMODUnity;
using UnityEngine;

namespace SoundUtils
{
    public class SpeedFMODParameterSetter : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10f;

        [SerializeField, ParamRef] private string parameterName;  
        
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }
        
        private void Update()
        {
            float speed = body.velocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);
            
            RuntimeManager.StudioSystem.setParameterByName(parameterName, normalizedSpeed);
        }
    }
}