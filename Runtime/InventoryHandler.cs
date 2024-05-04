using Godot;
using System;
using System.Collections.Generic;
using Roguelike.Runtime;

public partial class InventoryHandler : Node
{
	private Dictionary<Item, int> _inventory = new Dictionary<Item, int>();

	[Export] public Item testItem;
	
	public void AddItem(Item item, int amount = 1)
	{
		if (_inventory.ContainsKey(item))
		{
			if (_inventory.TryGetValue(item, out int amountOfItem))
			{
				_inventory[item] = amountOfItem + amount;
			}
			else
			{
				_inventory.Add(item, amount);
			}
		}
	}

	public void Consume(Item item)
	{
		if (_inventory.ContainsKey(item))
		{
			if (_inventory.TryGetValue(item, out int amount))
			{
				if (amount > 0)
				{
					if (item.Consumable)
					{
						item.Consume();
						RemoveItem(item);
					}
					else
					{
						GD.PrintErr($"Item {item.Name} is not a consumable!");
					}
				}
				else
				{
					GD.PrintErr($"Not enough items in inventory of type {item.Name}!");
					return;
				}
			}
		}
		else
		{
			GD.PrintErr($"Item not found {item.Name}!");
			return;
		}
	}

	public void RemoveItem(Item item, int amount = 1)
	{
		if (_inventory.ContainsKey(item))
		{
			if (_inventory.TryGetValue(item, out int value))
			{
				if (value > amount)
				{
					_inventory[item] = value - amount;
				}
				else
				{
					GD.PrintErr($"Attempting to remove more items than the existing in the inventory of {item.Name}");
				}
			}
		}
	}

	public void TestInventory() => AddItem(testItem);

	public Dictionary<Item, int> GetInventory() => _inventory;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
