using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody;
    public float mouseSpeed = 80f;
    float mouseX;
    float mouseY;
    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime ;

        // rotate the whole body of the player in the direction
        playerBody.Rotate(Vector3.up * mouseX);

        // if decrese instead of increase, the rotation will be flipped
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        // rotate only the camera, up and down, not the player
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
    }
}
