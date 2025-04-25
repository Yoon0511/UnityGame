using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Breath : Skill
{
    public ParticleSystem BREATH_EFFECT;
    public float Duration;
    public float DamageInterval;

    public GameObject P0; //p0
    public GameObject P1; //p1
    public GameObject P2; //p2

    Player Target;

    Coroutine IBreathCorutine;
    [SerializeField]
    BoxCollider BoxCollider;
    private void Start()
    {
        BoxCollider.enabled = false;
        BREATH_EFFECT.Stop();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        IBreathCorutine = StartCoroutine(IBreath());
    }

    IEnumerator IBreath()
    {
        BoxCollider.enabled = true;
        BREATH_EFFECT.Play();

        float elapsedTime = 0;
        while (elapsedTime < Duration)
        {
            elapsedTime += DamageInterval;

            if (Target != null && IsPointInTriangle(Target.transform.position))
            {
                DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(Atk, DAMAGEFONT_TYPE.YELLOW);
                Target.Hit(damgedata);
            }

            yield return new WaitForSeconds(DamageInterval);
        }

        BoxCollider.enabled = false;
        BREATH_EFFECT.Stop();
        yield return new WaitForSeconds(BREATH_EFFECT.main.duration);
    }

    bool IsPointInTriangle(Vector3 _target)
    {
        // Barycentric 좌표 계산
        Vector3 v0 = P1.transform.position - P0.transform.position;
        Vector3 v1 = P2.transform.position - P0.transform.position;
        Vector3 v2 = _target - P0.transform.position;

        float dot00 = Vector3.Dot(v0, v0);
        float dot01 = Vector3.Dot(v0, v1);
        float dot02 = Vector3.Dot(v0, v2);
        float dot11 = Vector3.Dot(v1, v1);
        float dot12 = Vector3.Dot(v1, v2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        // 점이 삼각형 내부에 있으면 u와 v가 0 이상이고 u + v <= 1
        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(Shared.GameMgr.IsCheckCharacterType(other,(int)CHARACTER_TYPE.PLAYER))
        {
            Target = other.GetComponent<Player>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Target = null;
    }

    public void BreathStart()
    {
        BREATH_EFFECT.Play();
    }

    public void BreathEnd()
    {
        BREATH_EFFECT.Stop();
        StopCoroutine(IBreathCorutine);
    }

    public override void SkillEnd()
    {
        base.SkillEnd();
        BreathEnd();
        BoxCollider.enabled = false;
    }
}
