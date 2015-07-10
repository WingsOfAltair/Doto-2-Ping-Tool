using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.Threading;

namespace Doto_2_Ping_Tool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private long[] PingList = new long[9];
        private int Cooldown;
        public MainWindow()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            Thread PingEngine = new Thread(new ThreadStart(PingThread));
            PingEngine.Start();
        }

        private void PingThread()
        {
            while (true)
            {
                // ping all servers
                PingList[0] = PingServer("lux.valve.net");
                PingList[1] = PingServer("146.66.155.1");
                PingList[2] = PingServer("sto.valve.net");
                PingList[3] = PingServer("192.69.96.1");
                PingList[4] = PingServer("208.78.164.1");
                PingList[5] = PingServer("gru.valve.net");
                PingList[6] = PingServer("sgp-1.valve.net");
                PingList[7] = PingServer("syd.valve.net");
                PingList[8] = PingServer("196.38.180.1");
                UpdateUI();
                // sleep ping thread
                Thread.Sleep(Cooldown);
            }
        }

        private void UpdateUI()
        {
            Application.Current.Dispatcher.Invoke(new Action(() => {
                EUW.Content = PingList[0].ToString() + " ms";
                EUE.Content = PingList[1].ToString() + " ms";
                CykaBlyat.Content = PingList[2].ToString() + " ms";
                USW.Content = PingList[3].ToString() + " ms";
                USE.Content = PingList[4].ToString() + " ms";
                SAmerica.Content = PingList[5].ToString() + " ms";
                SEA.Content = PingList[6].ToString() + " ms";
                Australia.Content = PingList[7].ToString() + " ms";
                SAfrica.Content = PingList[8].ToString() + " ms";
            }));
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                Cooldown = 10;
            else if (comboBox1.SelectedIndex == 1)
                Cooldown = 15;
            else if (comboBox1.SelectedIndex == 2)
                Cooldown = 20;
            else if (comboBox1.SelectedIndex == 3)
                Cooldown = 30;
            Cooldown = Cooldown * 1000;
        }

        private long PingServer(string Host)
        {
            Ping pingEngine = new Ping();
            PingReply pingReply = pingEngine.Send(Host);
            return pingReply.RoundtripTime;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
