/**********************************************************************************************************************************************************
 * XRUX_MaterialColor
 * ------------------
 *
 * 2021-09-01
 *
 * Changes the material color of the object it is on
 *
 * Roy Davies, Smart Digital Lab, University of Auckland.
 **********************************************************************************************************************************************************/
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Public functions
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public interface IXRUX_MaterialColor
{
    void InputR(XRData newRed);
    void InputG(XRData newGreen);
    void InputB(XRData newBlue);
    void InputA(XRData newAlpha);
}
// ----------------------------------------------------------------------------------------------------------------------------------------------------------



// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Main class
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
[AddComponentMenu("OpenXR UX/Tools/XRUX Material Color")]
public class XRUX_MaterialColor : MonoBehaviour, IXRUX_MaterialColor
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Public variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------

    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Private variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private float storedRed, storedGreen, storedBlue, storedAlpha = 255.0f;
    private Renderer objectToChange;
    // ------------------------------------------------------------------------------------------------------------------------------------------------------


        
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        objectToChange = gameObject.GetComponent<Renderer>();
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Input values of R, G, B and A
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InputR (XRData newRed)
    {
        storedRed = newRed.ToFloat() % 255.0f;
        objectToChange.material.color = new Color(storedRed / 255.0f, storedGreen / 255.0f, storedBlue / 255.0f, storedAlpha / 255.0f);
    }
    public void InputG (XRData newGreen)
    {
        storedGreen = newGreen.ToFloat() % 255.0f;
        objectToChange.material.color = new Color(storedRed / 255.0f, storedGreen / 255.0f, storedBlue / 255.0f, storedAlpha / 255.0f);
    }
    public void InputB (XRData newBlue)
    {
        storedBlue = newBlue.ToFloat() % 255.0f;
        objectToChange.material.color = new Color(storedRed / 255.0f, storedGreen / 255.0f, storedBlue / 255.0f, storedAlpha / 255.0f);
    }
    public void InputA (XRData newAlpha)
    {
        storedAlpha = newAlpha.ToFloat() % 255.0f;
        objectToChange.material.color = new Color(storedRed / 255.0f, storedGreen / 255.0f, storedBlue / 255.0f, storedAlpha / 255.0f);
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}
