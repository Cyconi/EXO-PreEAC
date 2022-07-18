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
        internal static bool GUIrun = true;
        static bool showing = true;
        internal IEnumerator ShowHideMenu()
        {
            for (; ; )
            {
                if (GetAsyncKeyState(Keys.Insert) < 0 && showing == true) //than hide it
                {
                    this.Hide();
                    //this.WindowState = FormWindowState.Minimized;                    
                    showing = false;
                    yield return new WaitForSeconds(0.2f);
                }
                else if (GetAsyncKeyState(Keys.Home) < 0 && showing == false) // than show it
                {
                    this.Show();
                    //this.WindowState = FormWindowState.Normal;                    
                    showing = true;
                    yield return new WaitForSeconds(0.2f);
                }
                yield return new WaitForSeconds(0.07f);
                if (!GUIrun)
                    yield break;
            }
        }
        internal IEnumerator ShowGuiBtnApi()
        {
            this.Hide();
            //this.WindowState = FormWindowState.Minimized;                    
            showing = false;            
            yield return new WaitForSeconds(0.07f);
            if (!GUIrun)
                yield break;            
        }
        internal IEnumerator HideGuiBtnApi()
        {
            this.Show();
            //this.WindowState = FormWindowState.Normal;                    
            showing = true;
            yield return new WaitForSeconds(0.07f);
            if (GUIrun)
                yield break;           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
