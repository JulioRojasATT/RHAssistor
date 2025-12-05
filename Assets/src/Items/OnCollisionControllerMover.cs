using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnCollisionControllerMover : MonoBehaviour {

    [SerializeField] private float speed;

    private List<CharacterController> controllersOnStairs = new List<CharacterController>();

    //[SerializeField] private OverrideCharacterController stepsInputListener;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller) && !controllersOnStairs.Contains(controller)) {
            controllersOnStairs.Add(controller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller) && controllersOnStairs.Contains(controller))
        {
            controllersOnStairs.Remove(controller);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.TryGetComponent(out CharacterController controller)) {
            //MoveController(controller);
        }
    }

    private void LateUpdate() {
        controllersOnStairs.ForEach(x => x.Move(transform.forward * speed * Time.deltaTime));
    }
}
