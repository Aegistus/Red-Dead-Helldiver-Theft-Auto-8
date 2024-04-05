using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SequenceNode : Node
{
    private List<Node> childNodes = new List<Node>();

    public SequenceNode(List<Node> childNodes)
    {
        this.childNodes = childNodes;
    }

    public SequenceNode(Node[] childNodes)
    {
        this.childNodes = childNodes.ToList();
    }
    public override NodeState Evaluate(float deltaTime)
    {
        for (int i = 0; i < childNodes.Count; i++)
        {
            switch (childNodes[i].Evaluate(deltaTime))
            {
                case NodeState.FAILURE:
                    CurrentState = NodeState.FAILURE;
                    return CurrentState;
                case NodeState.RUNNING:
                    CurrentState = NodeState.RUNNING;
                    return CurrentState;
                case NodeState.SUCCESS:
                    continue;
            }
        }
        CurrentState = NodeState.SUCCESS;
        return CurrentState;
    }
}
