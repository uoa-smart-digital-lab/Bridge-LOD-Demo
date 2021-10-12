/******************************************************************************************************************************************************
 * XRRig_RotateToCamera
 * --------------------
 *
 * 2021-08-25
 * 
 * Used by the heads down and heads up GUI - rotates the object it is placed on towards where the camera is positioned.
 * 
 * Roy Davies, Smart Digital Lab, University of Auckland.
 ******************************************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Public functions
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public interface _XRRig_RotateToCamera
{
}
// ----------------------------------------------------------------------------------------------------------------------------------------------------------



// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Main class
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public class XRRig_RotateToCamera : MonoBehaviour, _XRRig_RotateToCamera
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Public variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("____________________________________________________________________________________________________")]
    [Header("Rotate an object towards the direction the camera is pointing\nIntended to be used to rotate objects on the XRRig Head to keep them in view.\n____________________________________________________________________________________________________")]
    [Header("INPUTS")]
    [Header("Camera object rotation")]
    public Transform theCamera; // The GameObject containing the Camera
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Private variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private float initalYRotationOffset; // Used to keep the item in the same position relative to how it is placed in the editor.
    private bool moveOver = false; // Used to ensure movement only occurs if the head is turned past a certain point.
    private float smoothness = 0.05f; // Speed by which it rotates
    private float yVelocity = 0.0f; // Current velocity of rotation
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Set up the variables ready to go.
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        // Get the initial offset
        initalYRotationOffset = transform.eulerAngles.y - theCamera.eulerAngles.y;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Update the Y rotation value depending on how much the camera / head has turned.
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // The angle to move to
        float newAngle = theCamera.eulerAngles.y + initalYRotationOffset;

        // If the head has turned only a tiny bit, don't move it all - allows you to look at stuff in the UI without it moving away from view.
        if (Mathf.Abs(newAngle - transform.eulerAngles.y) < 15)
        {
            moveOver = false;
        }
        else
        {
            moveOver = true;
            smoothness = 0.5f;            
        }

        // Smoothly rotate around
        if (moveOver)
        {
            float floatingAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, newAngle, ref yVelocity, smoothness);
            transform.rotation = Quaternion.Euler(0, floatingAngle, 0);
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}