using System;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstCamera;
    public Camera secondCamera;
    public Camera thirdCamera;
    public Camera fourthCamera;

    private bool isFirstCameraActive = true;
    int indexCamera = 0;

    void Start()
    {
        // Ensure one camera is active at the start
        firstCamera.enabled = true;
        secondCamera.enabled = false;
        thirdCamera.enabled = false;
        fourthCamera.enabled = false;
    }

    private void turningOff()
    {
        firstCamera.enabled = false;
        secondCamera.enabled = false;
        thirdCamera.enabled = false;
        fourthCamera.enabled = false;
    }

    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(++indexCamera > 3){
                indexCamera = 0;
            }
            turningOff();

            if(indexCamera == 0){
                firstCamera.enabled = true;
            }else if(indexCamera == 1){
                secondCamera.enabled = true;
            }else if(indexCamera == 2){
                thirdCamera.enabled = true;
            }else if(indexCamera == 3){
                fourthCamera.enabled = true;
            }

            // isFirstCameraActive = !isFirstCameraActive;
            
            // // Enable/disable cameras based on the toggle state
            // firstCamera.enabled = isFirstCameraActive;
            // secondCamera.enabled = !isFirstCameraActive;
        }
    }
}
