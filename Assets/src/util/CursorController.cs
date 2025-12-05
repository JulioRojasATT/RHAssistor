using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    public void SetCursorState(CursorLockMode cursorLockMode) {
        Cursor.lockState = cursorLockMode;
    }

    public void ConfineCursor()
    {
        SetCursorState(CursorLockMode.Confined);
    }

    public void FreeCursor()
    {
        SetCursorState(CursorLockMode.None);
    }

    public void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        } else if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
