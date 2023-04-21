using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;

    bool drag = false;

    private Vector2 touchPos;
    private Vector3 dragPos;

    private Vector2 mousePos;

    [SerializeField]
    Vector2 maxDistance;
    [SerializeField]
    Vector2 minDistance;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0) && !drag)
        {
            if (!IsPointerOverUI(mousePos))
            {
                drag = true; 
                touchPos = mousePos;
            }
        }

        if (Input.GetMouseButton(0) && drag)
        {
            if (!IsPointerOverUI(mousePos))
            {
                dragPos = mainCamera.ScreenToViewportPoint(mousePos - touchPos);
                //마우스가 이동한 방향의 역반향으로 이동한다.
                Vector3 movingV = mainCamera.transform.position - dragPos;
                //카메라 이동제한을 위해 최소치와 최대치를 비교하고 오버될 경우 그 수치를 적용시킨다.
                if (movingV.x > maxDistance.x)
                {
                    movingV.x = maxDistance.x;
                }
                else if(movingV.x < minDistance.x)
                {
                    movingV.x = minDistance.x;
                }

                if (movingV.y > maxDistance.y)
                {
                    movingV.y = maxDistance.y;
                }
                else if (movingV.y < minDistance.y)
                {
                    movingV.y = minDistance.y;
                }
                mainCamera.transform.position = movingV;
            }
        }

        if (Input.GetMouseButtonUp(0) && drag)
        {
            drag = false;
        }
    }

    //화면을 드래그하는 도중 오브젝트를 터치하는 일을 방지하기 위한 체크용 함수
    public bool IsDraging()
    {
        return drag;
    }

    public bool IsPointerOverUI(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = touchPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
