using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// Convenience class for instantiation of prefabs
/// </summary>
public static class PrefabFactory
{   
    private static UnityEngine.Object _letterDisplay;
    public static GameObject LetterDisplay
    {
        get
        {
            if (_letterDisplay == null)
                _letterDisplay = Resources.Load("LetterDisplay");

            return (GameObject)GameObject.Instantiate(_letterDisplay);
        }
    }

    private static UnityEngine.Object _falingLetter;
    public static GameObject FallingLetter
    {
        get
        {
            if (_falingLetter == null)
                _falingLetter = Resources.Load("FallingLetter");

            return (GameObject)GameObject.Instantiate(_falingLetter);
        }
    }
}

