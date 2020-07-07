﻿using System;
using Graphite;
using Graphite.Controls;
using Graphite.Typography;
using ReMarkable.NET.Unix.Driver;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var screen = OutputDevices.Display;

            var w = new Window(screen.VisibleWidth, screen.VisibleHeight);
            w.Update += WindowUpdate;

            InputDevices.Digitizer.Pressed += (sender, code) => w.ConsumePress(InputDevices.Digitizer.State, code);
            InputDevices.Digitizer.Released += (sender, code) => w.ConsumeRelease(InputDevices.Digitizer.State, code);
            InputDevices.Digitizer.StylusUpdate += (sender, state) => w.ConsumeMove(state);

            InputDevices.Touchscreen.Pressed += (sender, finger) => w.ConsumePress(finger);
            InputDevices.Touchscreen.Released += (sender, finger) => w.ConsumeRelease(finger);
            InputDevices.Touchscreen.Moved += (sender, finger) => w.ConsumeMove(finger);

            var mainPage = w.CreatePage();

            var presses = 0;
            var random = new Random();

            var l = new Label
            {
                Bounds = new Rectangle(50, 150, 230, 50),
                Text = "Label"
            };

            var b = new Button
            {
                Bounds = new Rectangle(50, 50, 230, 50),
                Text = "Send"
            };

            b.FingerPress += (sender, finger) =>
            {
                l.Text = $"{Math.Pow(10, random.Next(6))} {presses} presses";
                presses++;
            };

            mainPage.Content.Add(b);
            mainPage.Content.Add(l);

            w.ShowPage(mainPage);

            while (true) { }
        }

        private static void WindowUpdate(object sender, WindowUpdateEventArgs e)
        {
            OutputDevices.Display.Draw(e.Buffer, e.UpdatedArea, e.UpdatedArea.Location);
        }
    }
}
