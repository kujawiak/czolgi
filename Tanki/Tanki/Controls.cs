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
        public KeyboardState oldState;
        public KeyboardState newState;
        public Direction direction { get; set; }
        public Controls()
        {
            oldState = Keyboard.GetState();
        }

        //class properties
        public bool CheckIfCanClose
        {
            get { return GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape); }
        }
        public bool ShootPressed  
        {
            get { return Keyboard.GetState().IsKeyDown(Keys.Space); }
        }
        private bool generateEnemiesPressed;
        public bool GenerateEnemiesPressed
        {
            get{ return generateEnemiesPressed; }
            set{ generateEnemiesPressed = value; }
        }
        public static string GetEnumMemberValue<T>(T enumItem) where T : struct
        {
            return  enumItem.ToString();
        }
       
        public Direction MoveControler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && // I think is ugly way to block other directions but I didn't find better at the moment...
                Keyboard.GetState().IsKeyUp(Keys.Up) &&
                Keyboard.GetState().IsKeyUp(Keys.Down) &&
                Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                return  Direction.Right;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) &&
                Keyboard.GetState().IsKeyUp(Keys.Up) &&
                Keyboard.GetState().IsKeyUp(Keys.Down) &&
                Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                return Direction.Left;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) &&
                Keyboard.GetState().IsKeyUp(Keys.Left) &&
                Keyboard.GetState().IsKeyUp(Keys.Down) &&
                Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                return Direction.Up;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) &&
                Keyboard.GetState().IsKeyUp(Keys.Left) &&
                Keyboard.GetState().IsKeyUp(Keys.Up) &&
                Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                return Direction.Down;
            }
            return Direction.unknown;
        }
        public bool OneKeyPress(Keys key) // we need this to catch only one key stroke
        {
            oldState = newState;
            newState = Keyboard.GetState();
            if (newState.IsKeyDown(key) && oldState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }
    }
}
