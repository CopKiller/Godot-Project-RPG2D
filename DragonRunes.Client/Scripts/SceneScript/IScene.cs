using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonRunes.Client.Scripts.SceneScript
{
    public interface IScene
    {
        void InitializeSceneData<T>(List<T> data) where T : Node;
    }
}
