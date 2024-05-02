using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;    //toc do quay
    [SerializeField] private float scrollSpeed = 10f;
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
    
    public void ZoomCamera(Vector2 scrollPos)
    {
        camera.fieldOfView -= scrollPos.y * scrollSpeed;
    }
}
