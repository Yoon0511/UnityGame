using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    // Start is called before the first frame update
    public GameObject BG;
    public GameObject STICK;

    [SerializeField]
    float MaxRange = 100f;
    Vector2 InputPos;
    Vector3 Pos;
    float Dist = 0.0f;
    public bool IsDrag = false;

    private RectTransform stick;
    private RectTransform bg;
    public Player Player;

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        IsDrag = true;

        InputPos = eventData.position - (Vector2)bg.position;

        Pos = InputPos.magnitude < MaxRange ? InputPos : InputPos.normalized * MaxRange;
        stick.anchoredPosition = Pos;

        Dist = Pos.magnitude / 100.0f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;

        stick.anchoredPosition = Vector2.zero;
        Player.JoystickMove(InputPos.normalized, 0.0f,IsDrag);
    }

    void Start()
    {
        stick = STICK.GetComponent<RectTransform>();
        bg = BG.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (IsDrag == false || Player == null)
            return;

        MoveToPlayer();
    }

    bool GetIsDrag()
    {
        if (stick.anchoredPosition.Equals(Vector2.zero))
        {
            IsDrag = false;
        }
        else
        {
            IsDrag = true;
        }
        return IsDrag;
    }
    void MoveToPlayer()
    {
        Player.JoystickMove(InputPos.normalized, Dist, IsDrag);
    }

    public void SetTarget(Player _player)
    {
        Player = _player;
    }
}