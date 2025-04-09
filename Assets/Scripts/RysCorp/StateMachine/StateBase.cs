using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("State Enter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("State Stay");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("State Exit");
    }
}

public class StateRunning : StateBase
{

    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
    }

    public override void OnStateExit()
    {
    }
}

public class StateZhonya : StateBase
{

    public override void OnStateEnter(object o = null)
    {
        
        base.OnStateEnter();
    }
}

public class StateDead : StateBase
{

    public override void OnStateEnter(object o = null)
    {
        
        base.OnStateEnter(o);
    }
}