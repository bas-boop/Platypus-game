using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbillity : MonoBehaviour
{
    private Vector2 _mouseWorldPosition;
    
    public bool isDashing;

    public void ActivateDash()
    {
        isDashing = true;

        Debug.Log(_mouseWorldPosition);
        transform.position = _mouseWorldPosition;
    }

    public void SetMousePos(Vector2 mousePos)
    {
        _mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Mathf.Abs(Camera.main.transform.position.z)));

        isDashing = false;
    }
}
