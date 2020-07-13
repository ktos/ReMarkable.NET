﻿using System;
using ReMarkable.NET.Unix.Driver.Display;
using ReMarkable.NET.Util;

namespace ReMarkable.NET.Unix.Driver
{
    /// <summary>
    /// Provides a static listing of available output devices. When referenced on a tablet, will contain instances of hardware drivers. When run in debug and with the RmEmulator assembly loaded, will contain instances of emulated drivers.
    /// </summary>
    public static class OutputDevices
    {
        /// <summary>
        /// Holds an instance of a display driver
        /// </summary>
        public static readonly IDisplayDriver Display;

        /// <summary>
        /// Loads the appropriate output devices
        /// </summary>
        static OutputDevices()
        {
#if DEBUG
            // Load emulated input devices
            var deviceContainer = Type.GetType("RmEmulator.EmulatedDevices, RmEmulator");
            if (deviceContainer != null)
            {
                deviceContainer.ReadStaticField("Display", out Display);

                return;
            }
#endif
            // Load hardware output devices
            Display = new HardwareDisplayDriver("/dev/fb0");
        }
    }
}
