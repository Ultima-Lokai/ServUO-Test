using System;
using System.Collections.Generic;
using Server.Commands;

namespace Server.Misc
{
    public class AutoRestart : Timer
    {
        private static bool m_Enabled = false; // is auto-restarting enabled?

        public static bool Enabled { get { return m_Enabled; } }

        private static TimeSpan RestartTimeOfDay = TimeSpan.FromHours(2.0); // time of day at which to restart (in Server (UTC) time.)
        private static readonly TimeSpan RestartDelay = TimeSpan.Zero; // how long to delay restart once the timer has finished.

        private static List<double> WarningDelays;

        private static string RestartMessage = "The server will be restarting for routine maintenance";

        private static bool m_Restarting;
        private static DateTime m_RestartDateTime;
        private static DateTime m_NextWarningTime;
        private static int WarningColor = 0x22;

        private static List<bool> WarningDelaysNOTSent;

        private static void ResetWarningDelayBools()
        {
            // -------------------- START HERE ----------------------
            // At what time interval(s) (in minutes) should the restart warning be displayed prior to restart?
            // (These numbers should go from Lowest to Highest for best results.)
            // -------------------- START HERE ----------------------
            WarningDelays = new List<double>() { 1.0, 2.0, 5.0, 10.0, 15.0, 20.0, 25.0, 30.0, 45.0 };

            m_RestartDateTime = DateTime.UtcNow.Date + RestartTimeOfDay;
            if (m_RestartDateTime < DateTime.UtcNow) m_RestartDateTime += TimeSpan.FromDays(1.0);

            WarningDelaysNOTSent = new List<bool>();
            for (int i = 0; i < WarningDelays.Count; i++)
            {
                WarningDelaysNOTSent.Add(getNextWarningTime(WarningDelays[i]) > DateTime.UtcNow);
            }
        }

        public AutoRestart()
            : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
        {
            this.Priority = TimerPriority.FiveSeconds;
        }

        private static int getWarningColor(string color)
        {
            switch (color)
            {
                case "red":
                    return Utility.RandomRedHue();
                case "pink":
                    return Utility.RandomPinkHue();
                case "blue":
                    return Utility.RandomBlueHue();
                case "yellow":
                    return Utility.RandomYellowHue();
                case "green":
                    return Utility.RandomGreenHue();
                case "orange":
                    return Utility.RandomOrangeHue();
                case "brown":
                    return Utility.RandomBirdHue();
                default:
                    return Utility.RandomDyedHue();
            }
        }

        private static DateTime getNextWarningTime(double nextDelay)
        {
            return m_RestartDateTime - TimeSpan.FromMinutes(nextDelay);
        }

        public static bool Restarting
        {
            get
            {
                return m_Restarting;
            }
        }
        public static void Initialize()
        {
            CommandSystem.Register("Restart", AccessLevel.Administrator, new CommandEventHandler(Restart_OnCommand));
            CommandSystem.Register("AutoRestartOn", AccessLevel.Administrator, new CommandEventHandler(AutoRestartOn_OnCommand));
            CommandSystem.Register("AutoRestartOff", AccessLevel.Administrator, new CommandEventHandler(AutoRestartOff_OnCommand));
            CommandSystem.Register("AutoRestartWhen", AccessLevel.Administrator, new CommandEventHandler(AutoRestartWhen_OnCommand));
            CommandSystem.Register("AutoRestartTime", AccessLevel.Administrator, new CommandEventHandler(AutoRestartTime_OnCommand));
            CommandSystem.Register("AutoRestartColor", AccessLevel.Administrator, new CommandEventHandler(AutoRestartColor_OnCommand));
            CommandSystem.Register("AutoRestartText", AccessLevel.Administrator, new CommandEventHandler(AutoRestartText_OnCommand));
            CommandSystem.Register("AR-On", AccessLevel.Administrator, new CommandEventHandler(AutoRestartOn_OnCommand));
            CommandSystem.Register("AR-Off", AccessLevel.Administrator, new CommandEventHandler(AutoRestartOff_OnCommand));
            CommandSystem.Register("AR-When", AccessLevel.Administrator, new CommandEventHandler(AutoRestartWhen_OnCommand));
            CommandSystem.Register("AR-Time", AccessLevel.Administrator, new CommandEventHandler(AutoRestartTime_OnCommand));
            CommandSystem.Register("AR-Color", AccessLevel.Administrator, new CommandEventHandler(AutoRestartColor_OnCommand));
            CommandSystem.Register("AR-Text", AccessLevel.Administrator, new CommandEventHandler(AutoRestartText_OnCommand));
            ResetWarningDelayBools();
            new AutoRestart().Start();
        }

        [Usage("AutoRestartText")]
        [Description("Sets the text for AutoRestart warning messages.")]
        private static void AutoRestartText_OnCommand(CommandEventArgs e)
        {
            try
            {
                RestartMessage = e.ArgString;
                e.Mobile.SendAsciiMessage(WarningColor, "New Restart Message: {0}.", RestartMessage);
            }
            catch
            {
                e.Mobile.SendMessage("Usage: AR-Text <string> ((  ex:  [AR-Text The Shard will be restarting for a major update  ))");
            }
        }

        [Usage("AutoRestartColor")]
        [Description("Sets the color for AutoRestart warning messages.")]
        private static void AutoRestartColor_OnCommand(CommandEventArgs e)
        {
            try
            {
                WarningColor = getWarningColor(e.Arguments[0].ToLower());
                e.Mobile.SendAsciiMessage(WarningColor, "This is the new Warning Color.");
            }
            catch
            {
                e.Mobile.SendMessage("Usage: AR-Color <name of color> ((  ex:  [AR-Color Blue  ))");
            }
        }

        [Usage("AutoRestartTime")]
        [Description("Sets the time of day for the AutoRestart feature.")]
        private static void AutoRestartTime_OnCommand(CommandEventArgs e)
        {
            try
            {
                RestartTimeOfDay = TimeSpan.FromHours(double.Parse(e.Arguments[0]));
                m_RestartDateTime = DateTime.UtcNow.Date + RestartTimeOfDay;
                e.Mobile.SendMessage("Restart time set to {0}.", m_RestartDateTime.TimeOfDay);
                ResetWarningDelayBools();
            }
            catch
            {
                e.Mobile.SendMessage("Usage: ARTime <int> ((int is the hour of the day to restart))");
            }
        }

        [Usage("AutoRestartWhen")]
        [Description("Displays the time of day set for AutoRestart.")]
        private static void AutoRestartWhen_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendMessage("AutoRestart is {0}scheduled for {1}.",
                Enabled ? "" : "DISABLED, but if enabled would be ", m_RestartDateTime.TimeOfDay);
        }

        [Usage("AutoRestartOff")]
        [Description("Disables the AutoRestart feature.")]
        private static void AutoRestartOff_OnCommand(CommandEventArgs e)
        {
            m_Enabled = false;
            e.Mobile.SendMessage("AutoRestart is now DISABLED.");
        }

        [Usage("AutoRestartOn")]
        [Description("Enables the AutoRestart feature.")]
        private static void AutoRestartOn_OnCommand(CommandEventArgs e)
        {
            m_Enabled = true;
            e.Mobile.SendMessage("AutoRestart is now ENABLED.");
        }

        [Usage("Restart [x (integer)]")]
        [Description("Restarts the Server now [or in ~x~ minutes].")]
        public static void Restart_OnCommand(CommandEventArgs e)
        {
            double minutes = 0.0;
            try { minutes = double.Parse(e.Arguments[0]); } catch { }
            if (m_Restarting)
            {
                e.Mobile.SendMessage("The server is already restarting.");
            }
            else
            {
                e.Mobile.SendMessage("You have initiated a server restart{0}.",
                    minutes > 0 ? string.Format(" for {0} minutes from now.", (int) minutes) : "");
                m_Enabled = true;
                m_RestartDateTime = DateTime.UtcNow + TimeSpan.FromMinutes(minutes);
            }
        }

        protected override void OnTick()
        {
            if (m_Restarting || !Enabled)
                return;

            int loopCounter = 0;
            foreach (double delay in WarningDelays)
            {
                if (WarningDelaysNOTSent[loopCounter] && DateTime.UtcNow > getNextWarningTime(delay))
                {
                    WarningDelaysNOTSent[loopCounter] = false;
                    Warning_Callback((int) delay);
                    return;
                }
                loopCounter++;
            }

            if (DateTime.UtcNow < m_RestartDateTime)
                return;

            AutoSave.Save();

            m_Restarting = true;

            Timer.DelayCall(RestartDelay, new TimerCallback(Restart_Callback));
        }

        private void Warning_Callback(int time)
        {
            World.Broadcast(WarningColor, true, "{0} in {1} minutes.", RestartMessage, time);
        }

        private void Restart_Callback()
        {
            Core.Kill(true);
        }
    }
}