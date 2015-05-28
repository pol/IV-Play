#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using Timer = System.Windows.Forms.Timer;

#endregion

namespace IV_Play.Forms
{
    /// <summary>
    /// Interaction logic for ProgressWPF.xaml
    /// </summary>
    public partial class ProgressWPF : Window
    {
        private BackgroundWorker _bgWorker = new BackgroundWorker();
        private bool cancelClosing = true;
        private int i;
        private Timer _timer = new Timer();

        public ProgressWPF()
        {
            InitializeComponent();
            _timer.Tick += TimerTick;
            _timer.Interval = 1000;
            _bgWorker.DoWork += BgWorkerDoWork;
            _bgWorker.RunWorkerCompleted += BgWorkerRunWorkerCompleted;
            _lblTime.Content = string.Format("Elapsed time: {0} Seconds", i);
            _lblProgress.Content = "Building XML data from MAME";
        }

        private void BgWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            _lblProgress.Dispatcher.Invoke(new ThreadStart(() => _lblProgress.Content = "Building XML data from MAME"));
            XmlParser.MakeDat();

            _lblProgress.Dispatcher.Invoke(new ThreadStart(() => _lblProgress.Content = "Generating Game list"));
            XmlParser.ReadDat();
        }

        /// <summary>
        /// Update the Elapsed time counter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            _lblTime.Content = string.Format("Elapsed time: {0} Seconds", ++i);
        }

        private void BgWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _timer.Stop();
            cancelClosing = false; //allows closing of the form.
            Close();
        }

        /// <summary>
        /// Starts building the cache.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WindowState = WindowState.Normal;

            Activate();
            
            TaskbarItemInfo = new TaskbarItemInfo();
             TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
            _timer.Start();
            _bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// This prevents the user from closing the form while refreshing the cache.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = cancelClosing;
        }
    }
}