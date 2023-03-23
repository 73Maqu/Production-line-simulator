using System;
using System.Drawing;
using System.Windows.Forms;

namespace POSK3
{
    public partial class Linia : Form
    {
        private int timerCount = 0;
        private int malftimerCount = 0;
        int awaria;
        int numer;
        int p1;
        int p2;
        Random r = new Random();
        public Linia()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) //timer do awarii
        {
            timerCount++;

            if (timerCount == 5)
            {
                timer2.Stop();
                awaria = r.Next(3) + 1;
                numer = r.Next(3) + 1;
                textBox4.BackColor = Color.Red;

                switch (awaria) //rodzaje awarii
                {
                    case 1:
                        textBox4.Text = ("Awaria chłodzenia - zmniejsz obroty");
                        break;
                    case 2:
                        textBox4.Text = ("Awaria pieca - zmniejsz temperaturę");
                        break;
                    case 3:
                        textBox4.Text = ("Sprwdzenie obecności operatora - naciśnij " + numer + " przycisk");
                        break;
                    default:
                        break;
                }

                if (awaria == 1)
                {
                    timer3.Start();
                    button5.Enabled = true;
                    /*if (p1 < 40)
                    {
                        textBox4.Text = ("");
                        textBox4.BackColor = Color.White;
                        awaria = 0;
                        malftimerCount = 0;
                        timer2.Start();
                        timer3.Stop();
                        timerCount = 0;
                    }*/
                }
                if (awaria == 2)
                {
                    timer3.Start();
                    button4.Enabled = true;
                    /*if (p2 < 500 )
                    {
                        textBox4.Text = ("");
                        textBox4.BackColor = Color.White;
                        awaria = 0;
                        malftimerCount = 0;
                        timer2.Start();
                        timer3.Stop();
                        timerCount = 0;
                    }*/

                }
                if (awaria == 3)
                {
                    timer3.Start();

                    switch (numer)
                    {
                        case 1:
                            button1.Enabled = true;
                            break;
                        case 2:
                            button2.Enabled = true;
                            break;
                        case 3:
                            button3.Enabled = true;
                            break;
                        default: break;
                    }

                }

            }
        }

        private void timer2_Tick(object sender, EventArgs e) //progress bar
        {
            if (trackBar2.Value != 0 && trackBar1.Value != 0)
            {
                timer1.Start();
                if (progressBar1.Value != progressBar1.Maximum)
                {
                    if (trackBar2.Value > 500 && trackBar1.Value < 30)
                    {
                        timer1.Stop();
                        textBox3.Text = "Za duża temperatura - włącz lepsze chłodzenie";
                        timer3.Start();

                        /*   if 
                           {
                               timer1.Start();
                               timer3.Stop();
                               textBox3.Text = "";
                               progressBar1.Step = 3;
                               progressBar1.PerformStep();
                           }*/
                    }
                    else
                    {
                        progressBar1.Step = 1;
                        timer3.Stop();
                        textBox3.Text = "";
                        timer1.Start();
                        progressBar1.PerformStep();
                        malftimerCount = 0;
                    }
                }
                else
                {
                    timer1.Stop();
                    timer2.Stop();
                    textBox3.Text = "Produkcja zakończona pomyślnie";
                    textBox3.BackColor = Color.Green;
                }
            }
            else timer1.Stop();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "Temperatura pieca wynosi " + trackBar2.Value.ToString() + " °C";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = "Prędkość wentylatorów wynosi " + trackBar1.Value.ToString() + " obrotów/sekundę";

        }


        private void timer3_Tick(object sender, EventArgs e) //timer awarii
        {
            p1 = trackBar1.Value;
            p2 = trackBar2.Value;
            malftimerCount++;
            if (malftimerCount == 5)
            {
                timer1.Stop();
                timer2.Stop();
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox3.Text = "Linia uszkodzona, program zostanie wyłączony";
                textBox3.BackColor = Color.Red;
            }
            if (malftimerCount == 10)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = ("");
            textBox4.BackColor = Color.White;
            awaria = 0;
            timerCount = 0;
            timer2.Start();
            timer3.Stop();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            malftimerCount = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = ("");
            textBox4.BackColor = Color.White;
            if (trackBar2.Value >= 100) { trackBar2.Value -= 100; }
            else { trackBar2.Value = 0; }
            awaria = 0;
            timerCount = 0;
            timer2.Start();
            timer3.Stop();
            malftimerCount = 0;
            textBox1.Text = "Temperatura pieca wynosi " + trackBar2.Value.ToString() + " °C";
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox4.Text = ("");
            textBox4.BackColor = Color.White;
            if (trackBar1.Value >= 10) { trackBar1.Value -= 10; }
            else { trackBar1.Value = 0; }
            awaria = 0;
            timerCount = 0;
            timer2.Start();
            timer3.Stop();
            malftimerCount = 0;
            textBox2.Text = "Prędkość wentylatorów wynosi " + trackBar1.Value.ToString() + " obrotów/sekundę";
            button5.Enabled = false;
        }

        private void Linia_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }

}
