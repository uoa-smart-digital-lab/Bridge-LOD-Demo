/**********************************************************************************************************************************************************
 * XRData_RGBtoHex
 * ---------------
 *
 * 2021-09-01
 *
 * A connector that takes 3 RGB values between 0 and 255 and returns a string in hexadecimal format
 *
 * Roy Davies, Smart Digital Lab, University of Auckland.
 **********************************************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Public functions
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public interface IXRData_RGBtoHex
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
[AddComponentMenu("OpenXR UX/Connectors/XRData RGB To Hex")]
public class XRData_RGBtoHex : MonoBehaviour, IXRData_RGBtoHex
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Public variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    public UnityXRDataEvent onChange;
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Private variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private float storedRed, storedGreen, storedBlue, storedAlpha = 255.0f;
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Input values of R, G, B and A and add to the current stored value, sending the response down the event queue
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void InputR (XRData newRed)
    {
        storedRed = newRed.ToFloat() % 255.0f;
        if (onChange != null) onChange.Invoke(new XRData(currentHexColor()));        
    }
    public void InputG (XRData newGreen)
    {
        storedGreen = newGreen.ToFloat() % 255.0f;
        if (onChange != null) onChange.Invoke(new XRData(currentHexColor()));        
    }
    public void InputB (XRData newBlue)
    {
        storedBlue = newBlue.ToFloat() % 255.0f;
        if (onChange != null) onChange.Invoke(new XRData(currentHexColor()));        
    }
    public void InputA (XRData newAlpha)
    {
        storedAlpha = newAlpha.ToFloat() % 255.0f;
        if (onChange != null) onChange.Invoke(new XRData(currentHexColor()));        
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Create a string of the current color in hexadecimal
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private string currentHexColor()
    {
        Color newColor = new Color(storedRed / 255.0f, storedGreen / 255.0f, storedBlue / 255.0f, storedAlpha / 255.0f);
        return(ColorUtility.ToHtmlStringRGBA(newColor));
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}
