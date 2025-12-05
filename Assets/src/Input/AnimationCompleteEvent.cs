using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AnimationCompleteEvent : MonoBehaviour
{
    public UnityEvent onAnimationComplete;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on " + gameObject.name);
        }
    }

    public void WaitForAnimationCoroutine(string stateName)
    {
        StartCoroutine(WaitForAnimation(stateName));
    }

    private IEnumerator WaitForAnimation(string stateName)
    {
        // Wait until the animator is actually in the target state
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }
        Debug.Log("Animation started");

        // Wait until the animation finishes
        while (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }
        Debug.Log("Animation completed");

        onAnimationComplete?.Invoke();
    }
}
