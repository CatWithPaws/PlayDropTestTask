using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 cameraOffset;

    private void Start()
    {
        cameraOffset = transform.position - player.transform.position;
    }

    void Update()
    {
        Camera.main.transform.position = player.transform.position + cameraOffset;
    }
}
