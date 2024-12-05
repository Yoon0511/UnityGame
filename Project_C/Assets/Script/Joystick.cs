using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    // Start is called before the first frame update
    public GameObject BG;
    public GameObject STICK;
    public GameObject TARGET;

    [SerializeField]
    float MaxRange = 100f;
    Vector2 InputPos;
    Vector3 Pos;

    float dist = 0.0f;

    private RectTransform stick;
    private RectTransform bg;
    private Player player;

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        InputPos = eventData.position - (Vector2)bg.position;

        Pos = InputPos.magnitude < MaxRange ? InputPos : InputPos.normalized * MaxRange;
        stick.anchoredPosition = Pos;

        dist = Pos.magnitude / 100.0f;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stick.anchoredPosition = Vector2.zero;
    }

    void Start()
    {
        stick = STICK.GetComponent<RectTransform>();
        bg = BG.GetComponent<RectTransform>();
        player = TARGET.GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        
    }
}
