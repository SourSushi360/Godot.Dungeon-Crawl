using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{
	[Export] public NodePath initialState;
	private Dictionary<string,State> States;
	private State CurrentState;

    public override void _Ready()
    {
        States = new Dictionary<string, State>();
		foreach (Node node in GetChildren()) {
			if (node is State s) {
				States[node.Name] = s;
				s.fsm = this;
				s.Ready();
				s.Exit();
			}
		}
		CurrentState = GetNode<State>(initialState);
		CurrentState.Enter();
    }
    public override void _Process(double delta)
    {
        CurrentState.Update((float) delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        CurrentState.PhysicsUpdate((float) delta);
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        CurrentState.HandleInput(@event);
    }

	public void TransitionTo(string key) {
		if (!States.ContainsKey(key) || CurrentState == States[key]) return;
		CurrentState.Exit();
		CurrentState = States[key];
		CurrentState.Enter();
	}
}
