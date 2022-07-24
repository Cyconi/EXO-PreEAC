using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;
using EXO;
using VRC.SDKBase;
using static EXO.Modules.Util;


namespace EXO.GUI
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        public Form1()
        {
            InitializeComponent();

            //make the windows transparent
            this.BackColor = System.Drawing.Color.Wheat;
            this.TransparencyKey = System.Drawing.Color.Wheat;

            //make the windows borderless
            this.FormBorderStyle = FormBorderStyle.None;

            //make the windows start in top left corner
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            //make the windows topmost
            this.TopMost = true;

            CheckForIllegalCrossThreadCalls = false;            

            MelonLoader.MelonCoroutines.Start(ShowHideMenu());            
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
        }        
        static bool showing = true;
        internal IEnumerator ShowHideMenu()
        {
            for (; ; )
            {
                if (GetAsyncKeyState(Keys.Insert) < 0 && showing == true) //hide it
                {
                    this.Hide();                    
                    showing = false;
                    yield return new WaitForSeconds(0.05f);
                }
                else if (GetAsyncKeyState(Keys.Home) < 0 && showing == false) //show it 
                {                    
                    this.Show();                    
                    showing = true;
                    yield return new WaitForSeconds(0.05f);
                }
                yield return new WaitForSeconds(0.05f);
            }
        }        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CLog.L("Getting All The Items");
            foreach (VRC_Pickup vrc_Pickup in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
                vrc_Pickup.transform.position = UserUtils.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
            }
            foreach (VRCSDK2.VRC_Pickup vrc_Pickup2 in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup2.gameObject);
                vrc_Pickup2.transform.position = UserUtils.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
            }
            foreach (VRC.SDK3.Components.VRCPickup vrc_Pickup3 in UnityEngine.Object.FindObjectsOfType<VRC.SDK3.Components.VRCPickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup3.gameObject);
                vrc_Pickup3.transform.position = UserUtils.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
            }
            foreach (VRCSDK2.VRC_ObjectSync vrc_PickupSDK in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_ObjectSync>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK.gameObject);
                vrc_PickupSDK.transform.position = UserUtils.GetLocalPlayer().transform.position + new Vector3(0f, 0.1f, 0f);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            CLog.L("Resetting All The Items");
            foreach (VRC_Pickup vrc_Pickup in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
                vrc_Pickup.transform.localPosition = new Vector3(0f, -100000f, 0f);
            }
            foreach (VRCSDK2.VRC_Pickup vrc_Pickup2 in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup2.gameObject);
                vrc_Pickup2.transform.localPosition = new Vector3(0f, -100000f, 0f);
            }
            foreach (VRC.SDK3.Components.VRCPickup vrc_Pickup3 in UnityEngine.Object.FindObjectsOfType<VRC.SDK3.Components.VRCPickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup3.gameObject);
                vrc_Pickup3.transform.localPosition = new Vector3(0f, -100000f, 0f);
            }
            foreach (VRCSDK2.VRC_ObjectSync vrc_PickupSDK in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_ObjectSync>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_PickupSDK.gameObject);
                vrc_PickupSDK.transform.localPosition = new Vector3(0f, -100000f, 0f);
                UserUtils.GetLocalPlayer().transform.position = new Vector3(0f, 100000f, 0f);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Physics.gravity = (true ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                EXO.Modules.Movement.FlyToggle = true;
            }
            else
            {                
                Physics.gravity = (false ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                EXO.Modules.Movement.FlyToggle = false;
            }                
        }        
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)            
                EXO.Modules.Movement.SpeedToggle = true;            
            else
                EXO.Modules.Movement.SpeedToggle = false;
        }       
        private void button3_Click(object sender, EventArgs e)
        {
            CLog.L("Forcing Everyone To Drop There Items");
            foreach (VRC_Pickup vrc_Pickup in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
                vrc_Pickup.Drop();
            }
            foreach (VRC.SDK3.Components.VRCPickup vrcPickup in UnityEngine.Object.FindObjectsOfType<VRC.SDK3.Components.VRCPickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrcPickup.gameObject);
                vrcPickup.Drop();
            }
            foreach (VRCSDK2.VRC_Pickup SDK2vrcPickup in UnityEngine.Object.FindObjectsOfType<VRCSDK2.VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(SDK2vrcPickup.gameObject);
                SDK2vrcPickup.Drop();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                Physics.gravity = (true ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                EXO.Modules.Movement.FastFlyToggle = true;
                EXO.Modules.Movement.NoClipToggle = true;
                EXO.Modules.Movement.NoClip();
            }
            else 
            {
                Physics.gravity = (false ? new Vector3(0f, 0f, 0f) : new Vector3(0f, -9.81f, 0f));
                EXO.Modules.Movement.FastFlyToggle = false;
                EXO.Modules.Movement.NoClipToggle = false;
                EXO.Modules.Movement.NoClip();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
                EXO.Modules.WorldExploits.ItemHide(false);
            else
                EXO.Modules.WorldExploits.ItemHide(true);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
                EXO.Patches.AntiUdon = true;
            else
                EXO.Patches.AntiUdon = false;
        }
    }
}
