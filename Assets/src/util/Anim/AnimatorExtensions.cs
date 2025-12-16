using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorExtensions : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    public void SetBoolToTrue(string boolName)
    {
        m_Animator.SetBool(boolName, true);
    }

    public void SetBoolToFalse(string boolName)
    {
        m_Animator.SetBool(boolName, false);
    }
}
