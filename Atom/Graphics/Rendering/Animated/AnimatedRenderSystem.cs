﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atom.Graphics;
using Atom.Physics;
using Microsoft.Xna.Framework;

namespace Atom.Graphics.Rendering.Animated
{
    public class AnimatedRenderSystem : BaseSystem
    {
        AnimatedSpriteComponent animatedSpriteComponent;
        AnimatedSequenceComponent animatedSequenceComponent;
        PositionComponent positionComponent;
        Rectangle spriteRectangle, sourceRectangle;

        public AnimatedRenderSystem()
        {
            ComponentTypeFilter = new TypeFilter()
                .AddFilter(typeof(AnimatedSpriteComponent))
                .AddFilter(typeof(PositionComponent))
                .AddFilter(typeof(AnimatedSequenceComponent));
        }

        public void GetComponents(int entityId)
        {
            positionComponent =
                GetComponentsByEntityId<PositionComponent>(entityId).FirstOrDefault();
            animatedSpriteComponent =
                GetComponentsByEntityId<AnimatedSpriteComponent>(entityId).FirstOrDefault();
            animatedSequenceComponent =
                GetComponentsByEntityId<AnimatedSequenceComponent>(entityId).FirstOrDefault();

            spriteRectangle =
                new Rectangle((int)positionComponent.X, (int)positionComponent.Y,
                    animatedSpriteComponent.FrameWidth, animatedSpriteComponent.FrameHeight);
        }
 
        int time;

        public override void Update(GameTime gameTime, int entityId)
        {
            GetComponents(entityId);
            if (animatedSpriteComponent == null || positionComponent == null) return;

            float frameRate = (1f / animatedSpriteComponent.FramesPerSecond) / (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.time > frameRate)
            { 
                if(animatedSequenceComponent == null )
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
                = animatedSpriteComponent.Image.Width;
            int sheetHeight
                = animatedSpriteComponent.Image.Height;

            //Frame Size 
            int frameWidth
                = animatedSpriteComponent.FrameWidth;
            int frameHeight
                = animatedSpriteComponent.FrameHeight;

            //Number of Sprite Elements 
            int numberOfSpritesX = sheetWidth / frameWidth;
            int numberOfSpritesY = sheetHeight / frameHeight;

            int sheetPositionX =
                (animatedSpriteComponent.FrameIndex % numberOfSpritesX) * frameWidth;
            int sheetPositionY =
                (animatedSpriteComponent.FrameIndex / numberOfSpritesX) * frameHeight;

            animatedSpriteComponent.Location
                = new Point(sheetPositionX, sheetPositionY);
            sourceRectangle = 
                new Rectangle((int) animatedSpriteComponent.Location.X, (int) animatedSpriteComponent.Location.Y,
                    animatedSpriteComponent.FrameWidth, animatedSpriteComponent.FrameHeight);

        }

        public void LinearSequence(int entityId)
        {
            if (animatedSpriteComponent == null) return;

            int endFrame = animatedSpriteComponent.FrameCount;

            if (animatedSpriteComponent.FrameIndex < endFrame)
                animatedSpriteComponent.FrameIndex++;
            else
                animatedSpriteComponent.FrameIndex = animatedSpriteComponent.SequenceStartFrame;
            
             SpriteLocation();
        }

        int currentSequenceIndex;
        public void CompositeSequence(int entityId)
        {
            if (animatedSpriteComponent == null || animatedSequenceComponent == null) return;

            int endFrame = animatedSequenceComponent.AnimationSequence.Count() -1;
            if (endFrame < 1) return;

            
                animatedSpriteComponent.FrameIndex =
                    animatedSequenceComponent.AnimationSequence[currentSequenceIndex];
                if (animatedSequenceComponent.CurrentSequenceDirection == SequenceDirection.FORWARD)
                {
                    currentSequenceIndex++;
                    if (currentSequenceIndex > endFrame - animatedSpriteComponent.SequenceStartFrame)
                        currentSequenceIndex = animatedSpriteComponent.SequenceStartFrame;
                }
                else if (animatedSequenceComponent.CurrentSequenceDirection == SequenceDirection.BACKWARD)
                {
                    currentSequenceIndex--;
                    if (currentSequenceIndex < animatedSpriteComponent.SequenceStartFrame)
                        currentSequenceIndex = animatedSpriteComponent.SequenceStartFrame + endFrame;
                }
        

            SpriteLocation();
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, int entityId)
        {
            GetComponents(entityId);
            if (animatedSpriteComponent == null || positionComponent == null) return;

            spriteBatch.Draw(animatedSpriteComponent.Image, spriteRectangle, sourceRectangle, Color.White);

            base.Draw(spriteBatch, entityId);
        }
    }
}
