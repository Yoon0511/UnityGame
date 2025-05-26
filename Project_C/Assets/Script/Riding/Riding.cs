using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Riding : MonoBehaviour
{
    [SerializeField]
    Animator Animator;
    [SerializeField]
    float WalkSpeed;
    [SerializeField]
    float RunSpeed;
    [SerializeField]
    GameObject Body;
    [SerializeField]
    Player Owner;

    private void Start()
    {
        FsmInit();
    }

    private void FixedUpdate()
    {
        Fsm.UpdateState();
    }

    public void OnRiding()
    {
        Body.SetActive(true);
    }

    public void OffRiding()
    {
        Body.SetActive(false);
    }

    public void PlayAnimation(int _state)
    {
        Animator.SetInteger("Ani_State", _state);
    }

    public float GetSpeedforState()
    {
        switch(CurrState)
        {
            case (int)RIDING_STATE.WALK:
                return WalkSpeed;
            case (int)RIDING_STATE.RUN:
                return RunSpeed;
            default:
                return 0;
        }
    }
    public void SetWalkSpeed(float _speed) { WalkSpeed = _speed; }
    public float GetWalkSpeed() { return WalkSpeed; }
    public void SetRunSpeed(float _speed) { RunSpeed = _speed; }
    public float GetRunSpeed() { return RunSpeed; }
    public void SetOwner(Player _owner) { Owner = _owner; }
    public Player GetOwner() { return Owner; }

}
