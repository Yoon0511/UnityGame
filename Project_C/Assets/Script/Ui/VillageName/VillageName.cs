using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageName : Particle
{
    public Text Text;
    public RectTransform RectTransform;

    private void Awake()
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform,false);
        transform.SetAsLastSibling();
        Duration = 5.0f;
    }
    public void Init(string _name)
    {
        Text.text = _name;
        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
        RectTransform.anchoredPosition = new Vector2(0, 339);
    }
}
