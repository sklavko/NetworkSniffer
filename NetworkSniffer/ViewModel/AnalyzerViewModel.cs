﻿using GalaSoft.MvvmLight;
using NetworkSniffer.Model;
using System;
using System.Collections.ObjectModel;


namespace NetworkSniffer.ViewModel
{
    public class AnalyzerViewModel : ViewModelBase
    {
        public AnalyzerViewModel()
        {
            PacketLengthStats = StatsHandler.PacketLengthStats;

            TransportProtocolStats = StatsHandler.TransportProtocolStats;

            StatsHandler.Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CapturingTime = (e.SignalTime - StatsHandler.CaptureStartTime).ToString().Substring(0, 12);
            PacketsTotal = StatsHandler.PacketsTotal;
            BytesTotal = StatsHandler.BytesTotal;
            if ((e.SignalTime - StatsHandler.CaptureStartTime).Seconds != 0)
            {
                AveragePPS = Math.Round((double)PacketsTotal / (e.SignalTime - StatsHandler.CaptureStartTime).Seconds, 3);
                AverageBPS = BytesTotal / (e.SignalTime - StatsHandler.CaptureStartTime).Seconds;
            }
        }

        public ObservableCollection<PacketLengthCategory> PacketLengthStats { get; private set; }

        public ObservableCollection<TransportProtocolCategory> TransportProtocolStats { get; private set; }

        public string capturingTime;
        public string CapturingTime
        {
            get
            {
                return capturingTime;
            }
            set
            {
                capturingTime = value;
                RaisePropertyChanged("CapturingTime");
            }
        }

        private int packetsTotal;
        public int PacketsTotal
        {
            get
            {
                return packetsTotal;
            }
            set
            {
                packetsTotal = value;
                RaisePropertyChanged("PacketsTotal");
            }
        }

        private int bytesTotal;
        public int BytesTotal
        {
            get
            {
                return bytesTotal;
            }
            set
            {
                bytesTotal = value;
                RaisePropertyChanged("BytesTotal");
            }
        }

        private double averagePPS;
        public double AveragePPS
        {
            get
            {
                return averagePPS;
            }
            set
            {
                averagePPS = value;
                RaisePropertyChanged("AveragePPS");
            }
        }

        private int averageBPS;
        public int AverageBPS
        {
            get
            {
                return averageBPS;
            }
            set
            {
                averageBPS = value;
                RaisePropertyChanged("AverageBPS");
            }
        }
    }
}
