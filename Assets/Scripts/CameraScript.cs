using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform Camera;
    [SerializeField] Transform McPos;


    private void LateUpdate()
    {
        if (McPos != null)
        {
            Vector3 Pos = new(McPos.position.x, McPos.position.y, -10);
            Camera.position = Vector3.Lerp(Camera.position, Pos, 0.5f);
        }
    }
}