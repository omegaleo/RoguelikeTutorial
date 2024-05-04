
using Godot;
using Godot.Collections;

namespace Roguelike.Runtime;

public partial class SaveHandler : InstancedNode<SaveHandler>
{
    private const string SAVE_GAME_PATH = "user://savegame_{0}.tres";
    private const int MAX_SAVE_FILES = 4;

    private Save[] _saves = new Save[MAX_SAVE_FILES];

    public void SaveGame(Vector3 pos, System.Collections.Generic.Dictionary<Item, int> inventory, int slot = 0)
    {
        var save = new Save(pos, inventory);
        var fileName = string.Format(SAVE_GAME_PATH, slot);
        ResourceSaver.Save(save, fileName);
        _saves[slot] = save;
    }

    public override void _Ready()
    {
        for (int slot = 0; slot < MAX_SAVE_FILES; slot++)
        {
            string fileName = string.Format(SAVE_GAME_PATH, slot);

            if (ResourceLoader.Exists(fileName))
            {
                var save = ResourceLoader.Load<Save>(fileName);
                _saves[slot] = save;
            }
        }
        
        base._Ready();
    }

    public void LoadGame(int slot = 0)
    {
        
    }
}