﻿using ReMarkable.NET.Unix.Driver.Display;
using ReMarkable.NET.Unix.Driver.Display.EinkController;
using ReMarkable.NET.Unix.Driver.Display.Framebuffer;
using RmEmulator.Framebuffer;
using SixLabors.ImageSharp;

namespace RmEmulator.Devices
{
    public class EmulatedDisplayDriver : IDisplayDriver
    {
        private readonly EmulatorWindow _emulatorWindow;

        public int VirtualWidth => VisibleWidth;
        public int VirtualHeight => VisibleHeight;
        public int VisibleWidth { get; }
        public int VisibleHeight { get; }
        public IFramebuffer Framebuffer { get; }

        public EmulatedDisplayDriver(EmulatorWindow emulatorWindow, int visibleWidth, int visibleHeight)
        {
            _emulatorWindow = emulatorWindow;
            VisibleWidth = visibleWidth;
            VisibleHeight = visibleHeight;

            Framebuffer = new EmulatedFramebuffer(_emulatorWindow, visibleWidth, visibleHeight);
        }

        public void Refresh(Rectangle rectangle, WaveformMode mode)
        {
            _emulatorWindow.RefreshRegion(rectangle, mode);
        }
    }
}