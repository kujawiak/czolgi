using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanki
{
    public class UnitManager
    {
        private List<Actor> _units;

        public UnitManager()
        {
            _units = new List<Actor>();
        }
        public void Add(Actor actor)
        {
            if (!_units.Contains(actor))
            {
                _units.Add(actor);
            }
        }

        public List<Actor> Units { 
            get 
            { 
                return _units; 
            }
        }

        //TODO: replace this dirty way to provide all units with Textures and Spritebatches
        //      Factory Method should do the trick
        public void SetTexture(Texture2D defaultTexture)
        {
            _units.Where(a => a.Texture == null)
                  .Select(a => a.Texture = defaultTexture).ToList();
        }
        public void SetSpriteBatch(SpriteBatch spriteBatch)
        {
            _units.Where(a => a.SpriteBatch == null)
                  .Select(a => a.SpriteBatch = spriteBatch).ToList();
        }

        public void CleanUp()
        {
            // remove all marked to remove units
            _units.RemoveAll(a => a.RemoveMe);
            // remove out of bound projectiles
            // TODO: _probably_ obsolete after setting map boundaries
            _units.RemoveAll(a => a.Type == ActorType.Projectile && a.OutOfBounds);
        }


    }
}
