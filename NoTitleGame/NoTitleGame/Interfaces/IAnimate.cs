namespace Interfaces
{
    using Microsoft.Xna.Framework;
    interface IAnimate
    {
        // Fields with automatic properties
        int Frames { get; set; }
        float Elapsed { get; set; }
        float Scale { get; set; }
        bool FacingRight { get; set; }
        Rectangle SourceRect { get; set; }

        // Proccess animations
        void ProccessAnimations(float delay, GameTime gameTime);

        // Animate character when idle
        void AnimateCharacterIdle(float delay, GameTime gameTime);

        // Animate character when moving
        void AnimateCharacterRun(float delay, GameTime gameTime);

        // Animate character when jumping
        void AnimateCharacterJump(float delay, GameTime gameTime);
    }
}
