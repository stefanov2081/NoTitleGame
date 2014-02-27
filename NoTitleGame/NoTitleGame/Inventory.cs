namespace NoTitleGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Interfaces;

    public class Inventory : GameObject, IClickable
    {
        //Fields
        private Rectangle inventoryRectangle;
        private Texture2D pixel;
        private bool isExpanded;

        //Properties
        public Game.FiredEvent OnMouseClick;
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                this.isExpanded = value;
            }
        }

        public Rectangle InventoryRectangle
        {
            get
            {
                return this.inventoryRectangle;
            }
            set
            {
                this.inventoryRectangle = value;
            }
        }

        public Texture2D Pixel
        {
            get { return this.pixel; }
            set { this.pixel = value; }
        }

        //Methods
        //Constructor
        public Inventory(GraphicsDevice device, Texture2D pixel, int x, int y)
            : base(x, y, "Inventory")
        {
            this.IsExpanded = false;
            this.Pixel = pixel;
            int rectY = device.PresentationParameters.BackBufferHeight / 3;
            int rectX = device.PresentationParameters.BackBufferWidth / 2 + 480;
            this.InventoryRectangle = new Rectangle(rectX, rectY, 292, 320);
        }

        //Update location method - for drawing
        public void UpdateInventoryLocation(GraphicsDevice device, SpriteBatch spriteBatch)
        {
            if (this.IsExpanded)
            {
                Rectangle newRect = new Rectangle(this.InventoryRectangle.X + this.InventoryRectangle.Width, this.InventoryRectangle.Y, this.InventoryRectangle.Width, this.InventoryRectangle.Height);
                this.InventoryRectangle = newRect;
                DirectDraw.DrawInventory(this, device, spriteBatch);
            }
            if (!this.IsExpanded)
            {
                Rectangle newRect = new Rectangle(this.InventoryRectangle.X - this.InventoryRectangle.Width, this.InventoryRectangle.Y, this.InventoryRectangle.Width, this.InventoryRectangle.Height);
                this.InventoryRectangle = newRect;
                DirectDraw.DrawInventory(this, device, spriteBatch);
            }
        }

        //Interface method
        public void MouseClick(object sender)
        {
            if (sender is Game)
            {
                isExpanded = !isExpanded;
            }
        }
    }
}
