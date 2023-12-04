using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    public class KeyModel
    {
        public Keys Jump { get; init; }
        public Keys Left { get; init; }
        public Keys Right { get; init; }
        public Keys Attack { get; init; }
    }
}
