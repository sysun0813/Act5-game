using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;

    

    float cameraHalfHeight, cameraHalfWidth;

    [Header("낮을 수록 부드럽게 따라감")]
    public float smoothSpeed = 3f;

    public float limitMinX, limitMaxX, limitMinY, limitMaxY;

    StageManager stageManager;

    private void Start()
    {
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = Camera.main.aspect * Camera.main.orthographicSize;
    }

    public void CameraLateUpdate(List<Main_Character> playerCharacters)
    {
        if (playerCharacters.Count > 0)
        {
            targetTransform = playerCharacters[0].transform;
        }
        if (targetTransform != null)
        {
            Vector3 desiredPosition = new Vector3(
                Mathf.Clamp(targetTransform.position.x, limitMinX + cameraHalfWidth, limitMaxX - cameraHalfWidth),
                transform.position.y,
                -10f);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(limitMinX, limitMinY), new Vector3(limitMaxX, limitMinY));
        Gizmos.DrawLine(new Vector2(limitMinX, limitMaxY), new Vector3(limitMaxX, limitMaxY));
        Gizmos.DrawLine(new Vector2(limitMinX, limitMinY), new Vector3(limitMinX, limitMaxY));
        Gizmos.DrawLine(new Vector2(limitMaxX, limitMinY), new Vector3(limitMaxX, limitMaxY));
    }
}
