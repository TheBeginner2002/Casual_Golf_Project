using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;    //toc do quay
    [SerializeField] private float scrollSpeed = 1f;
    private new Camera camera;

    public static CameraRotation instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        camera = Camera.main;
    }
    
    public void RotateCamera(float xAxisRotation)           
    {
        transform.Rotate(Vector3.down, -xAxisRotation * rotationSpeed); //quay camera
    }
    
    public void ZoomCamera(float scrollPos)
    {
        camera.fieldOfView -= scrollPos * scrollSpeed * 0.1f;
    }
}
