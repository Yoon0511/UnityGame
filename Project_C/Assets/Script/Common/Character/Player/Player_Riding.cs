using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    protected bool IsRiding = false;

    [SerializeField]
    GameObject Body;
    [SerializeField]
    GameObject Sword;

    [SerializeField]
    GameObject Sanddle;

    [SerializeField]
    Riding Riding;

    public void RidingInit()
    {
        Riding.Init();
    }
    public void SetSanddle(GameObject _sanddle)
    {
        Sanddle = _sanddle;
    }

    public void OnRiding()
    {
        if(PV.IsMine)
        {
            Shared.SoundMgr.PlaySFX("SPWANRIDING");
        }

        Shared.ParticleMgr.CreateParticle("Smoke", transform, 0.7f);

        Riding.OnRiding();
        
        IsRiding = true;
        Sword.SetActive(false);
        Body.transform.localPosition = new Vector3(0, 1.67f, 0.21f);
        Body.transform.SetParent(Sanddle.transform);

        ChangeState((int)STATE.RIDING);
    }

    public void OffRiding()
    {
        Shared.ParticleMgr.CreateParticle("Smoke", transform, 0.7f);

        Riding.OffRiding();
        
        IsRiding = false;
        Sword.SetActive(true);
        Body.transform.SetParent(transform);
        Body.transform.localPosition = Vector3.zero;

        ChangeState((int)STATE.IDLE);
    }

    public void RidingSpeed()
    {
        Riding.GetSpeedforState();
    }
    public Riding GetRiding()
    {
        return Riding;
    }

    public void SetIsRiding(bool _isRiding) {  IsRiding = _isRiding; }
    public bool GetIsRiding() { return IsRiding; }
}
