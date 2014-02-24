using Microsoft.Xna.Framework;
namespace NoTitleGame
{
    interface IAnimate
    {
        // Fields with automatic properties
        int frames { get; set; }
        float elapsed { get; set; }
        float scale { get; set; }
        bool facingRight { get; set; }
        Rectangle sourceRect { get; set; }

        // Proccess animations
        void ProccessAnimations(float delay, GameTime gameTime);

        // Animate character when idle
        void AnimateCharacterIdle(float delay, GameTime gameTime);

        // Animate character when moving
        void AnimateCharacterMove(float delay, GameTime gameTime);
    }
}
