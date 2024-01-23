
 
using System.Drawing;


public static class TileSets 
{
    
    public static void tileSets() 
    {
        Memory.Tileset = new Sprite[22,20];

        Image img = SpriteBuffer.Current.Get("src/sprites/tileset/Tile.png");
        
 
        int spriteWidth = 24;
        int spriteHeight = 24;

        int spritesRows = img.Width / spriteWidth;
        int spritesColuns = img.Height / spriteHeight;


        Memory.Tileset = new Sprite[spritesColuns, spritesRows];

        for (int i = 0; i < spritesColuns; i++)
        {
            for (int j = 0; j < spritesRows; j++)
            {

                int x = j * spriteWidth;
                int y = i * spriteHeight;
                int layer = 0;

                RectangleF spriteRect = new RectangleF(x, y, spriteWidth, spriteHeight);

                SubImage subImage = new SubImage(img, spriteRect); // teste


                if(spritesRows == 13) 
                    layer = 1;

                Sprite sprite = new Sprite(subImage, null, new PointF(0, 0), new SizeF(240,240), layer );
                Memory.Tileset[i, j] = sprite;
                Screen.Queue.Add(sprite);
            }
        }
    }
}

       