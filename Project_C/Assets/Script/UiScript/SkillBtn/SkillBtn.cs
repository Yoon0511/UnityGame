using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillBtn: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int SkillIndex;
    [SerializeField]
    Image Image;
    [SerializeField]
    Text CoolTimeText;
    Color Originalcolor = new Color(255, 255, 255,255);

    Player Player;
    Canvas Canvas;
    public GameObject DragAndDropSkill;
    GameObject Dragobj;
    Skill Skill;

    [SerializeField]
    Skill FixSkil;
    private void Start()
    {
        Player = Shared.GameMgr.PLAYER;
        Canvas = Shared.GameMgr.CANVAS;

        if(FixSkil != null)
        {
            StartCoroutine(ISkillInput());
        }
    }
    IEnumerator ISkillInput()
    {
        yield return new WaitForSeconds(0.1f);
        InputSkill(FixSkil);
    }
    public void InputSkill(Skill _skill)
    {
        Skill = _skill;
        Player.SetCurrentSkill(SkillIndex, Skill);

        Image.sprite = Shared.GameMgr.GetSpriteAtlas("Skill_Icons", Skill.SpriteName);
        Image.color = Originalcolor;
    }

    public void UseSkill()
    {
        if(Skill == null)
        {
            Shared.UiMgr.CreateSystemMsg("스킬을 등록해주세요.", SYSTEM_MSG_TYPE.UI);
            return;
        }

        Player.UseSkill(SkillIndex);

        if (Player.IsCurrentSkillNull(SkillIndex) == false)
        {
            CoolTimeText.gameObject.SetActive(true);
            Image.color = new Color(0, 0, 0);
            StartCoroutine(ICoolTime());
        }
    }
   
    IEnumerator ICoolTime()
    {
        float Cooltime = Player.GetSkillCoolTime(SkillIndex);
        float SkillCooltime = Player.GetCurrentSkillCoolTime(SkillIndex);
        while (Cooltime > 0f)
        {
            Cooltime = Player.GetSkillCoolTime(SkillIndex);

            float T = (SkillCooltime - Cooltime) / SkillCooltime;
            Image.fillAmount = T;
            float textT = Cooltime;
            CoolTimeText.text = textT.ToString("F1");
            
            float tcolor = T * 255f;
            Image.color = new Color(tcolor, tcolor, tcolor);

            if (Cooltime <= 0f)
            {
                Image.color = Originalcolor;
                CoolTimeText.gameObject.SetActive(false);
                StopCoroutine(ICoolTime());
            }
            yield return null;
        }
    }
    public void SkillSwap(SkillBtn _other)
    {
        Skill temp = GetSkill();
        InputSkill(_other.GetSkill());
        _other.InputSkill(temp);
    }

    public bool IsSkillNull()
    {
        return Skill == null;
    }

    public Skill GetSkill() { return Skill; }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Dragobj = Instantiate(DragAndDropSkill);
        Dragobj.GetComponent<DragAndDropSkill>().Init(Skill,this);
        Dragobj.transform.SetParent(Canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Dragobj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Dragobj.GetComponent<DragAndDropSkill>().DropSkill();
        Destroy(Dragobj);
    }
}
