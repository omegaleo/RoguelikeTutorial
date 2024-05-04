using Godot;
using System;
using Roguelike.Runtime;

public partial class Player : CharacterBody3D
{
	public const float SPEED = 5.0f;
	public const float JUMP_VELOCITY = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float Gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	[Export] public InventoryHandler _inventory;

	public override void _Ready()
	{
		if (InputManager.Instance != null)
		{
			InputManager.Instance.OnMovement += OnMovement;
		}
		else
		{
			GD.Print("Input Manager is not created!");
		}
		
		_inventory.TestInventory();
		SaveHandler.Instance.SaveGame(Position, _inventory.GetInventory());
		
		base._Ready();
	}

	private void OnMovement(Vector3 inputDir)
	{
		Vector3 velocity = Velocity;
		

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JUMP_VELOCITY;

		// Get the input direction and handle the movement/deceleration.
		Vector3 direction = (Transform.Basis * inputDir).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * SPEED;
			velocity.Z = direction.Z * SPEED;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, SPEED);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= Gravity * (float)delta;
	}
}
