using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Save : Resource
{
	[Export] public Vector3 Position { get; set; }
	public Dictionary<Item, int> Inventory { get; set; }

	public Save() : this(Vector3.Zero, new Dictionary<Item, int>()) {}
	
	public Save(Vector3 pos, Dictionary<Item, int> inventory)
	{
		Position = pos;
		Inventory = inventory;
	}
}
