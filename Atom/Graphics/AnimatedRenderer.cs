using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.World;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Atom.Graphics
{
    public class AnimatedRenderer : StaticRenderer
    {
        protected int framecountWidth;
        protected int framecountHeight;
        protected float TimePerFrame;
        protected int Frame;
        protected float TotalElapsed;
        protected bool Paused;

        public AnimatedRenderer(GameObject gameObject,
            ContentManager content, string assetName,
            int frameCountWidth, int frameCountHeight, int framesPerSec)
            : base(gameObject,content,assetName)
        {
            framecountWidth = frameCountWidth;
            framecountHeight = frameCountHeight;
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (Paused)
                return;
            TotalElapsed +=(float) gameTime.ElapsedGameTime.TotalSeconds;
            if (TotalElapsed > TimePerFrame)
            {
                Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                Frame = Frame % (framecountWidth * framecountHeight);
                TotalElapsed -= TimePerFrame;
            }

            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

            DrawFrame(spriteBatch, Frame, GameObject.position);
        }
        private void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos)
        {
            int FrameWidth = Image.Width / framecountWidth;
            int sheetx = frame % framecountWidth * FrameWidth;

            int FrameHeight = Image.Height / framecountHeight;
            int sheety = (frame / framecountWidth) * FrameHeight;
         
            Rectangle sourcerect = new Rectangle(sheetx,sheety,
                FrameWidth, FrameHeight);

            batch.Draw(Image, GameObject.position, sourcerect, Color.White,
               GameObject.rotation, GameObject.origin, GameObject.scale, 
               SpriteEffects.None, GameObject.layerDepth);
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
