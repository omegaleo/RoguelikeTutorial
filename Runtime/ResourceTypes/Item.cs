using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
    [Export] public string Name { get; set; }
    
    [Export] public bool Consumable { get; set; }

    public virtual void Consume()
    {
        
    }
}
