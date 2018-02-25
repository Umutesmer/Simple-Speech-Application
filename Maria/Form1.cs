using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Maria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            sestanima_ayarlari();
        }
        SpeechRecognitionEngine recoEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer speechSyn = new SpeechSynthesizer();
        bool izin;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            izin = true;
            recoEngine.RecognizeAsync();
        }

        void sestanima_ayarlari()
        {
            string[] ihtimaller = { "Hello", "Please open League Of Legends" };
            Choices seçenekler = new Choices(ihtimaller);
            Grammar grammer = new Grammar(new GrammarBuilder(seçenekler));
            recoEngine.LoadGrammar(grammer);
            recoEngine.SetInputToDefaultAudioDevice();
            recoEngine.SpeechRecognized += ses_tanıdığında;
        }

        private void ses_tanıdığında(object sender, SpeechRecognizedEventArgs e)
        {
            speechSyn.SelectVoiceByHints(VoiceGender.Female);
            if (izin == true)
            {
                pictureBox1.Visible = true;
                izin = false;
                if (e.Result.Text == "Please open League Of Legends")
                {
                    System.Diagnostics.Process.Start("C://Riot Games//League of Legends//lol.launcher.exe");
                    if (radioButton1.Checked == true)
                    {
                        speechSyn.SpeakAsync("Okey. I open League of legends");
                    }
                    if (radioButton2.Checked == true)
                    {
                        webBrowser1.Document.GetElementById("source").InnerText = "Tamam. Oyunu başlatıyorum.";
                        timer1.Start();
                    }
                }
                if (e.Result.Text == "Hello")
                {
                    if (radioButton1.Checked == true)
                    {
                        speechSyn.SpeakAsync("Hello");
                    }
                    if (radioButton2.Checked == true)
                    {
                        webBrowser1.Document.GetElementById("source").InnerText = "Selam, hoşgeldin!";
                        timer1.Start();
                    }
                }
            }
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            label1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
        }

        private void konulToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            label1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            webBrowser1.Document.GetElementById("gt-src-listen").InvokeMember("click");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
