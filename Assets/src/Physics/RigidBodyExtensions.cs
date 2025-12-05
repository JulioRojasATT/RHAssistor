using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidBodyExtensions : MonoBehaviour
{
    private Rigidbody m_RigidBody;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    public void Stop() {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
        m_RigidBody.isKinematic = true;
    }

    public void StopNonKinematic()
    {
        m_RigidBody.velocity = Vector3.zero;
        m_RigidBody.angularVelocity = Vector3.zero;
    }
}
