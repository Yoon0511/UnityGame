using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialMark : Particle
{
    [SerializeField]
    Image Image;
    [SerializeField]
    RectTransform RectTransform;

    Transform Owner;
    private void Awake()
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
        Duration = 1.0f;
    }

    public void Init(string _markname,float _duration,Transform _owner)
    {        
        Owner = _owner;
        Duration = _duration;
        Image.sprite = Shared.GameMgr.GetSpriteAtlas("SpecialMark", _markname);
    }

    public void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(Owner.position);
        RectTransform.position = pos;
    }
}
