using UnityEngine;

public class ObjectRotator : MonoBehaviour {

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float rotationFactor;

    [SerializeField] private Transform objectToRotate;

    [SerializeField] private bool canRotateUp = true;

    [SerializeField] private bool canRotateRight = true;

    private void LateUpdate(){
        if(objectToRotate != null) {
            transform.Rotate(Vector3.up, rotationSpeed * rotationFactor * Time.deltaTime);
        }
    }

    public void SetRotationFactor(float newFactor){
        rotationFactor = newFactor;
    }

    public void RotateUp(float amount) {
        if (!canRotateUp) return;
        transform.Rotate(Vector3.up, rotationSpeed * amount * Time.deltaTime);
    }

    public void RotateRight(float amount){
        if (!canRotateRight) return;
        transform.Rotate(Vector3.right, rotationSpeed * amount * Time.deltaTime);
    }

    public void ResetRotation()=>transform.rotation = Quaternion.identity;
    public void ResetRotationLocal() => transform.localRotation = Quaternion.identity;
}
