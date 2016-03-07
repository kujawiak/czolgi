using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Tanki
{
    class Controls
    {
        public KeyboardState ks = Keyboard.GetState();

        public bool CheckIfCanClose()
        {
            return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
        }
        public int MoveControler()
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                return 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                return 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                return 3;
            }
            return -1;
        }
        public bool ShootPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Space);
        }
        public bool GenerateEnemiesPressed()
        {
            return Keyboard.GetState().IsKeyDown(Keys.N);
        }
    }
}
