using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.IO;
using System.Collections;
using System.Security.AccessControl;

namespace Certifport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }
        Form2 settingsForm = new Form2();
        int numprojet = 0;
        string[] tableQst;
        String data = "";
        string[] word = "NorthwindElectric.docx|BellowsCollegeSyllabus.docx|Lesdinosaures.docx|AlpineSkiHouse.docm|AuteursDeRecitsFantastiques.docx".Split('|');


        private void Form1_Load(object sender, EventArgs e)
        { 
            string filePath = @"ficher\word\coupon.docx";
            string dest= @"C:\Users\"+Environment.UserName+@"\Documents\coupon.docx";

            if (File.Exists(dest) ==false) {
                try
                {
                    File.Copy(filePath, dest, true);
                }
                catch (Exception)
                {

                    MessageBox.Show("Lors de la première ouverture du programme, Windows protection  doit être désactivée \n\n   desactiviha w aji mar7ba bik  (●'◡'●) |", "Accès refusé",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    // To show error details
                    
                    Application.Exit();
                    return;
                }
               
            }

            
            OpenMicrosoftWord(@"ficher\word\"  + word[numprojet]);
            btn6.Visible = true;
            lbl1.Text = "Projet 1/5";
            loadQst();
             tableQst = data.Split('|');
            txtqst.Text = tableQst[0];
            numprojet++;
        }

        string projet = "projet";
        private void next_projet(object sender, EventArgs e)
        {
            
            btn6.Visible = false;
            if (numprojet == word.Length)
            {
                if(MessageBox.Show("Voulez-vous ouvrir Version 2 ?", "Version 1 Complet",MessageBoxButtons.YesNoCancel)==DialogResult.Yes){
                    settingsForm.Show();
                    this.Hide();
                } 
                
                return;
            }
               
            OpenMicrosoftWord(@"ficher\word\" + word[numprojet]);
    
            lbl1.Text = "Projet " + (numprojet + 1) + "/5";

            Inetialiselesbtn();

            loadQst();

            tableQst = data.Split('|');
            txtqst.Text = tableQst[n];
            numprojet++;
            //txtqst.Text = tableQst[0]; ;



        }
        int n = 1;
        private void LesTachePass(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            

            if (b.Text == "Marquer comme terminee")
            {
                if (n == 0) { btn1.BackColor = Color.Gray; pbtn1.Visible = true; btn2.BackColor = Color.Orange; }
                if (n == 1) { btn2.BackColor = Color.Gray; pbtn2.Visible = true; btn3.BackColor = Color.Orange; }
                if (n == 2) { btn3.BackColor = Color.Gray; pbtn3.Visible = true; btn4.BackColor = Color.Orange; }
                if (n == 3) { btn4.BackColor = Color.Gray; pbtn4.Visible = true; btn5.BackColor = Color.Orange; }
                if (n == 4) { btn5.BackColor = Color.Gray; pbtn5.Visible = true; btn6.BackColor = Color.Orange; }
                if (n == 5 && numprojet<2) { btn6.BackColor = Color.Gray; pbtn6.Visible = true; }
                n++;
                if (n > 5) { n = 5; }
            }
            if (b.Text == "Tache precedente")
            {
                if (n == 0) { btn1.BackColor = Color.Blue; pbtn1.Visible = false;  }
                if (n == 1) { btn2.BackColor = Color.Blue; pbtn2.Visible = false;  pbtn1.Visible = false; btn1.BackColor = Color.Orange; }
                if (n == 2) { btn3.BackColor = Color.Blue; pbtn3.Visible = false;  pbtn2.Visible = false; btn2.BackColor = Color.Orange; }
                if (n == 3) { btn4.BackColor = Color.Blue; pbtn4.Visible = false;  pbtn3.Visible = false; btn3.BackColor = Color.Orange; }
                if (n == 4) { btn5.BackColor = Color.Blue; pbtn5.Visible = false;  pbtn4.Visible = false; btn4.BackColor = Color.Orange; }
                if (n == 5) { btn6.BackColor = Color.Blue; pbtn6.Visible = false;  pbtn5.Visible = false; btn5.BackColor = Color.Orange; }
                n--;
                if (n < 0) { n = 0; }
            }

            
             tableQst = data.Split('|');
            txtqst.Text = tableQst[n];



        }


     
        
        
      





        private void button1_Click(object sender, EventArgs e)
        {
            string reponse = @"ficher\video\P" + (numprojet)+"Qst"+(n+1);
            Process.Start(reponse+".mp4");

        }

        public static void OpenMicrosoftWord(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = filePath;
            Process.Start(startInfo);
        }

        int second = 00;
        int min = 00;
        int h = 50;

        private void timer1_Tick(object sender, EventArgs e)
        {

            second++;

            lbltime.Text = h + ":" + min;
            if (second == 10)
            {
                min--; second = 0;
            }
            if (min == 0) { h--; min = 59; }
            if (h == 0 && min == 0) { Application.Exit(); }

        }

       

        public void loadQst()

        {
            Inetialiselesbtn();


            projet = "projet"+ numprojet;
            data = "";
            
          
            
            var lines = File.ReadAllLines(@"ficher\QSt\" + projet + ".txt");
            try
            {
                StreamReader sr = new StreamReader(@"ficher\QSt\" + projet + ".txt");

                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    data += line;
                }

                sr.Close();
            }
            catch (Exception x)
            {
                // Let the user know what went wrong.
                MessageBox.Show("The file could not be read:");
                MessageBox.Show(x.Message);
            }
         
        }

        public void Inetialiselesbtn()
        {
            btn1.BackColor = Color.Orange; pbtn1.Visible = false;
            btn2.BackColor = Color.Blue; pbtn2.Visible = false;
            btn3.BackColor = Color.Blue; pbtn3.Visible = false;
            btn4.BackColor = Color.Blue; pbtn4.Visible = false;
            btn5.BackColor = Color.Blue; pbtn5.Visible = false;
            btn6.BackColor = Color.Blue; pbtn6.Visible = false;
           
          
            n = 0;
            txtqst.Text = "";
           
           
        }

       

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            second = 00;
            min = 00;
            h = 50;
        }

        private void lestache_click(object sender, EventArgs e)
        {

        }

        private void Verion2_Pass(object sender, EventArgs e)
        {
            
            // Show the settings form
            settingsForm.Show();
            this.Hide();
            
        }
    }
}
