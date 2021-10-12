/**********************************************************************************************************************************************************
 * XRRig_SendEventOnAngle
 * ----------------------
 *
 * 2021-08-25
 * 
 * Checks the angle between a source object and a destination object and sends an event if the source is 'looking at' the target.
 * 
 * Roy Davies, Smart Digital Lab, University of Auckland.
 **********************************************************************************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Public functions
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public interface _XRRig_SendEventOnAngle
{
}
// ----------------------------------------------------------------------------------------------------------------------------------------------------------



// ----------------------------------------------------------------------------------------------------------------------------------------------------------
// Main class
// ----------------------------------------------------------------------------------------------------------------------------------------------------------
public class XRRig_SendEventOnAngle : MonoBehaviour, _XRRig_SendEventOnAngle
{
    public enum Direction { FORWARD, BACK }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Public variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    [Header("____________________________________________________________________________________________________")]
    [Header("Send a chosen event and action, and boolean value, when the two transformations have\nless than a certain angle between them.\nSends true when going inside the angle and false when going outside of the angle.\n____________________________________________________________________________________________________")]
    [Header("INPUTS")]
    [Header("Source Transformation")]
    public Transform source; // Source object to look from
    [Header("Target Transformation")]
    public Transform target; // Target object to look towards

    [Header("____________________________________________________________________________________________________")]
    [Header("SETTINGS")]
    [Header("Angle at which to trigger (in and out)")]
    public float triggerAngle = 20.0f; // The angle at which to trigger the event
    [Header("Looking forwards or backwards")]
    public Direction zDirection = Direction.FORWARD; // Are we looking along the Z axis or back along the Z axis?
    [Header("____________________________________________________________________________________________________")]
    [Header("OUTPUTS")]
    [Header("Event to send")]
    public XRDeviceEventTypes eventToSendOnTrigger; // The event to trigger
    [Header("Action to send")]
    public XRDeviceActions actionToSendOnTrigger; // The action to trigger
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Private variables
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    private XRDeviceEvents eventQueue; // The XR Event Queue
    private bool firstTime = true; // First time running?
    private bool previousLookingAt = false; // Used to make sure we only send an event when something changes.
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Set up the variables ready to go.
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        // Find the event queue
        eventQueue = XRRig.EventQueue;
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------



    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    // Check the angle on each frame, and if there is a change in circumstance, send an event
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // Get the Vector between the source and target
        Vector3 targetDir = source.position - target.position;
        // Determine the angle between the forward or back direction and the above vector.
        float angle = Vector3.Angle(targetDir, (zDirection == Direction.FORWARD)? -source.forward : source.forward);
        // If the angle is less than the trigger angle, then - yup, we're looking at it.
        bool lookingAt = (angle < triggerAngle);

        // Create and send an event if something has changed.
        if ((previousLookingAt != lookingAt) || firstTime)
        {
            // Create an event to send
            XREvent eventToSend = new XREvent();
            eventToSend.eventType = eventToSendOnTrigger;
            eventToSend.eventAction = actionToSendOnTrigger;
            eventToSend.eventBool = lookingAt;
            if (eventQueue != null) eventQueue.Invoke(eventToSend);

            previousLookingAt = lookingAt;
            firstTime = false;
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------------------------------
}

