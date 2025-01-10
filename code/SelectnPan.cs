using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectnPan : MonoBehaviour
{
    public void UpdateVar(int value)
    {
        if (value != GlobalVariables.selectMode)
        {
            GlobalVariables.selectMode = value;
        } else {
            GlobalVariables.selectMode = 0;
        }
        
    }
}
