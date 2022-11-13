using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static Controls _controls;
    public static Controls controls
    {
        get
        {
            if(_controls == null)
                _controls = new();
            return _controls;

        }
    }

}
