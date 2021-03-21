using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Collections;

namespace Certifport
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        int n = 0;
        int numprojet = 0;
        string projet = "projet";
        string[] tableQst;
        String data = "";
        int second = 00;
        int min = 00;
        int h = 50;
        string[] word = "projet1.docx|projet2.docx|projet3.docx|projet4.docx|projet5.docx".Split('|');

        

        private void Form2_Load(object sender, EventArgs e)
        {
           
            OpenMicrosoftWord(@"ficher\word\" + word[numprojet]);
            btn6.Visible = true;
            lbl1.Text = "Projet 1/5";
            loadQst();
            tableQst = data.Split('|');
            txtqst.Text = tableQst[0];
            numprojet++;
            second = 00;
            min = 00;
            h = 50;
        }

        private void Reponse(object sender, EventArgs e)
        {
            string reponse = @"ficher\video\2P" + (numprojet) + "Qst" + (n + 1);
            Process.Start(reponse + ".mp4");
        }

        private void LesTAcheSuivant(object sender, EventArgs e)
        {
            Button b = (Button)sender;


            if (b.Text == "Marquer comme terminee")
            {
                if (n == 0) { btn1.BackColor = Color.Gray; pbtn1.Visible = true; btn2.BackColor = Color.Orange; }
                if (n == 1) { btn2.BackColor = Color.Gray; pbtn2.Visible = true; btn3.BackColor = Color.Orange; }
                if (n == 2) { btn3.BackColor = Color.Gray; pbtn3.Visible = true; btn4.BackColor = Color.Orange; }
                if (n == 3) { btn4.BackColor = Color.Gray; pbtn4.Visible = true; btn5.BackColor = Color.Orange; }
                if (n == 4 && numprojet!=5) { btn5.BackColor = Color.Gray; pbtn5.Visible = true; btn6.BackColor = Color.Orange; }
                if (n == 5 && numprojet < 2) { btn6.BackColor = Color.Gray; pbtn6.Visible = true; }
                if (n == 4 && numprojet == 5) { pbtn5.Visible = false; }

                n++;
                if (n > 5) { n = 5; }
            }
            if (b.Text == "Tache precedente")
            {
                if (n == 0) { btn1.BackColor = Color.Blue; pbtn1.Visible = false; }
                if (n == 1) { btn2.BackColor = Color.Blue; pbtn2.Visible = false; btn1.BackColor = Color.Orange; pbtn1.Visible = false;}
                if (n == 2) { btn3.BackColor = Color.Blue; pbtn3.Visible = false; btn2.BackColor = Color.Orange; pbtn2.Visible = false;}
                if (n == 3) { btn4.BackColor = Color.Blue; pbtn4.Visible = false; btn3.BackColor = Color.Orange; pbtn3.Visible = false;}
                if (n == 4) { btn5.BackColor = Color.Blue; pbtn5.Visible = false; btn4.BackColor = Color.Orange; pbtn4.Visible = false;}
                if (n == 5) { btn6.BackColor = Color.Blue; pbtn6.Visible = false; btn5.BackColor = Color.Orange; pbtn5.Visible = false;}
                n--;
                if (n < 0) { n = 0; }
            }


            tableQst = data.Split('|');
            if (tableQst.Length>n) { 
             txtqst.Text = tableQst[n];
            }


        }

        public static void OpenMicrosoftWord(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = filePath;
            Process.Start(startInfo);
        }

        public void loadQst()

        {
            Inetialiselesbtn();


            projet = "projet" + numprojet;
            data = "";


          //  MessageBox.Show(@"ficher\QSt\2" + projet + ".txt");
            var lines = File.ReadAllLines(@"ficher\QSt\2" + projet + ".txt");
            try
            {
                StreamReader sr = new StreamReader(@"ficher\QSt\2" + projet + ".txt");

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

        private void next_projet(object sender, EventArgs e)
        {

            btn6.Visible = false;
           
            if (numprojet == word.Length) 
            {
                if (MessageBox.Show("Est ce que tu veux Sortir !", "Vous avez terminé la version 2", MessageBoxButtons.YesNo)==DialogResult.Yes){ Application.Exit();return; }
                else
                {
                    numprojet = 0;
                    n = 0;
                    OpenMicrosoftWord(@"ficher\word\" + word[numprojet]);
                    btn6.Visible = true;
                    lbl1.Text = "Projet 1/5";
                    loadQst();
                    tableQst = data.Split('|');
                    txtqst.Text = tableQst[0];
                    numprojet++;
                    btn5.Visible = true;
                    return;
                }
            }
            OpenMicrosoftWord(@"ficher\word\" + word[numprojet]);

            lbl1.Text = "Projet " + (numprojet + 1) + "/5";

            Inetialiselesbtn();

            loadQst();

            tableQst = data.Split('|');
            txtqst.Text = tableQst[n];
           
            numprojet++;
            if (numprojet == 5) { btn5.Visible = false; pbtn5.Visible = false; }
        }

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





        private void button2_Click    (object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            second = 00;
            min = 00;
            h = 50;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
