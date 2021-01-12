﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WindowsDesktop;

namespace PrivacyTools
{
    public static class PrivacyManager
    {
        public static Timer OverlayTimer = new Timer(10);
        public static Overlay WindowOverlay = new Overlay();
        public static void BlackOverlay1(IntPtr handle)
        {
            OverlayTimer.Elapsed += OverlayTimer_Elapsed;
            if (OverlayTimer.Enabled)
            {
                OverlayTimer.Stop();
            }
            else
            {
                OverlayTimer.Start();
            }
        }

        private static void OverlayTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //get desktop window Graphics object
            Graphics desktop = Graphics.FromHwnd(GetDesktopWindow());
            //use a black SolidBrush
            using (SolidBrush b = new SolidBrush(Color.Black))
            {
                //paint it all
                desktop.FillRectangle(b, desktop.ClipBounds);
            }
        }

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();

        public static void BlackOverlay2(IntPtr handle)
        {
            if (WindowOverlay.Visible)
            {
                WindowOverlay.Enabled = false;
                WindowOverlay.Close();
                WindowOverlay = new Overlay();
            }
            else
            {
                WindowOverlay.Show();
            }
        }

        public static void BlackOverlay3(IntPtr handle)
        {
            BlackOverlay1(handle);
            BlackOverlay2(handle);
        }

        public static void MinimizeAll(IntPtr handle)
        {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);

        const int WM_COMMAND = 0x111;
        const int MIN_ALL = 419;
        const int MIN_ALL_UNDO = 416;

        public static void NewDesktop(IntPtr handle)
        {
            var desktop = VirtualDesktop.Create();
            desktop.Switch();
        }

        public static void Lock(IntPtr handle)
        {
            LockWorkStation();
        }

        [DllImport("user32.dll")]
        public static extern bool LockWorkStation();

        public static void Suspend(IntPtr handle)
        {
            SetSuspendState(false, true, true);
        }

        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public static void Hibernate(IntPtr handle)
        {
            SetSuspendState(true, true, true);
        }

        private static int SC_MONITORPOWER = 0xF170;
        private static int WM_SYSCOMMAND = 0x0112;

        enum MonitorState
        {
            ON = -1,
            OFF = 2,
            STANDBY = 1
        }

        public static void NoVideo(IntPtr handle)
        {
            SendMessage(handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)(int)MonitorState.OFF);
        }

        public static void StandbyVideo(IntPtr handle)
        {
            SendMessage(handle, WM_SYSCOMMAND, (IntPtr)SC_MONITORPOWER, (IntPtr)(int)MonitorState.STANDBY);
        }
    }
}