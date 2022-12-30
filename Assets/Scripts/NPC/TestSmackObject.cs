using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSmackObject : MonoBehaviour
{
    public void GotHit()
    {
        Debug.Log(gameObject.name + " got hit!");
    }
}
