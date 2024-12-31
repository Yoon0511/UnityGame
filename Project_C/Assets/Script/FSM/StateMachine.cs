using UnityEngine;

public class StateMachine
{
    private StateBase cur_state;

    public StateMachine()
    {
        cur_state = null;
    }
    public StateMachine(StateBase _state)
    {
        cur_state = _state;
        ChangeState(_state);
    }

    public void ChangeState(StateBase _nextstate)
    {
        if (cur_state == _nextstate) return;

        if (cur_state != null)
            cur_state.OnStateExit();

        cur_state = _nextstate;
        cur_state.OnStateEnter();
    }

    public void UpdateState()
    {
        if (cur_state != null)
            cur_state.OnStateUpdate();
    }

}
