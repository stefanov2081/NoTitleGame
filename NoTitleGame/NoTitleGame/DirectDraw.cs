namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Characters;
    public static class DirectDraw
    {
        // Draw anywhere
        public static void DrawAnywhere(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Texture2D pixel, SpriteBatch spriteBatch,
            int X, int Y, int width, int heigth)
        {
            spriteBatch.Draw(pixel, new Rectangle(X, Y + heigth - thicknessOfBorder, width, thicknessOfBorder), borderColor);
        }

        // Draw bottom line
        public static void DrawToBottom(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Texture2D pixel, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }

        // Draw top line
        public static void DrawToTop(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Texture2D pixel, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
        }

        // Draw left line
        public static void DrawToLeft(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Texture2D pixel, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
        }

        // Draw right line
        public static void DrawToRight(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, Texture2D pixel, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
        }

        // Draw inventory
        public static void DrawInventory(Inventory inventoryObject, GraphicsDevice device, SpriteBatch spriteBatch)
        {
            int imageHeight = device.PresentationParameters.BackBufferHeight / 3;
            DirectDraw.DrawAnywhere(inventoryObject.InventoryRectangle, imageHeight, Color.Wheat, inventoryObject.Pixel, spriteBatch, inventoryObject.InventoryRectangle.X, inventoryObject.InventoryRectangle.Y, 300, 300);
        }

        // Draws bottom stats bars and info
        public static void DrawBottomStats(DrawRectangle background, DrawRectangle progressBar, Character currentChar,
            GraphicsDeviceManager graphics, GraphicsDevice device, SpriteBatch spriteBatch, SpriteFont font)
        {
            // Set the position and dimensions of stats bars
            int barWidthBack = device.PresentationParameters.BackBufferWidth / 2;
            int barHeight = device.PresentationParameters.BackBufferHeight / 26;
            int barX = device.PresentationParameters.BackBufferWidth / 10;
            int barY = device.PresentationParameters.BackBufferHeight - (device.PresentationParameters.BackBufferHeight / 2 - 
                device.PresentationParameters.BackBufferHeight / 3);
            // Update width of stats bars
            float healthBarWidth = (float)device.PresentationParameters.BackBufferWidth / 2.0f * 
                ((float)currentChar.CurrentHealth / (float)currentChar.MaxHealth);
            float manaBarWidht = (float)device.PresentationParameters.BackBufferWidth / 2.0f * 
                ((float)currentChar.CurrentMana / (float)currentChar.MaxMana);
            float shieldBarWidth = (float)device.PresentationParameters.BackBufferWidth / 2.0f * 
                ((float)currentChar.CurrentShield / (float)currentChar.MaxShield);

            // Health bar
            Rectangle healthBar = new Rectangle();
            healthBar.Width = (int)healthBarWidth;
            healthBar.Height = barHeight;
            healthBar.X = barX;
            healthBar.Y = barY;
            // Health bar background
            Rectangle healthBarBG = healthBar;
            healthBarBG.Width = barWidthBack;

            // Mana bar
            Rectangle manaBar = new Rectangle();
            manaBar.Width = (int)manaBarWidht;
            manaBar.Height = barHeight;
            manaBar.X = barX;
            manaBar.Y = barY + healthBar.Height;
            // Mana bar background
            Rectangle manaBarBG = manaBar;
            manaBarBG.Width = barWidthBack;

            // Shield bar
            Rectangle shieldBar = new Rectangle();
            shieldBar.Width = (int)shieldBarWidth;
            shieldBar.Height = barHeight;
            shieldBar.X = barX;
            shieldBar.Y = barY + healthBar.Height + manaBar.Height;
            // Shield bar background
            Rectangle shieldBarBG = shieldBar;
            shieldBarBG.Width = barWidthBack;

            // Draw the background of the stats
            DirectDraw.DrawToBottom(background.Rectangle, device.PresentationParameters.BackBufferHeight / 5 + 2, Color.Black, background.Pixel, spriteBatch);
            DirectDraw.DrawToBottom(background.Rectangle, device.PresentationParameters.BackBufferHeight / 5, Color.White, background.Pixel, spriteBatch);

            // Draw health bar background
            DirectDraw.DrawToBottom(healthBarBG, healthBarBG.Height, Color.White, progressBar.Pixel, spriteBatch);
            // Draw health bar
            DirectDraw.DrawToBottom(healthBar, healthBar.Height, Color.Red, progressBar.Pixel, spriteBatch);

            // Draw mana bar background
            DirectDraw.DrawToBottom(manaBarBG, manaBarBG.Height, Color.White, progressBar.Pixel, spriteBatch);
            // Draw mana bar
            DirectDraw.DrawToBottom(manaBar, manaBar.Height, Color.Blue, progressBar.Pixel, spriteBatch);

            // Draw shield bar background
            DirectDraw.DrawToBottom(shieldBarBG, shieldBarBG.Height, Color.White, progressBar.Pixel, spriteBatch);
            // Draw shield bar
            DirectDraw.DrawToBottom(shieldBar, shieldBar.Height, Color.Yellow, progressBar.Pixel, spriteBatch);

            // Draw infoboxes
            DrawBottomInfo(progressBar, currentChar, graphics, device, spriteBatch, font);
        }

        private static void DrawBottomInfo(DrawRectangle infoBox, Character currentChar, GraphicsDeviceManager graphics, 
            GraphicsDevice device, SpriteBatch spriteBatch, SpriteFont font)
        {
            // Set the position and dimensions of info boxes bars
            int infoBoxWidth = device.PresentationParameters.BackBufferWidth / 8;
            int infoBoxHeight = device.PresentationParameters.BackBufferHeight / 15;
            int infoBoxX = device.PresentationParameters.BackBufferWidth / 2 + device.PresentationParameters.BackBufferWidth / 3;
            int infoBoxY = device.PresentationParameters.BackBufferHeight - (device.PresentationParameters.BackBufferHeight / 2 -
                device.PresentationParameters.BackBufferHeight / 3) - 15;

            // Name info box
            Rectangle nameInfo = new Rectangle();
            nameInfo.Width = infoBoxWidth;
            nameInfo.Height = infoBoxHeight;
            nameInfo.X = infoBoxX;
            nameInfo.Y = infoBoxY;

            // Weapon info box
            Rectangle weaponInfo = new Rectangle();
            weaponInfo.Width = infoBoxWidth;
            weaponInfo.Height = infoBoxHeight;
            weaponInfo.X = infoBoxX;
            weaponInfo.Y = infoBoxY + nameInfo.Height / 2;

            // Level ingo box
            Rectangle levelInfo = new Rectangle();
            levelInfo.Width = infoBoxWidth;
            levelInfo.Height = infoBoxHeight;
            levelInfo.X = infoBoxX;
            levelInfo.Y = infoBoxY + nameInfo.Height / 2 + weaponInfo.Height / 2;

            // Armor info box
            Rectangle armorInfo = new Rectangle();
            armorInfo.Width = infoBoxWidth;
            armorInfo.Height = infoBoxHeight;
            armorInfo.X = infoBoxX;
            armorInfo.Y = infoBoxY + nameInfo.Height / 2 + weaponInfo.Height / 2 + levelInfo.Height / 2;

            if (graphics.PreferredBackBufferWidth == 1024)
            {
                // Draw name info
                DrawToBottom(nameInfo, nameInfo.Height, Color.White, infoBox.Pixel, spriteBatch);
                spriteBatch.DrawString(font, currentChar.Name, new Vector2(nameInfo.X + 2, nameInfo.Y + 14), Color.Black);
                
                // Draw weapon info
                DrawToBottom(weaponInfo, weaponInfo.Height, Color.White, infoBox.Pixel, spriteBatch);
                spriteBatch.DrawString(font, currentChar.SelectedWeapon.ToString(), new Vector2(weaponInfo.X + 2, weaponInfo.Y + 14), Color.Black);

                // Draw level info
                DrawToBottom(levelInfo, levelInfo.Height, Color.White, infoBox.Pixel, spriteBatch);
                spriteBatch.DrawString(font, "Level: " + currentChar.Level.ToString(), new Vector2(levelInfo.X + 2, levelInfo.Y + 14), Color.Black);

                // Armor info
                DrawToBottom(armorInfo, armorInfo.Height, Color.White, infoBox.Pixel, spriteBatch);
                spriteBatch.DrawString(font, "Armor: " + currentChar.Armor.ToString(), new Vector2(armorInfo.X + 2, armorInfo.Y + 14), Color.Black);

            }
            else
            {
                spriteBatch.DrawString(font, currentChar.Name, new Vector2(nameInfo.X, nameInfo.Y), Color.Black);
                spriteBatch.DrawString(font, currentChar.SelectedWeapon.ToString(), new Vector2(weaponInfo.X, weaponInfo.Y), Color.Black);
                spriteBatch.DrawString(font, "Level: " + currentChar.Level.ToString(), new Vector2(levelInfo.X, levelInfo.Y), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + currentChar.Armor.ToString(), new Vector2(armorInfo.X, armorInfo.Y), Color.Black);
            }
        }
    }
}
