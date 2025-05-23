using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageName : Particle
{
    public Text Text;

    private void Awake()
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform,false);
        transform.SetAsLastSibling();
        Duration = 3.0f;
    }
    public void Init(string _name)
    {
        Text.text = _name;
    }
}
