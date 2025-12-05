using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformExtensions : MonoBehaviour
{
    public void ResetLocalPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    public void ResetLocalRotation()
    {
        transform.localRotation = Quaternion.identity;
    }

    public void ResetTransformLocal()
    {
        ResetLocalPosition();
        ResetLocalRotation();
    }

    public void TeleportTo(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;   
    }

    public void TeleportCharacterController(Transform target)
    {
        StartCoroutine(TeleportCharacterControllerCor(target));
    }

    public void ResetParent()
    {
        transform.parent = null;
    }

    public IEnumerator TeleportCharacterControllerCor(Transform target)
    {        
        GetComponent<CharacterController>().enabled = false;
        yield return new WaitForFixedUpdate();
        transform.position = target.position;
        transform.rotation = target.rotation;
        GetComponent<CharacterController>().enabled = true;
    }
}
