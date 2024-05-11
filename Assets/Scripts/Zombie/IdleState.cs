using Godot;
using System;

public partial class IdleState : State
{
	[Export] CharacterBody2D enemy;
	private Vector2 dir;
	private float timer;
	private Random rand = new Random();

	private void RandomWander() {
		dir = new Vector2(rand.Next(-1,1),rand.Next(-1,1)).Normalized();
		timer = rand.Next(1,2);
	}
    public override void Enter()
    {
        RandomWander();
    }
    public override void Update(float delta)
    {
        if (timer > 0) timer -= delta;
		else RandomWander();
    }
    public override void PhysicsUpdate(float delta)
    {
        enemy.Velocity = dir;
		enemy.MoveAndCollide(dir);
    }
}
