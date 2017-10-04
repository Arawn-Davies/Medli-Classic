using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using Medli.System;
namespace Medli.MUI
{
    class MUIInit
    {
        public static Canvas canvas;
        public static void Init()
        {
            Console.WriteLine("Cosmos booted successfully. Let's go in Graphic Mode");

            canvas = FullScreenCanvas.GetFullScreenCanvas();

            canvas.Clear(Color.Blue);
            Pen pen = new Pen(Color.Black);
            pen.Color = Color.Black;
            canvas.DrawFilledRectangle(pen, 0, 320, 25, 50);


        }
    }
}
