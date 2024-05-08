using Godot;
using System;

public partial class player_knight : CharacterBody2D
{
	// Stats
	private const float speed = 200.0f;
	[Export] public int hp = 10;
	// Attributes
	private Vector2 velocity, direction;
	public AnimatedSprite2D sprite2D;

	// Process
    public override void _Ready()
    {
        sprite2D = GetNode<AnimatedSprite2D>("Sprite2D");
    }
    public override void _PhysicsProcess(double delta)
    {
		velocity = Vector2.Zero;
		if (Input.IsActionPressed("up")) {
			velocity.Y -= 1;
			direction = new Vector2(direction.X, -1);
		}
		if (Input.IsActionPressed("down")) {
			velocity.Y += 1;
			direction = new Vector2(direction.X, 1);
		}
		if (Input.IsActionPressed("left")) {
			velocity.X -= 1;
			direction = Vector2.Left;
		}
		if (Input.IsActionPressed("right")) {
			velocity.X += 1;
			direction = Vector2.Right;
		}
		velocity = velocity.Normalized();
		Velocity = velocity*speed;
		// velocity.X = Mathf.MoveToward(Velocity.X, 0, 50);

		bool isLeft = direction.X < 0;
		sprite2D.FlipH = isLeft;
		if (velocity.X != 0 || velocity.Y != 0) {
			sprite2D.Animation = "run";
		} else sprite2D.Animation = "idle";
		MoveAndSlide();
    }
    public override void _Process(double delta)
    {
    }
}
