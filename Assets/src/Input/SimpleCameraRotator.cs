using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleCameraRotator : MonoBehaviour {
     
        public float yaw;
        public float pitch;
        public float roll;
        
        public float targetYaw;
        public float targetPitch;
        public float targetRoll;
     
        public float deltaX, deltaY;

    [SerializeField] private InputActionReference rotateReference;
        
        [Header("Rotation Settings")]
        [Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation.")]
        public AnimationCurve mouseSensitivityCurve = new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

        [Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."), Range(0.001f, 1f)]
        public float rotationLerpTime = 0.01f;

    private void Awake()
    {
        rotateReference.action.Enable();
        rotateReference.action.performed += OnRotatePerformed;
        rotateReference.action.canceled += OnRotateCancelled;
    }

    private void OnRotateCancelled(InputAction.CallbackContext obj) {
        deltaX = 0;
        deltaY = 0;
    }

    private void OnRotatePerformed(InputAction.CallbackContext obj)
    {
        Vector2 lookInput = obj.ReadValue<Vector2>();
        deltaX = lookInput.x;
        deltaY = -lookInput.y;
    }

    void Update() {
            Vector2 inputRotationVector = new Vector2(deltaX, deltaY);
            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(inputRotationVector.magnitude);
            
            targetYaw += deltaX * mouseSensitivityFactor;
            targetPitch += deltaY * mouseSensitivityFactor;
            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
            LerpTowards(targetYaw, targetPitch, targetRoll, rotationLerpPct);

            UpdateTransform();
        }
        
        public void ResetRotation() {
            yaw = 0;
            pitch = 0;
            roll = 0;
            targetYaw = 0;
            targetPitch = 0;
            targetRoll = 0;
            transform.localEulerAngles = Vector3.zero;
        }

        public void LerpTowards(float targetYaw, float targetPitch, float targetRoll, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, targetYaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, targetPitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, this.targetRoll, rotationLerpPct);
        }

        public void UpdateTransform()
        {
            transform.localEulerAngles = new Vector3(pitch, yaw, roll);
        }
}
