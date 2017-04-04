using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NewSpecialEvent.Tests;
using SPORTident;
using SPORTident.Common;

namespace NewSpecialEvent.Logic
{
    public class MockReader : IReader
    {

        private CancellationTokenSource cts;

        public ReaderDeviceInfo InputDevice
        {
            get; set;
        }

        public bool InputDeviceIsOpen
        {
            get
            {
                return true;
            }
        }

        public ReaderDeviceInfo OutputDevice
        {
            get; set;
        }

        public bool OutputDeviceIsOpen
        {
            get
            {
                return true;
            }
        }

        public event DataReadCompletedEventHandler CardRead;
        public event DeviceConfigurationReadEventHandler DeviceConfigurationRead;
        public event FileLoggerEventHandler ErrorOccured;
        public event ReaderDeviceChangedEventHandler InputDeviceChanged;
        public event ReaderDeviceStateChangedEventHandler InputDeviceStateChanged;
        public event FileLoggerEventHandler LogEvent;

        public bool CloseInputDevice()
        {
            if (this.cts != null)
            {
                this.cts.Cancel();
            }
            return true;
        }

        public bool CloseOutputDevice()
        {
            return true;
        }

        public bool OpenInputDevice()
        {
            var generator = new MockResultGenerator();
            this.cts = new CancellationTokenSource();
            generator.CardRead += this.CardRead;
            generator.PollAsync(this.cts.Token);
            return true;
        }

        public bool OpenOutputDevice()
        {
            return true;
        }
    }
}
