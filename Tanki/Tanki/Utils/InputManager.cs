using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki.Utils
{
    public class InputManager
    {
        private KeyboardState _oldState { get; set; }
        public InputManager(KeyboardState keyboardState)
        {
            _oldState = keyboardState;
        }

        internal bool SingleStroke(KeyboardState state, Keys key)
        {
            bool result = false;
            if (state.IsKeyDown(Keys.Q) && _oldState.IsKeyUp(Keys.Q))
            {
                result = true;
            }

            _oldState = state;
            return result;
        }
    }
}
