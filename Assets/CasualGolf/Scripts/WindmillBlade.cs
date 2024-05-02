using UnityEngine;

public class WindmillBlade : MonoBehaviour
{
    [SerializeField] private GameObject windmill;
    [SerializeField] private float rotationSpeed = 60f;

    private void Update()
    {
        windmill.transform.Rotate(Vector3.back,rotationSpeed * Time.deltaTime);
    }
}
