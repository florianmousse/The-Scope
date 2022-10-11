using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTracking : MonoBehaviour
{
    private OVRCameraRig cam1;
    private Vector3 p0;

    private Quaternion q0;
    private bool disablepositionnal, disablerotationnal;

    private Vector3 headPosition, origPos;

    // Start is called before the first frame update
    void Start()
    {
        cam1 = GameObject.FindObjectsOfType<OVRCameraRig>()[0];
        disablepositionnal = false;
        disablerotationnal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            disablepositionnal = !disablepositionnal;
            p0 = cam1.centerEyeAnchor.position;
        }

        // A button, positionnal tracking
        if (disablepositionnal) {
            Vector3 deltaT = p0 - cam1.centerEyeAnchor.position;
            cam1.centerEyeAnchor.parent.position += deltaT;
        }

        // X button, rotationnal tracking
        if (OVRInput.GetDown(OVRInput.Button.Three)) {
            disablerotationnal = !disablerotationnal;
        }

        // A button, positionnal tracking
        if (disablerotationnal) {
            headPosition = cam1.centerEyeAnchor.position;
            cam1.centerEyeAnchor.parent.localRotation *= q0 * Quaternion.Inverse(cam1.centerEyeAnchor.localRotation);
            Vector3 deltaQ = headPosition - cam1.centerEyeAnchor.position;
            cam1.centerEyeAnchor.parent.position += deltaQ;
        }

        q0 = cam1.centerEyeAnchor.localRotation;
        origPos = cam1.centerEyeAnchor.position;
    }
}
