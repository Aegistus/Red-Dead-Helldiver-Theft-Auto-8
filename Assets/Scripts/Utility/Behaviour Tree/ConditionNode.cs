using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConditionNode : Node
{
    private Func<bool> condition;

    public ConditionNode(Func<bool> condition)
    {
        this.condition = condition;
    }

    public override NodeState Evaluate(float deltaTime)
    {
        CurrentState = ConvertToNodeState(condition());
        return CurrentState;
    }

    private NodeState ConvertToNodeState(bool condition)
    {
        if (condition == true)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
