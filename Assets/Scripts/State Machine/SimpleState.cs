using Godot;
using System;
using System.Collections.Generic;

public partial class SimpleState : Node
{
	private bool HasBeenInitialized = false;
	private bool OnUpdateHasFired = false;
	// State Events
	[Signal] public delegate void StartEventHandler();
	[Signal] public delegate void UpdatedEventHandler();
	[Signal] public delegate void ExitedEventHandler();

	public virtual void OnStart(Dictionary<string,object> message) {
		EmitSignal(nameof(StartEventHandler));
		HasBeenInitialized = true;
	}
	public virtual void OnUpdate() {
		if (!HasBeenInitialized) return;
		EmitSignal(nameof(UpdatedEventHandler));
		OnUpdateHasFired = true;
	}
	public virtual void UpdateState(double dt) {
		if (!OnUpdateHasFired) return;
	}
	public virtual void OnExit(string nextState) {
		if (!HasBeenInitialized) return;
		EmitSignal(nameof(ExitedEventHandler));
		HasBeenInitialized = false;
		OnUpdateHasFired = false;
	}
}
