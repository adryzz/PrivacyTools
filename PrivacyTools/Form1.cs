using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace PrivacyTools
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook Hook;
        Macro Keybind = new Macro();
        bool AllowExit = false;
        List<Keys> AllKeys = new List<Keys>();
        List<VideoAction> Actions = new List<VideoAction>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllKeys = Enum.GetValues(typeof(Keys)).Cast<Keys>().ToList();
            Actions = Enum.GetValues(typeof(VideoAction)).Cast<VideoAction>().ToList();
            foreach(Keys k in AllKeys)
            {
                comboBox1.Items.Add(k.ToString());
            }

            foreach(VideoAction v in Actions)
            {
                comboBox2.Items.Add(v.ToString());
            }

            if (System.IO.File.Exists("keybind.json"))
            {
                Keybind = Newtonsoft.Json.JsonConvert.DeserializeObject<Macro>(System.IO.File.ReadAllText("keybind.json"));
            }
            else
            {
                System.IO.File.WriteAllText("keybind.json", Newtonsoft.Json.JsonConvert.SerializeObject(Keybind, Newtonsoft.Json.Formatting.Indented));
            }
            Hook = new GlobalKeyboardHook();
            Hook.HookedKeys.Add(Keybind.Key);
            Hook.KeyDown += Hook_KeyDown;
        }

        private void Hook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys.HasFlag(Keys.Control) == Keybind.Control && Control.ModifierKeys.HasFlag(Keys.Alt) == Keybind.Alt && Control.ModifierKeys.HasFlag(Keys.Shift) == Keybind.Shift)
            {
                Invoke(new Execute(ExecuteActions));
            }
        }

        public void ExecuteActions()
        {
            switch (Keybind.Action1)
            {
                case VideoAction.BlackOverlay1:
                    PrivacyManager.BlackOverlay1(Handle);
                    break;
                case VideoAction.BlackOverlay2:
                    PrivacyManager.BlackOverlay2(Handle);
                    break;
                case VideoAction.BlackOverlay3:
                    PrivacyManager.BlackOverlay3(Handle);
                    break;
                case VideoAction.MinimizeAll:
                    PrivacyManager.MinimizeAll(Handle);
                    break;
                case VideoAction.NewDesktop:
                    PrivacyManager.NewDesktop(Handle);
                    break;
                case VideoAction.Lock:
                    PrivacyManager.Lock(Handle);
                    break;
                case VideoAction.Suspend:
                    PrivacyManager.Suspend(Handle);
                    break;
                case VideoAction.Hibernate:
                    PrivacyManager.Hibernate(Handle);
                    break;
                case VideoAction.NoVideo:
                    PrivacyManager.NoVideo(Handle);
                    break;
                case VideoAction.StandbyVideo:
                    PrivacyManager.StandbyVideo(Handle);
                    break;
            }
        }
        private delegate void Execute();

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > 0 && comboBox2.SelectedIndex > 0)
            {
                Keybind.Key = AllKeys[comboBox1.SelectedIndex];
                Keybind.Action1 = Actions[comboBox2.SelectedIndex];
                Keybind.Control = checkBox1.Checked;
                Keybind.Alt = checkBox2.Checked;
                Keybind.Shift = checkBox3.Checked;
                System.IO.File.WriteAllText("keybind.json", Newtonsoft.Json.JsonConvert.SerializeObject(Keybind, Newtonsoft.Json.Formatting.Indented));
                Hook.unhook();
                Hook = new GlobalKeyboardHook();
                Hook.HookedKeys.Add(Keybind.Key);
                Hook.KeyDown += Hook_KeyDown;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !AllowExit;
            Hide();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            Cursor.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllowExit = true;
            Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (Keybind.Action1 != VideoAction.Nothing)
            {
                Hide();
            }
        }
    }
}
