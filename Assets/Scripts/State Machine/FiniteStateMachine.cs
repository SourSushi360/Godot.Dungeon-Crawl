using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class FiniteStateMachine : Node
{
	// States
	public List<SimpleState> States;
	public string CurrentState {get; private set;}
	public string LastState;
	protected SimpleState state = null;

    public override void _Ready()
    {
        base._Ready();

		States = GetNode<Node>("States").GetChildren().OfType<SimpleState>().ToList();
    }
	private void SetState(SimpleState _state, Dictionary<string,object> message) {
		if (_state == null) return;
		if (state != null) state.OnExit(_state.GetType().ToString());
		
		LastState = CurrentState;
		CurrentState = _state.GetType().ToString();

		state = _state;
		state.OnStart(message);
		state.OnUpdate();
	}
	public void ChangeState(string stateName, Dictionary<string,object> message = null) {
		foreach(SimpleState _state in States) {
			if (stateName == _state.GetType().ToString()) {
				SetState(_state, message);
				return;
			}
		}
	}
    public override void _PhysicsProcess(double delta)
    {
		if (state == null) return;
		state.UpdateState(delta);
    }
}
