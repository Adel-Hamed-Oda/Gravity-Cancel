using Unity.Cinemachine;
using UnityEngine;

public class PlayerCameraController : Manager<PlayerCameraController>
{
    [SerializeField] private CinemachineCamera playerCamera;

    // Using LateUpdate is best practice for cameras so they move AFTER the player has moved
    private void LateUpdate()
    {
        // Check if the TrackingTarget (Follow) is null
        if (playerCamera.Target.TrackingTarget == null)
        {
            PlayerController playerController = FindFirstObjectByType<PlayerController>();
            if (playerController != null)
            {
                // 1. Extract the Target struct
                CameraTarget cameraTargets = playerCamera.Target;

                // 2. Set the Tracking Target so the camera follows the player's position
                cameraTargets.TrackingTarget = playerController.transform;

                // (Optional) Set the LookAt Target if you also want it to rotate to track the player
                cameraTargets.LookAtTarget = playerController.transform;

                // 3. Assign the modified struct back to the camera
                playerCamera.Target = cameraTargets;
            }
        }
    }
}