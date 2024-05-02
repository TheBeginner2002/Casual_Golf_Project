using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float distanceBetweenBallAndMouseClickLimit = 1.5f; //gioi han vi tri chuot den bong

    private float distanceBetweenBallAndMouseClick; //kiem tr vi tri chuot
    private bool canRotate = false;
    
    void Update()
    {
        if (GameManager.instance.gameStatus != GameStatus.Playing) return;

        if (Input.GetMouseButtonDown(0) && !canRotate)
        {
            GetDistance();//khoang cach giua bong va chuot
            canRotate = true;
            
            if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
            {
                BallControl.instance.MouseDownMethod();
            }
        }

        if (canRotate)
        {
            if (Input.GetMouseButton(0))
            {
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseNormalMethod();
                }
                else
                {
                    CameraRotation.instance.RotateCamera(Input.GetAxis("Mouse X"));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                canRotate = false;
                if (distanceBetweenBallAndMouseClick <= distanceBetweenBallAndMouseClickLimit)
                {
                    BallControl.instance.MouseUpMethod();
                }
            }
        }
    }

    private void LateUpdate()
    {
        CameraRotation.instance.ZoomCamera(Input.mouseScrollDelta);
    }

    void GetDistance()// Tinh khoang cach khi click chuot va qua bong
    {
        if (BallControl.instance)
        {
            var plane = new Plane(Camera.main.transform.forward, BallControl.instance.transform.position);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float dist;
            if (plane.Raycast(ray, out dist))
            {
                var v3Pos = ray.GetPoint(dist); //tim ra vi tri cua con chuot
                distanceBetweenBallAndMouseClick = Vector3.Distance(v3Pos, BallControl.instance.transform.position);
            }
        } 
    }

}
