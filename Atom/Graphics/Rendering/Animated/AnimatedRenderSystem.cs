using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Atom.Graphics;
using Atom.Physics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Atom.Graphics.Rendering.Animated
{
    public class AnimatedRenderSystem : BaseSystem
    {
        AnimatedSpriteComponent _animatedSpriteComponent;
        AnimatedSequenceComponent _animatedSequenceComponent;
        PositionComponent _positionComponent;
        Rectangle _spriteRectangle, _sourceRectangle;
        
        int currentSequenceIndex;
        int time;

        public AnimatedRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(AnimatedSpriteComponent))
                .AddFilter(typeof(PositionComponent))
                .AddFilter(typeof(AnimatedSequenceComponent));
        }

        public void GetComponents(int entityId)
        {
            _positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();
            _animatedSpriteComponent =
                GetComponentsByEntityId<AnimatedSpriteComponent>(entityId).FirstOrDefault();
            _animatedSequenceComponent =
                GetComponentsByEntityId<AnimatedSequenceComponent>(entityId).FirstOrDefault();

            if (_positionComponent == null || _animatedSpriteComponent == null) return;

            _spriteRectangle =
                new Rectangle((int)_positionComponent.X, (int)_positionComponent.Y,
                    _animatedSpriteComponent.FrameWidth, _animatedSpriteComponent.FrameHeight);
        }
 
        public override void Update(GameTime gameTime, int entityId)
        {
            GetComponents(entityId);
            if (_animatedSpriteComponent == null || _positionComponent == null) return;

            float frameRate = (1f / _animatedSpriteComponent.FramesPerSecond) / (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.time > frameRate)
            { 
                if(_animatedSequenceComponent == null )
                LinearSequence(entityId);
                else
                CompositeSequence(entityId);
                this.time = 0;
            }
            else
                this.time++;


            base.Update(gameTime, entityId);
        }

        public void SpriteLocation()
        {
            //Dimension of the spriteSheet 
            int sheetWidth
                = _animatedSpriteComponent.Image.Width;
            int sheetHeight
                = _animatedSpriteComponent.Image.Height;

            //Frame Size 
            int frameWidth
                = _animatedSpriteComponent.FrameWidth;
            int frameHeight
                = _animatedSpriteComponent.FrameHeight;

            //Number of Sprite Elements 
            int numberOfSpritesX = sheetWidth / frameWidth;
            int numberOfSpritesY = sheetHeight / frameHeight;

            int sheetPositionX =
                (_animatedSpriteComponent.FrameIndex % numberOfSpritesX) * frameWidth;
            int sheetPositionY =
                (_animatedSpriteComponent.FrameIndex / numberOfSpritesX) * frameHeight;

            _animatedSpriteComponent.Location
                = new Point(sheetPositionX, sheetPositionY);
            _sourceRectangle = 
                new Rectangle((int) _animatedSpriteComponent.Location.X, (int) _animatedSpriteComponent.Location.Y,
                    _animatedSpriteComponent.FrameWidth, _animatedSpriteComponent.FrameHeight);

        }

        public void LinearSequence(int entityId)
        {
            if (_animatedSpriteComponent == null) return;

            int endFrame = _animatedSpriteComponent.FrameCount;

            if (_animatedSpriteComponent.FrameIndex < endFrame)
                _animatedSpriteComponent.FrameIndex++;
            else
                _animatedSpriteComponent.FrameIndex = _animatedSpriteComponent.SequenceStartFrame;
            
             SpriteLocation();
        }
        
        public void CompositeSequence(int entityId)
        {
            if (_animatedSpriteComponent == null || _animatedSequenceComponent == null) return;

            int endFrame = _animatedSequenceComponent.AnimationSequence.Count() -1;
            if (endFrame < 1) return;

            
                _animatedSpriteComponent.FrameIndex =
                    _animatedSequenceComponent.AnimationSequence[currentSequenceIndex];
                if (_animatedSequenceComponent.CurrentSequenceDirection == SequenceDirection.FORWARD)
                {
                    currentSequenceIndex++;
                    if (currentSequenceIndex > endFrame - _animatedSpriteComponent.SequenceStartFrame)
                        currentSequenceIndex = _animatedSpriteComponent.SequenceStartFrame;
                }
                else if (_animatedSequenceComponent.CurrentSequenceDirection == SequenceDirection.BACKWARD)
                {
                    currentSequenceIndex--;
                    if (currentSequenceIndex < _animatedSpriteComponent.SequenceStartFrame)
                        currentSequenceIndex = _animatedSpriteComponent.SequenceStartFrame + endFrame;
                }
        

            SpriteLocation();
        }

        public override void Draw(SpriteBatch spriteBatch, int entityId)
        {
            GetComponents(entityId);
            if (_animatedSpriteComponent == null || _positionComponent == null) return;

            spriteBatch.Draw(_animatedSpriteComponent.Image, _spriteRectangle, _sourceRectangle, Color.White);

            base.Draw(spriteBatch, entityId);
        }
    }
}
