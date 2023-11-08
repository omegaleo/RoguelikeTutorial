using Godot;
using System;

public partial class InputManager : InstancedNode<InputManager>
{
	public Action<Vector3> OnMovement;
	public Action<Vector3> OnCameraMovement;

	public override void _UnhandledInput(InputEvent @event)
	{
		// Make sure we're capturing the mouse so that it doesn't go outside the window and doesn't appear on screen
		if (@event is InputEventMouseButton)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
		// If we press the key assigned to ui_cancel, it'll release the mouse so it can got outside the window
		else if (@event.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}

		// If we're capturing the mouse
		if (Input.MouseMode == Input.MouseModeEnum.Captured)
		{
			// If the mouse has moved
			if (@event is InputEventMouseMotion motion)
			{
				// TODO: turn into an option later on
				var sensitivity = 0.01f; // Mouse sensitivity
				OnCameraMovement.Invoke(new Vector3(motion.Relative.X * sensitivity, motion.Relative.Y * sensitivity, 0f));
			}
		}
		
		base._UnhandledInput(@event);
	}

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
