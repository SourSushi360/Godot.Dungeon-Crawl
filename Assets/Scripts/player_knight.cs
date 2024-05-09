using Godot;
using System;

public partial class player_knight : CharacterBody2D
{
	// Attributes
	[Export] public const float speed = 200.0f;
	[Export] public int hp = 10;
	public AnimatedSprite2D sprite2D;
	private Vector2 direction;
	
	// Process
    public override void _Ready()
    {
        sprite2D = GetNode<AnimatedSprite2D>("Sprite2D");
    }
    public override void _PhysicsProcess(double delta)
    {
		direction = Input.GetVector("left","right","up","down").Normalized();
		Velocity = direction*speed;
		
		bool isLeft = direction.X < 0;
		sprite2D.FlipH = isLeft;
		if (Velocity.IsZeroApprox()) {
			sprite2D.Animation = "idle";
		} else sprite2D.Animation = "run";
		MoveAndSlide();
    }
}
