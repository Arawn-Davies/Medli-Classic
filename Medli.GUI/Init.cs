using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using Medli;

namespace Medli.GUI
{
    public class MUI
    {
        public static Cosmos.HAL.Mouse m = new Cosmos.HAL.Mouse();

        public static Canvas canvas;
        public static void Init()
        {
            Console.WriteLine("Cosmos booted successfully. Let's go in Graphic Mode");
            Console.Clear();
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            Main();
            

        }
        public static void Main()
        {
            canvas.Clear(Color.Blue);
            Pen pen = new Pen(Color.Black);
            pen.Color = Color.Black;
            canvas.DrawFilledRectangle(pen, 0, 0, 600, 50);
            Pen mousePen = new Pen(Color.White);
            mousePen.Color = Color.White;
            DrawMouse(mousePen, 40, 25);
            Console.ReadKey(true);
        }
        public static void MouseDraw()
        {
            Pen pen = new Pen(Color.Red);
            canvas.DrawLine(pen, m.X, m.Y, m.X + 5, m.Y);
            canvas.DrawLine(pen, m.X, m.Y, m.X, m.Y - 5);
            canvas.DrawLine(pen, m.X, m.Y, m.X + 5, m.Y - 5);
        }
        public static void DrawMouse(Pen pen, int x, int y)
        {
            canvas.DrawPoint(pen, x, y);
            canvas.DrawPoint(pen, x + 1, y + 1);
            canvas.DrawPoint(pen, x + 1, y);
            canvas.DrawPoint(pen, x, y + 1);
            canvas.DrawPoint(pen, x +2 , y + 1);
            canvas.DrawPoint(pen, x + 1, y + 2);
            canvas.DrawPoint(pen, x + 2, y + 2);
            canvas.DrawPoint(pen, x + 3, y + 3);
            canvas.DrawPoint(pen, x + 4, y + 4);
        }
    }
}
