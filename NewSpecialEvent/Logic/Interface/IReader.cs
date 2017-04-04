using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPORTident;
using SPORTident.Common;

namespace NewSpecialEvent.Logic
{
    public interface IReader
    {
        event DataReadCompletedEventHandler CardRead;
        event DeviceConfigurationReadEventHandler DeviceConfigurationRead;
        event ReaderDeviceChangedEventHandler InputDeviceChanged;
        event ReaderDeviceStateChangedEventHandler InputDeviceStateChanged;
        event FileLoggerEventHandler LogEvent;
        event FileLoggerEventHandler ErrorOccured;

        ReaderDeviceInfo OutputDevice { get; set; }

        ReaderDeviceInfo InputDevice { get; set; }

        bool OpenInputDevice();
        bool OpenOutputDevice();

        bool InputDeviceIsOpen {get;}
        bool OutputDeviceIsOpen {get;}
        bool CloseInputDevice();
        bool CloseOutputDevice();

    }
}
