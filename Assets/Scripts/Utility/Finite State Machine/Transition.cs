using System;

public class Transition
{
    /// <summary>
    /// State this transition will go to if Condition == true.
    /// </summary>
    public Type ToState { get; }

    /// <summary>
    /// The condition that will activate this transition.
    /// </summary>
    Func<bool> condition { get; }

    bool manuallyTriggered = false;

    public Transition(Type toState, Func<bool> condition)
    {
        ToState = toState;
        this.condition = condition;
    }

    public Transition(Type toState, Func<bool> condition1, Func<bool> condition2)
    {
        ToState = toState;
        condition = () => condition1() && condition2();
    }

    public Transition(Type toState, Func<bool> condition1, Func<bool> condition2, Func<bool> condition3)
    {
        ToState = toState;
        condition = () => condition1() && condition2() && condition3();
    }

    /// <summary>
    /// Use to manually trigger the transition. Subscribe to an event for an event-based transition.
    /// </summary>
    public void ManuallyTrigger()
    {
        manuallyTriggered = true;
    }

    public bool Check()
    {
        if (manuallyTriggered)
        {
            manuallyTriggered = false;
            return true;
        }
        else
        {
            return condition();
        }
    }
}
