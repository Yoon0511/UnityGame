using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageImageText : MonoBehaviour
{
    public GameObject PARENT;
    public GameObject DAMAGEIMG;
    public GameObject DAMAGENUMBER;
    public Sprite[] DamageImg;
    Canvas Canvas;
    private void Start()
    {
        for (int i =0;i<10;++i)
        {
            DamageImg[i] = Shared.GameMgr.GetSpriteAtlas("Damage", i.ToString());
        }
        Canvas = Shared.GameMgr.CANVAS;
    }

    public void CreateDamageImage(int _damage,Vector3 _pos)
    {
        string damage = _damage.ToString();

        Vector3 pos = Camera.main.WorldToScreenPoint(_pos);

        GameObject obj = Instantiate(DAMAGEIMG);
        obj.GetComponent<RectTransform>().anchoredPosition = pos;
        obj.transform.SetParent(Canvas.transform);

        for (int i = 0;i<damage.Length;++i)
        {
            Image img = Instantiate(DAMAGENUMBER).GetComponent<Image>();
            img.transform.SetParent(obj.transform);
            int SpriteIndex = damage[i] - '0';
            img.sprite = DamageImg[SpriteIndex];
        }
    }
}
