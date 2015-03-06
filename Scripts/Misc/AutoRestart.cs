/****************************** AutoRestart.cs ********************************
 * Modified by Lokai for Free Ultima Online shards
 *   This completely custom version includes several commands to assist
 *   in managing your restart, whether automated or manual.
/***************************************************************************
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 ***************************************************************************/
using System;
using System.Collections.Generic;
using Server.Commands;

namespace Server.Misc
{
    public class AutoRestart : Timer
    {
        private static readonly RestartType RestartFrequency = RestartType.Daily; // The server restarts daily or weekly on a particular day of the week.
        private static readonly DayOfWeek RestartDay = DayOfWeek.Monday; // IF the server restarts weekly, the day of week is set here.
        private static readonly TimeSpan RestartDelay = TimeSpan.Zero; // Here we set how long to delay the restart once the timer has finished.

        private enum RestartType { Daily, Weekly }
        private static bool m_Enabled = false; // is auto-restarting enabled?
        public static bool Enabled { get { return m_Enabled; } }

        private static TimeSpan RestartTimeOfDay = TimeSpan.FromHours(2.0); // The time of day at which to restart (in Server (UTC) time.)
        private static List<double> WarningDelays;
        private static string RestartMessage = "The server will be restarting for routine maintenance";
        private static bool m_Restarting;
        public static bool Restarting { get { return m_Restarting; } }
        private static DateTime m_RestartDateTime;
        private static DateTime m_NextWarningTime;
        private static int WarningColor = 0x22;

        private static List<bool> WarningDelaysNOTSent;

        private static void ResetWarningDelayBools(bool auto)
        {
            // -------------------- START HERE ----------------------
            // At what time interval(s) (in minutes) should the restart warning be displayed prior to restart?
            // (These numbers should go from Lowest to Highest for best results.)
            // -------------------- START HERE ----------------------
            WarningDelays = new List<double>() { 1.0, 2.0, 5.0, 10.0, 15.0, 20.0, 25.0, 30.0, 45.0 };

			if (auto)
			{
				m_RestartDateTime = DateTime.UtcNow.Date + RestartTimeOfDay;
				if (m_RestartDateTime < DateTime.UtcNow) m_RestartDateTime += TimeSpan.FromDays(1.0);
				if (RestartFrequency == RestartType.Weekly)
				{
					while(m_RestartDateTime.DayOfWeek != RestartDay)
					{
						m_RestartDateTime += TimeSpan.FromDays(1.0);
					}
				}
			}

            WarningDelaysNOTSent = new List<bool>();
            for (int i = 0; i < WarningDelays.Count; i++)
            {
                WarningDelaysNOTSent.Add(getNextWarningTime(WarningDelays[i]) > DateTime.UtcNow);
            }
        }

        public AutoRestart()
            : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
        {
            Priority = TimerPriority.FiveSeconds;
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
                    try { return int.Parse(color); } catch { } // Maybe they typed a number...
                    return Utility.RandomDyedHue();
            }
        }

        private static DateTime getNextWarningTime(double nextDelay)
        {
            return m_RestartDateTime - TimeSpan.FromMinutes(nextDelay);
        }
        public static void Initialize()
        {
            CommandSystem.Register("Restart", AccessLevel.Administrator, Restart_OnCommand);
            CommandSystem.Register("AutoRestartOn", AccessLevel.Administrator, AutoRestartOn_OnCommand);
            CommandSystem.Register("AutoRestartOff", AccessLevel.Administrator, AutoRestartOff_OnCommand);
            CommandSystem.Register("AutoRestartWhen", AccessLevel.Administrator, AutoRestartWhen_OnCommand);
            CommandSystem.Register("AutoRestartTime", AccessLevel.Administrator, AutoRestartTime_OnCommand);
            CommandSystem.Register("AutoRestartColor", AccessLevel.Administrator, AutoRestartColor_OnCommand);
            CommandSystem.Register("AutoRestartText", AccessLevel.Administrator, AutoRestartText_OnCommand);
            CommandSystem.Register("AR-On", AccessLevel.Administrator, AutoRestartOn_OnCommand);
            CommandSystem.Register("AR-Off", AccessLevel.Administrator, AutoRestartOff_OnCommand);
            CommandSystem.Register("AR-When", AccessLevel.Administrator, AutoRestartWhen_OnCommand);
            CommandSystem.Register("AR-Time", AccessLevel.Administrator, AutoRestartTime_OnCommand);
            CommandSystem.Register("AR-Color", AccessLevel.Administrator, AutoRestartColor_OnCommand);
            CommandSystem.Register("AR-Text", AccessLevel.Administrator, AutoRestartText_OnCommand);
            ResetWarningDelayBools(true);
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
                e.Mobile.SendMessage("Restart time set to {0} {1}.", m_RestartDateTime.ToShortTimeString(), RestartFrequency == RestartType.Daily ? "Daily" : RestartDay.ToString());
                ResetWarningDelayBools(true);
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
            e.Mobile.SendMessage("AutoRestart is {0}scheduled for {1} {2}.",
                Enabled ? "" : "DISABLED, but if enabled would be ", m_RestartDateTime.ToShortTimeString(), RestartFrequency == RestartType.Daily ? "Daily" : RestartDay.ToString());
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
				ResetWarningDelayBools(false);
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