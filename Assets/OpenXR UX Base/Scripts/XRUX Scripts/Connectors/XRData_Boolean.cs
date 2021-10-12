/**********************************************************************************************************************************************************
 * XRData_Boolean
 * --------------
 *
 * 2021-09-05
 *
 * Sends a Boolean when told to.
 *
 * Roy Davies, Smart Digital Lab, University of Auckland.
 **********************************************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Public functions
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public interface _XRData_Boolean
{
    void Go();
}
// ----------------------------------------------------------------------------------------------------------------------------------------------------------



// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Main class
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
[AddComponentMenu("OpenXR UX/Connectors/XRData Boolean")]
public class XRData_Boolean : MonoBehaviour, _XRData_Boolean
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Public variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("____________________________________________________________________________________________________")]
    [Header("Generate a boolean variable for XRData.\n____________________________________________________________________________________________________")]
    [Header("INPUTS\n\n - Go() - Trigger to activate function.")]

    [Header("____________________________________________________________________________________________________")]
    [Header("SETTINGS")]
    [Header("The Boolean variable.")]
    public bool value;
    public bool sendOnStart = false;

    [Header("____________________________________________________________________________________________________")]
    [Header("OUTPUTS")]
    public UnityXRDataEvent onChange;
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Private variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private bool firstTime = true;
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        if (firstTime)
        {
            firstTime = false;
            if (sendOnStart) Go();
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Send a Boolean
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Go()
    {
        if (onChange != null) onChange.Invoke(new XRData(value));
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}