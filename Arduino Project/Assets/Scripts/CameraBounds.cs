using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    private Camera mainCamera;                
    private float screenHalfWidth;            
    private float screenLimitX;               

    private void Awake()
    {
        mainCamera = Camera.main;
        screenHalfWidth = mainCamera.aspect * mainCamera.orthographicSize;
        screenLimitX = screenHalfWidth - 0.5f;
    }

    private void LateUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Vector3 clampedPosition = player.transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenLimitX, screenLimitX);
            player.transform.position = clampedPosition;
        }
    }
}
