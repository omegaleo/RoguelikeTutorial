using Godot;
using System;

public partial class InputManager : InstancedNode<InputManager>
{
	public Action<Vector3> OnMovement;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Declare our movement as 0,0,0 at start
		var movement = Vector3.Zero;
		
		if (Input.IsActionPressed("movementUp"))
		{
			movement.Z = 1f;
		}
		else if (Input.IsActionPressed("movementDown"))
		{
			movement.Z = -1f;
		}
		
		if (Input.IsActionPressed("movementRight"))
		{
			movement.X = 1f;
		}
		else if (Input.IsActionPressed("movementLeft"))
		{
			movement.X = -1f;
		}

		// If our movement is not 0,0,0 , let's invoke our Action that will call the methods that have been hooked to it.
		if (movement != Vector3.Zero)
		{
			try
			{
				OnMovement.Invoke(movement);
			}
			catch (Exception e)
			{
				// In case of an exception, let's print the error message,
				// most of the time it can be related to a hook not being deleted once the script it belongs to has been destroyed
				GD.Print(e.Message);
			}
		}
		
		base._Process(delta);
	}
}
