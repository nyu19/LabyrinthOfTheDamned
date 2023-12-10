/*
 * Names:
 *  - Nakul Upasani
 *  - Shahyar Fida
 * Revision History:
 *  - Created By Nakul Upasani; Created: 1-Dec-2023
 * 
 */
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthOfTheDamned.Scenes.ActionScene.Components
{
    /// <summary>
    /// Texture Modes
    /// </summary>
    public class TextureModel
    {
        public Texture2D Walk { get; init; }
        public Texture2D Attack { get; init; }
        public Texture2D Idle { get; init; }
        public Texture2D Hurt { get; init; }
        public Texture2D Death { get; init; }
    }
}
