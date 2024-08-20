using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Lever2D : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    [SerializeField] bool vertical;
    public RectTransform leverBackground;  // Background lever
    private RectTransform leverHandle;     // Handle lever
    public float moveRange = 100f;         // Range pergerakan handle
    public float snapThreshold = 25f;      // Threshold untuk snapping posisi

    // Events ketika tuas mencapai posisi tertentu
    public UnityEvent OnUpperPositionReached;
    public UnityEvent OnCenterPositionReached;
    public UnityEvent OnBottomPositionReached;

    private Vector2 upperPosition;
    private Vector2 centerPosition;
    private Vector2 bottomPosition;

    void Start()
    {
        leverHandle = GetComponent<RectTransform>();

        // Tentukan tiga posisi untuk snapping
        if (vertical)
        {
            upperPosition = new Vector2(0, moveRange);
            centerPosition = new Vector2(0, 0);
            bottomPosition = new Vector2(0, -moveRange);
        }
        else
        {
            upperPosition = new Vector2(moveRange, 0);
            centerPosition = new Vector2(0, 0);
            bottomPosition = new Vector2(-moveRange, 0);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(leverBackground, eventData.position, eventData.pressEventCamera, out localPoint);

        if (vertical)
        {
            // Batasi pergerakan handle pada sumbu Y
            localPoint.y = Mathf.Clamp(localPoint.y, -moveRange, moveRange);
            localPoint.x = 0; // Batasi agar hanya bergerak vertikal
        }
        else
        {
            // Batasi pergerakan handle pada sumbu X
            localPoint.x = Mathf.Clamp(localPoint.x, -moveRange, moveRange);
            localPoint.y = 0; // Batasi agar hanya bergerak horizontal
        }

        // Atur posisi handle
        leverHandle.anchoredPosition = localPoint;

        // Cek posisi lever
        if (vertical)
        {
            CheckLeverPosition(localPoint.y);
        }
        else
        {
            CheckLeverPosition(localPoint.x);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestPosition();
    }

    void CheckLeverPosition(float position)
    {
        if (position <= (vertical ? bottomPosition.y : bottomPosition.x) + snapThreshold)
        {
            OnBottomPositionReached?.Invoke(); // Panggil event saat mencapai posisi bawah
        }
        else if (position >= (vertical ? upperPosition.y : upperPosition.x) - snapThreshold)
        {
            OnUpperPositionReached?.Invoke(); // Panggil event saat mencapai posisi atas
        }
        else if (Mathf.Abs(position) < snapThreshold)
        {
            OnCenterPositionReached?.Invoke(); // Panggil event saat berada di tengah
        }
    }

    void SnapToClosestPosition()
    {
        float currentPos = vertical ? leverHandle.anchoredPosition.y : leverHandle.anchoredPosition.x;

        // Hitung jarak ke setiap posisi
        float distanceToUpper = Mathf.Abs(currentPos - (vertical ? upperPosition.y : upperPosition.x));
        float distanceToCenter = Mathf.Abs(currentPos - (vertical ? centerPosition.y : centerPosition.x));
        float distanceToBottom = Mathf.Abs(currentPos - (vertical ? bottomPosition.y : bottomPosition.x));

        // Tentukan posisi terdekat
        if (distanceToUpper < distanceToCenter && distanceToUpper < distanceToBottom)
        {
            leverHandle.anchoredPosition = vertical ? new Vector2(0, upperPosition.y) : new Vector2(upperPosition.x, 0);
            OnUpperPositionReached?.Invoke();
        }
        else if (distanceToCenter < distanceToUpper && distanceToCenter < distanceToBottom)
        {
            leverHandle.anchoredPosition = vertical ? new Vector2(0, centerPosition.y) : new Vector2(centerPosition.x, 0);
            OnCenterPositionReached?.Invoke();
        }
        else
        {
            leverHandle.anchoredPosition = vertical ? new Vector2(0, bottomPosition.y) : new Vector2(bottomPosition.x, 0);
            OnBottomPositionReached?.Invoke();
        }
    }
    public void MoveToBottomPosition()
    {
        leverHandle.anchoredPosition = vertical ? bottomPosition : new Vector2(bottomPosition.x, 0);
        OnBottomPositionReached?.Invoke();  // Panggil event saat mencapai posisi bawah
    }
}
