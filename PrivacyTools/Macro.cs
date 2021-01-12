using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrivacyTools
{
    class Macro
    {
        public Keys Key;
        public bool Control = false;
        public bool Alt = false;
        public bool Shift = false;
        public VideoAction Action1;
        public PlayerAction Action2;
    }

    public enum VideoAction
    {
        Nothing,
        BlackOverlay1,
        BlackOverlay2,
        BlackOverlay3,
        MinimizeAll,
        NewDesktop,
        Lock,
        Suspend,
        Hibernate,
        NoVideo,
        StandbyVideo
    }

    public enum PlayerAction
    {
        Nothing,
        Pause,
        Mute
    }
}
