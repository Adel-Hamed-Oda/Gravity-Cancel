using UnityEngine;

public class CameraPositionLimiter : MonoBehaviour
{
    [SerializeField] private float playerHeightOffset = 4f;

    private void LateUpdate()
    {
        // Get the current position of the camera
        Vector3 currentPosition = transform.position;
        // Clamp the camera's position within the defined boundaries
        float clampedX = currentPosition.x;
        float clampedY = Mathf.Clamp(currentPosition.y, GetMinCameraY(), GetMaxCameraY());
        float clampedZ = currentPosition.z;
        // Update the camera's position with the clamped values
        transform.position = new Vector3(clampedX, clampedY, clampedZ);
    }

    private float GetMinCameraY()
    {
        if (PlayerManager.Instance == null) return 0f;
        return PlayerManager.Instance.minPlayerHeight + playerHeightOffset;
    }
    private float GetMaxCameraY()
    {
        if (PlayerManager.Instance == null) return 0f;
        return PlayerManager.Instance.maxPlayerHeight - playerHeightOffset;
    }
}