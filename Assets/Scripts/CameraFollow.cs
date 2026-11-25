
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;

    // Desplazamiento vertical de la cámara
    public float verticalOffset = 2f;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y + verticalOffset,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}


