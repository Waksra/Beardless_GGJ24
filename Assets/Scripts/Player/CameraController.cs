using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField, Range(-180, 180)] private float minPitch = -75f;
        [SerializeField, Range(-180, 180)] private float maxPitch = 75f;

        private new Transform transform;

        float xRotation;

        private void Awake()
        {
            transform = GetComponent<Transform>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            xRotation = transform.localRotation.eulerAngles.x;
        }

        private void LateUpdate()
        {
            Vector2 lookVector = InputHandler.LookInput.Value;
            xRotation += lookVector.y;
            xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);

            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

            // transform.rotation = Quaternion.RotateTowards(
            //     transform.rotation,
            //     targetTransform.rotation * Quaternion.Euler(xRotation, 0, 0), 
            //     rotationSpeed * Time.deltaTime);
        }
    }
}