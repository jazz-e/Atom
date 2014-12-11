using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atom.Graphics
{
    public class AnimatedRenderer : Renderer
    {
        protected int framecount;
        protected float TimePerFrame;
        protected int Frame;
        protected float TotalElapsed;
        protected bool Paused;

        public AnimatedRenderer(GameObject gameObject,
            ContentManager content, string assetName,
            int frameCount, int framesPerSec)
            : base(gameObject,content,assetName)
        {
            framecount = frameCount;
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (Paused)
                return;
            TotalElapsed +=(float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % framecount;
                TotalElapsed -= TimePerFrame;
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            DrawFrame(spriteBatch, Frame, _gameObject.position);
        }
        private void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
        {
            int FrameWidth = _image.Width / framecount;
            Rectangle sourcerect = new Rectangle(FrameWidth * frame, 0,
                FrameWidth, _image.Height);
            batch.Draw(_image, _gameObject.position, sourcerect, Color.White,
               _gameObject.rotation, _gameObject.origin, _gameObject.scale, 
               SpriteEffects.None, _gameObject.layerDepth);
        }

        public bool IsPaused
        {
            get { return Paused; }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Pause();
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        public void Pause()
        {
            Paused = true;
        }

    }
}
