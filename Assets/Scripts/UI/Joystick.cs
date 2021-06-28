using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform stick;
    [SerializeField] private RectTransform background;

    public Vector2 InputDirecion { get; private set; }
    private Vector2 localPoint;
    private float stickRadius;

    private RectTransform joystickRect;
    private void Start()
    {
        joystickRect = GetComponent<RectTransform>();
        stickRadius = (background.sizeDelta / 3).magnitude;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        JoystickInput(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        JoystickInput(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystickInput();
    }
    private void JoystickInput(PointerEventData eventData)
    {
        InputDirecion = (eventData.position - (Vector2)background.position).normalized;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickRect,
            eventData.position, eventData.pressEventCamera, out localPoint))
        {
            Vector2 pos = Vector2.ClampMagnitude(localPoint, stickRadius);
            stick.localPosition = pos;
        }
    }
    private void ResetJoystickInput()
    {
        InputDirecion = Vector2.zero;
        stick.localPosition = Vector2.zero;
    }
}
