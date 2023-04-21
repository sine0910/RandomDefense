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
                //���콺�� �̵��� ������ ���������� �̵��Ѵ�.
                Vector3 movingV = mainCamera.transform.position - dragPos;
                //ī�޶� �̵������� ���� �ּ�ġ�� �ִ�ġ�� ���ϰ� ������ ��� �� ��ġ�� �����Ų��.
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

    //ȭ���� �巡���ϴ� ���� ������Ʈ�� ��ġ�ϴ� ���� �����ϱ� ���� üũ�� �Լ�
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
