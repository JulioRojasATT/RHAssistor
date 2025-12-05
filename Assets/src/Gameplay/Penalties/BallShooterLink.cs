using UnityEngine;
using UnityEngine.Events;

public class BallShooterLink : MonoBehaviour {
    
    public UnityEvent onKick;

    public void Kick(){
        onKick.Invoke();
    }
}
