namespace NewSpecialEvent
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using Common;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Dao.ResultCtx;
    using NewSpecialEvent.DataGrids;
    using NewSpecialEvent.Logic;
    using NewSpecialEvent.Models;
    using NewSpecialEvent.Printing;
    using SimpleInjector;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using SPORTident;
    using SPORTident.Common;
    using SPORTident.Communication;
    using SPORTident.Communication.UsbDevice;

    /// <summary>
    /// The main Form of the application
    /// </summary>
    public partial class Form1 : Form
    {
        private IReader reader;

        private bool _connected;
        private Dictionary<int, DeviceInfo> _deviceInfoList;

        /// <summary>
        /// PrintEngine to print documents.
        /// </summary>
        private static PrintEngine printEngine = new PrintEngine();

        /// <summary>
        /// Name of the printer to use.
        /// </summary>
        private string printerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1(Container container, IReader reader)
        {
            this.SimpleInjectorContainer = container;
            this.reader = reader;
            this.InitializeComponent();
        }

        private Container SimpleInjectorContainer { get; set; }

        /// <summary>
        /// Starts the printing.
        /// </summary>
        private static void DoPrint()
        {
            Form1.printEngine.Print();
        }

        /// <summary>
        /// Called when the form is loaded.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The eventArgs</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.btnOpen.Enabled = false;

            using (this.SimpleInjectorContainer.BeginExecutionContextScope())
            {
                var resultSeeder = this.SimpleInjectorContainer.GetInstance<IDatabaseInitializer<ResultContext>>();
                var resultDao = this.SimpleInjectorContainer.GetInstance<IResultDataAccess>();
                try
                {
                    resultSeeder.InitializeDatabase(resultDao.Context);
                } catch(Exception ex)
                {

                }
            }

            reader.InputDeviceChanged += _reader_InputDeviceChanged;
            reader.InputDeviceStateChanged += _reader_InputDeviceStateChanged;
            reader.LogEvent += _reader_LogEvent;
            reader.ErrorOccured += _reader_ErrorOccured;
            reader.CardRead += _reader_CardRead;
            reader.DeviceConfigurationRead += _reader_DeviceConfigurationRead;

            var newDevice = new ReaderDeviceInfo(ReaderDeviceType.None);
            reader.OutputDevice = newDevice;

            this.RefreshDeviceList();

            this.btnOpen.InvokeIfRequired(() =>
            {
                this.btnOpen.BackColor = Color.Red;
            });
        }

        /// <summary>
        /// Called when Set Zero Time is clicked.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The eventArgs</param>
        private void ZeroBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // #if DEBUG
                // Constants.ZeroTime = DateTime.Now.AddMinutes(-5);
                // #else
                Constants.ZeroTime = DateTime.Parse(this.zeroTextBox.Text);

                // #endif
            }
            catch (Exception)
            {
                this.infoTextBox.InvokeIfRequired(() =>
                {
                    this.infoTextBox.Text = "Zero time is not Set. Date format example: 2014/05/13 12:00:00\n";
                });     
            }

            this.infoTextBox.InvokeIfRequired(() =>
            {
                this.infoTextBox.Text = "Zero time is set: " + Constants.ZeroTime.ToString() + "\n" + this.infoTextBox.Text;
            });

            Constants.ZeroTimeIsSet = true;

            this.btnOpen.InvokeIfRequired(() =>
            {
                this.btnOpen.Enabled = true;
            });
        }

        /// <summary>
        /// Called when a runner is not found by SI Card number.
        /// </summary>
        /// <param name="cardNumber">The card that was not found.</param>
        private void RunnerNotFound(object o, long cardNumber)
        {
            var text = string.Format(CultureInfo.CurrentCulture, "Runner with SI Card Number: {0} is not found in the database.\n", cardNumber);
            this.UpdateInfoTextBox(text);
        }

        private void OnFinishTimeMissing(object i, EventArgs e)
        {
            var text = string.Format(CultureInfo.CurrentCulture, "---------------FINISH PUNCH MISSING FOR THIS RESULT.----------------\n");
            this.UpdateInfoTextBox(text);
        }

        /// <summary>
        /// Called when new computed result is available.
        /// </summary>
        /// <param name="resultId">The new computed resultId</param>
        private void NewResultAvailable(SportidentCard card)
        {
            using (this.SimpleInjectorContainer.BeginExecutionContextScope())
            {
                var cardHandlerService = this.SimpleInjectorContainer.GetInstance<INewCardHandlerService>();
                cardHandlerService.RunnerNotFound += this.RunnerNotFound;
                cardHandlerService.ResultError += this.btnOpen_Click;
                cardHandlerService.FinishPunchMissing += this.OnFinishTimeMissing;

                var processedResult = cardHandlerService.HandleNewCard(card);

                if (processedResult != null)
                {
                    this.UpdateTextBoxes(processedResult);

                    if (this.printEnabled.Checked)
                    {
                        this.AddToPrintEngine(processedResult);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a text to the main info screen (textbox)
        /// </summary>
        /// <param name="text">The text to display</param>
        private void UpdateInfoTextBox(string text)
        {
            this.infoTextBox.InvokeIfRequired(() =>
            {
                this.infoTextBox.Text = text + this.infoTextBox.Text;
            });
        }

        /// <summary>
        /// Updates all textboxes on the form with the result data
        /// </summary>
        /// <param name="result">The result.</param>
        private void UpdateTextBoxes(Result result)
        {
            Color backgroundColor = Color.White;
            if (result.Error != null)
            {
                backgroundColor = Color.Red;
            }

            this.UpdateTextBox(result.Runner.Name, this.nameTextBox, backgroundColor);
            this.UpdateTextBox(result.Runner.Team.Name, this.teamTextBox, backgroundColor);
            this.UpdateTextBox(result.Course.Name, this.courseTextBox, backgroundColor);
            this.UpdateTextBox(result.TimeStr, this.timeTextBox, backgroundColor);
            this.UpdateTextBox(result.FinishTime.ToString(), this.finishtimeTextBox, backgroundColor);
            this.UpdateTextBox(result.Runner.Team.Category, this.categoryTextBox, backgroundColor);

            var text = "[" + DateTime.Now + "] New Result: " + result.Runner.Name + " Team: " + result.Runner.Team.Name + " Time: " + result.TimeStr + " Course: " + result.Course.Name + "\n";
            if (!string.IsNullOrEmpty(result.ErrorText))
            {
                text += result.ErrorText + "\n";
            }

            this.UpdateInfoTextBox(text);
        }

        /// <summary>
        /// Updates a given textbox.
        /// </summary>
        /// <param name="text">The new text</param>
        /// <param name="txtBox">The textbox to update.</param>
        /// <param name="color">The background color.</param>
        private void UpdateTextBox(string text, TextBox txtBox, Color color)
        {
            txtBox.InvokeIfRequired(() =>
            {
                txtBox.Text = text;
                txtBox.BackColor = color;
            });
        }

        /// <summary>
        /// Called when the Entries button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void EntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entries entries = new Entries();
            entries.Show();
        }

        /// <summary>
        /// Called when the Teams button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void TeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Teams teams = new Teams();
            teams.Show();
        }

        /// <summary>
        /// Called when the Printer Settings button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PrinterSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.printDialog.ShowDialog() == DialogResult.OK)
            {
                this.printerName = this.printDialog.PrinterSettings.PrinterName;
                this.UpdateInfoTextBox("Selected printer is: " + this.printerName + "\n");
                Form1.printEngine.PrinterSettings.PrinterName = this.printerName;
            }
        }

        /// <summary>
        /// Called when the Courses button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Courses courses = new Courses();
            courses.Show();
        }

        /// <summary>
        /// Adds a new Result to the print engine
        /// </summary>
        /// <param name="result">The Result to add</param>
        private void AddToPrintEngine(IPrintable result)
        {
            if (result != null)
            {
                printEngine.AddPrintObject(result);

                this.printLabel.InvokeIfRequired(() =>
                {
                    this.printLabel.Text = (int.Parse(this.printLabel.Text, CultureInfo.InvariantCulture) + 1).ToString(CultureInfo.InvariantCulture);
                });
                
                if (int.Parse(this.printLabel.Text, CultureInfo.InvariantCulture) == 3)
                {
                    this.printLabel.InvokeIfRequired(() =>
                    {
                        this.printLabel.Text = "0";
                    });
                }
            }
        }

        /// <summary>
        /// Called when the Print Result button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void PrintResultBtn_Click(object sender, EventArgs e)
        {
            Form1.DoPrint();
            this.printLabel.InvokeIfRequired(() =>
            {
                this.printLabel.Text = "0";
            });
        }

        /// <summary>
        /// Called when the Read Courses button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void CourseReaderBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.courseFileTextBox.Text))
            {
                MessageBox.Show("No course file is specified!");
                return;
            }

            using (this.SimpleInjectorContainer.BeginExecutionContextScope())
            {
                var courseParser = this.SimpleInjectorContainer.GetInstance<ICourseParser>();
                courseParser.Path = this.courseFileTextBox.Text;
                var list = courseParser.ReadCourseFile().ToList();
                list.ForEach(s => this.UpdateInfoTextBox(s));
                this.UpdateInfoTextBox(string.Format(CultureInfo.CurrentCulture, "{0} courses has been added\n!", list.Count));
            }
        }

        /// <summary>
        /// Called when the Browse (Courses) button is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void CourseBrowseBtn_Click(object sender, EventArgs e)
        {
            if (this.openCourseDialog.ShowDialog() == DialogResult.OK)
            {
                this.courseFileTextBox.Text = this.openCourseDialog.FileName;
            }
        }

        /// <summary>
        /// Called when the Results By Category button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void ResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResultsViewer rs = new ResultsViewer();
            rs.Show();
        }

        /// <summary>
        /// Called when Results By Course button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs.</param>
        private void ResultsByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CourseResultsViewer rs = new CourseResultsViewer();
            rs.Show();
        }

        private void DeviceListRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshDeviceList();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.ConnectDisconnect();
        }

        private void RefreshDeviceList()
        {
            cmbSerialPort.Items.Clear();

            _deviceInfoList = new Dictionary<int, DeviceInfo>();

            var devList = DeviceInfo.GetAvailableDeviceList(true, (int)DeviceType.Serial | (int)DeviceType.UsbHid);

            var n = 0;
            DeviceInfo addItem;

            foreach (var item in devList)
            {
                addItem = item;

                cmbSerialPort.Items.Add(DeviceInfo.GetPrettyDeviceName(item));

                _deviceInfoList.Add(n++, addItem);
            }

            if (cmbSerialPort.Items.Count > 0) cmbSerialPort.SelectedIndex = 0;
        }

        private void ConnectDisconnect()
        {
            //if connected -> disconnect or vice versa
            if (_connected)
            {
                _closeDevices();

                this.zeroBtn.InvokeIfRequired(() =>
                {
                    this.zeroBtn.Enabled = true;
                });

                this.grpInputDevice.InvokeIfRequired(() =>
                {
                    this.grpInputDevice.Enabled = true;
                });

                this.btnOpen.InvokeIfRequired(() =>
                {
                    this.btnOpen.BackColor = Color.Red;
                });
            }
            else
            {
                _closeDevices();
                //set input and output device
                if (!_setInputDevice()) return;

                //open input and output device
                try
                {
                    reader.OpenInputDevice();
                    reader.OpenOutputDevice();
                }
                catch (Exception)
                {
                    MessageBox.Show("Could not open the input device, please check device configuration.",
                        "ReaderDemoProject", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                _connected = true;
                this.zeroBtn.InvokeIfRequired(() =>
                {
                    this.zeroBtn.Enabled = false;
                });

                this.grpInputDevice.InvokeIfRequired(() =>
                {
                    this.grpInputDevice.Enabled = false;
                });

                this.btnOpen.InvokeIfRequired(() =>
                {
                    this.btnOpen.BackColor = Color.Green;
                });
            }
        }

        /// <summary>
        /// Close input and output devices for Reader
        /// </summary>
        private void _closeDevices()
        {
            if (reader.InputDeviceIsOpen)
            {
                reader.CloseInputDevice();
            }

            if (reader.OutputDeviceIsOpen)
            {
                reader.CloseOutputDevice();
            }

            _connected = false;
        }

        /// <summary>
        /// Sets the input device for the current Reader instance
        /// </summary>
        private bool _setInputDevice()
        {
            ReaderDeviceInfo device = null;

            if (!_deviceInfoList.ContainsKey(cmbSerialPort.SelectedIndex) ||
                !ReaderDeviceInfo.IsDeviceValid(_deviceInfoList[cmbSerialPort.SelectedIndex].DeviceName))
            {
                MessageBox.Show("Could not determine device. Please refresh the device list and retry.",
                    "ReaderDemo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            device = new ReaderDeviceInfo(_deviceInfoList[cmbSerialPort.SelectedIndex], ReaderDeviceType.SiDevice);

            try
            {
                reader.InputDevice = device;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("An exception occured preparing the device {0}: \n\n{1}.",
                                device, ex.Message), "ReaderDemoProject", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void _reader_CardRead(object sender, SportidentDataEventArgs readoutData)
        {
            //handle this event to further process a read out card
            //you will find the card data in the e.Card array that may contain several cards

            if (readoutData == null || readoutData.Cards == null || readoutData.Cards.Count() < 1)
            {
                return;
            }

            var newCard = readoutData.Cards.FirstOrDefault();
            if(newCard == null)
            {
                return;
            }

            this.NewResultAvailable(newCard);
        }

        /// <summary>Handles the event that is thrown when the reader class logs a message (info, warning, error...)</summary>
        private static void _reader_LogEvent(object sender, FileLoggerEventArgs e)
        {

        }

        /// <summary>Handles the event that is thrown when the reader class indicates a state change for the input device</summary>
        private void _reader_InputDeviceStateChanged(object sender, ReaderDeviceStateChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ReaderDeviceStateChangedEventHandler(_reader_InputDeviceStateChanged), sender, e);
                return;
            }

            switch (e.CurrentState)
            {
                case DeviceState.D0Online:
                    //this.UpdateInfoTextBox("INPUT device connected" + Environment.NewLine);
                    break;
                case DeviceState.D5Busy:
                    //this.UpdateInfoTextBox("INPUT device working" + Environment.NewLine);
                    break;
                default:
                    //this.UpdateInfoTextBox("INPUT device disconnected" + Environment.NewLine);
                    break;
            }
        }

        /// <summary>Handles the event that is thrown when the reader class indicates that the input device has changed</summary>
        private void _reader_InputDeviceChanged(object sender, ReaderDeviceChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new ReaderDeviceChangedEventHandler(_reader_InputDeviceChanged), sender, e);
                return;
            }

            var inputSource = string.Empty;

            switch (e.CurrentDevice.ReaderDeviceType)
            {
                case ReaderDeviceType.SiDevice:
                    inputSource = DeviceInfo.GetPrettyDeviceName(e.CurrentDevice);
                    break;
                case ReaderDeviceType.SiLiveSoapService:
                    inputSource = "SPORTident Live SOAP Service";
                    break;
                default:
                    inputSource = e.CurrentDevice.ReaderDeviceType.ToString();
                    break;
            }

            this.UpdateInfoTextBox("INPUT via " + inputSource + Environment.NewLine);
        }

        private void _reader_ErrorOccured(object sender, FileLoggerEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new FileLoggerEventHandler(_reader_ErrorOccured), sender, e);
                return;
            }

            MessageBox.Show(string.Format("{0}\n", e.Message), "ReaderDemoProject", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (!_connected) return;

            _closeDevices();
            this.RefreshDeviceList();
        }

        private void _reader_DeviceConfigurationRead(object sender, StationConfigurationEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new DeviceConfigurationReadEventHandler(_reader_DeviceConfigurationRead), sender, e);
                return;
            }

            if (InvokeRequired)
            {
                Invoke(new DeviceConfigurationReadEventHandler(_reader_DeviceConfigurationRead), sender, e);
                return;
            }

            this.UpdateInfoTextBox("Unknown device" + Environment.NewLine);

            var msg = "no description available";
            var failed = false;
            switch (e.Result)
            {
                case StationConfigurationResult.OperatingModeNotSupported:
                    msg = "The selected operating mode is not supported on the current device.";
                    failed = true;
                    break;
                case StationConfigurationResult.DeviceDoesNotHaveBackup:
                    msg = "The device does not have a backup memory storage.";
                    failed = true;
                    break;
                case StationConfigurationResult.ReadoutMasterBackupNotSupported:
                    msg = "Reading the backup of SI-Master with firmware < FW595 is not supported.";
                    failed = true;
                    break;
            }

            if (failed)
            {
                MessageBox.Show(
                    string.Format(
                        "The device configuration could not be read successfully. Reason is probably: {0} ({1})",
                        e.Result, msg), "ReaderDemo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            switch (e.Device.Product.ProductFamily)
            {
                case ProductFamily.SimSrr:
                    this.UpdateInfoTextBox(string.Format("S/N: {0}, FW: {1}, OpMode: {2}, Protocol: {3}, Channel: {4}",
                        e.Device.SerialNumber, e.Device.FirmwareVersion, e.Device.Product.ProductType,
                        e.Device.SimSrrUseModD3Protocol, e.Device.SimSrrChannel));

                    //check device configuration
                    if (e.Device.SimSrrUseModD3Protocol != 1)
                    {
                        MessageBox.Show(
                            "The device is not configured to use AIR+ protocol. To use extended features it is recommended to enable AIR+ protocol for this device.",
                            "ReaderDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
                case ProductFamily.Bs8SiMaster:
                case ProductFamily.Bsx7:
                case ProductFamily.Bsx8:
                case ProductFamily.Bs11Large:
                case ProductFamily.Bs11Small:
                    this.UpdateInfoTextBox(
                        string.Format("Code no.: {0} (S/N: {1}, FW: {2}), OpMode: {3}, AutoSend: {4}, Legacy prot: {5}",
                            e.Device.CodeNumber, e.Device.SerialNumber, e.Device.FirmwareVersion, e.Device.OperatingMode,
                            e.Device.AutoSendMode, e.Device.LegacyProtocolMode));

                    //check device configuration
                    if (e.Device.OperatingMode != OperatingMode.Readout && !e.Device.AutoSendMode)
                    {
                        MessageBox.Show(
                            "The device is not configured to read cards and has not set autosend flag. No data can be processed from this station.\n\nPlease reconfigure!",
                            "ReaderDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else if (e.Device.LegacyProtocolMode)
                    {
                        MessageBox.Show(
                            "The device is configured to use legacy protocol.\nThis application does not support legacy protocol.\n\nPlease reconfigure!",
                            "ReaderDemo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;
            }
        }

    }
}