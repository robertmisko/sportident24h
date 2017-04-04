using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPORTident;
using SPORTident.Common;

namespace NewSpecialEvent.Logic
{
    public class SiReader : IReader
    {
        private Reader reader;

        public SiReader(Reader reader)
        {
            this.reader = reader;
            this.reader.WriteBackupFile = true;
            this.reader.BackupFileName = Path.Combine(Environment.CurrentDirectory, 
                                            string.Format(@"backup\{0:yyyy-MM-dd}_stamps.bak", 
                                            DateTime.Now));
            this.reader.WriteLogFile = false;
        }

        public ReaderDeviceInfo InputDevice
        {
            get
            {
                return this.reader.InputDevice;
            }

            set
            {
                this.reader.InputDevice = value;
            }
        }

        public bool InputDeviceIsOpen
        {
            get
            {
                return this.reader.InputDeviceIsOpen;
            }
        }

        public ReaderDeviceInfo OutputDevice
        {
            get
            {
                return this.reader.OutputDevice;
            }

            set
            {
                this.reader.OutputDevice = value;
            }
        }

        public bool OutputDeviceIsOpen
        {
            get
            {
                return this.reader.OutputDeviceIsOpen;
            }
        }

        public event DataReadCompletedEventHandler CardRead
        {
            add
            {
                this.reader.CardRead += value;
            }
            remove
            {
                this.reader.CardRead -= value;
            }
        }

        public event DeviceConfigurationReadEventHandler DeviceConfigurationRead
        {
            add
            {
                this.reader.DeviceConfigurationRead += value;
            }
            remove
            {
                this.reader.DeviceConfigurationRead -= value;
            }
        }

        public event ReaderDeviceChangedEventHandler InputDeviceChanged
        {
            add
            {
                this.reader.InputDeviceChanged += value;
            }
            remove
            {
                this.reader.InputDeviceChanged -= value;
            }
        }

        public event ReaderDeviceStateChangedEventHandler InputDeviceStateChanged
        {
            add
            {
                this.reader.InputDeviceStateChanged += value;
            }
            remove
            {
                this.reader.InputDeviceStateChanged -= value;
            }
        }

        public event FileLoggerEventHandler LogEvent
        {
            add
            {
                this.reader.LogEvent += value;
            }
            remove
            {
                this.reader.LogEvent -= value;
            }
        }

        public event FileLoggerEventHandler ErrorOccured
        {
            add
            {
                this.reader.ErrorOccured += value;
            }
            remove
            {
                this.reader.ErrorOccured -= value;
            }
        }

        public bool CloseInputDevice()
        {
            return this.reader.CloseInputDevice();
        }

        public bool CloseOutputDevice()
        {
            return this.reader.CloseOutputDevice();
        }

        public bool OpenInputDevice()
        {
            return this.reader.OpenInputDevice();
        }

        public bool OpenOutputDevice()
        {
            return this.reader.OpenOutputDevice();
        }
    }
}
