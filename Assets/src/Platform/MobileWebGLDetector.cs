using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class MobileWebGLDetector : MonoBehaviour
{
    [SerializeField] private BoolScriptableValue isWebGLMobile;
    
    [SerializeField] private BoolScriptableValue isAndroid;

    [SerializeField] private UnityEvent onWebGLMobileDetected;

    [SerializeField] private UnityEvent onWebGLMobileNotDetected;

    [Header("Force fixed value")]
    [SerializeField] private bool forceFixedValue;
    
    [SerializeField] private bool fixedValue;

    #if UNITY_WEBGL
    [DllImport("__Internal")]
        private static extern bool CheckMobile();
    
        [DllImport("__Internal")]
        private static extern bool ExtendedCheckMobile();
    
        [DllImport("__Internal")]
        private static extern bool IsIOS();
    #endif
    

    private void Start()
    {
        if(isWebGLMobile.Value)
        {
            onWebGLMobileDetected?.Invoke();
            return;
        }
        onWebGLMobileNotDetected?.Invoke();
    }

    public void SetValueOfWebGlMobile() {
        if (forceFixedValue) {
            isWebGLMobile.SetValue(fixedValue);
            return;
        }        
        isWebGLMobile.SetValue(IsMobileWebGL());
        isAndroid.SetValue(!IsOnIOSDevice());
    }

    public bool IsOnIOSDevice() {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return IsIOS();
        #endif
        return false;
    }

    public bool IsMobileWebGL()
    {
        #if !UNITY_EDITOR && UNITY_WEBGL
            return CheckMobile() || ExtendedCheckMobile();
        #endif
        return false;
    }
}
