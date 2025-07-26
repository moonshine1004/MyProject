using UnityEngine;
using UnityEngine.EventSystems;

public class CardUI : MonoBehaviour
{
    private RectTransform rectTransform; // 카드 위치 제어용
    private Canvas canvas;               // 마우스 좌표 변환에 필요
    private bool isDragging = false;     // 드래그 상태 플래그
    private Vector2 offset;              // 클릭 위치 보정용

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    void Update()
    {
        // 1. 마우스 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;

            // 마우스가 카드 위에 있는지 수동 체크
            if (IsMouseOverCard(mousePosition))
            {
                isDragging = true;

                // 클릭 지점 보정 계산
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rectTransform,
                    mousePosition,
                    canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                    out offset
                );
            }
        }

        // 2. 드래그 중이면 마우스 따라 이동
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform,
                Input.mousePosition,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                out localPoint))
            {
                rectTransform.anchoredPosition = localPoint - offset;
            }
        }

        // 3. 마우스 버튼을 뗐을 때 드래그 종료
        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    // 카드 영역에 마우스가 있는지 직접 체크하는 함수
    private bool IsMouseOverCard(Vector2 screenPosition)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(
            rectTransform,
            screenPosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera
        );
    }
}
