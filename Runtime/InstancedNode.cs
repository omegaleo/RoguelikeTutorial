using Godot;
using System;

public partial class InstancedNode<T> : Node where T : Node
{
	public static T Instance = null;

	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this as T;
		}
		
		base._Ready();
	}
}
