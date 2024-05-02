using UnityEngine;

public class CameraFollow : MonoBehaviour //Camera theo bong
{
    public static CameraFollow instance;

    [SerializeField] private ActiveVectors activeVectors;

    private GameObject followTarget;
    private Vector3 offset;
    private Vector3 changePos;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void SetTarget(GameObject target) //Ham set target bong
    {
        followTarget = target;
        offset = followTarget.transform.position - transform.position;
        changePos = transform.position;
    }
    
    private void LateUpdate()// Di chuyen Camera
    {
        if (followTarget)
        {
            Vector2 scrollDelta = Input.mouseScrollDelta;
            if (activeVectors.x)
            {
                changePos.x = followTarget.transform.position.x - offset.x;
            }
            if (activeVectors.y)
            {
                changePos.y = followTarget.transform.position.y - offset.y;
            }
            if (activeVectors.z)
            {
                changePos.z = followTarget.transform.position.z - offset.z;
            }

            transform.position = changePos + new Vector3(scrollDelta.y, scrollDelta.y, scrollDelta.y);
        }
    }
    
}

[System.Serializable]
public class ActiveVectors
{
    public bool x, y, z;
}
