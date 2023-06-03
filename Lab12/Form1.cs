using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab12
{
    public partial class Form1 : Form
    {
        public Form1() 
        {
            InitializeComponent();
        }

        float[,] Q = new float[3,3];
        double t = 0;
        int st = 0; 
        double alpha = 0;

        private void StartBtn_Click(object sender, EventArgs e)
        {
            if (StartBtn.Text == "Start")
            {
                StartBtn.Text = "Pause";
                Initiate();

                timer1.Start();
            }
            else if (StartBtn.Text == "Pause")
            {
                StartBtn.Text = "Start";
                Initiate();

                timer1.Stop();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWeather(0);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                Q[i, j] = 0;

            st = 0;
            t = 0;
        }

        private void StepBtn_Click(object sender, EventArgs e)
        {
            Initiate();
            Step();
        }

        private void SetWeather(int index)
        {
            if (index == 0)
            {
                LabelPicture.Text = "It's CLEAR :)";
            }
            else if (index == 1)
            {
                LabelPicture.Text = "It's CLOUDY :/";
            }
            else if (index == 2)
            {
                LabelPicture.Text = "It's OVERCAST :(";
            }
        }

        private void Initiate()
        {
            Q[0, 0] = (float)edit1.Value;
            Q[0, 1] = (float)edit2.Value;
            Q[0, 2] = (float)edit3.Value;
            Q[1, 0] = (float)edit4.Value;
            Q[1, 1] = (float)edit5.Value;
            Q[1, 2] = (float)edit6.Value;
            Q[2, 0] = (float)edit7.Value;
            Q[2, 1] = (float)edit8.Value;
            Q[2, 2] = (float)edit9.Value;

			alpha = 0;
        }

        private void Step()
        {
            Random rand = new Random();
            double RandomNumber = rand.NextDouble();
			alpha = Math.Log(RandomNumber) / Q[st,st];
            t += alpha;
            Day.Text = "Day: " + Math.Round(t,1);

            int forRand = (int)(-Q[st, st] * 10);

            int newRand = rand.Next(forRand) + 1; 

            int i = -1;

            while (newRand > 0 && i < 2)
            {
                i++;
                if (i != st)
                {
                    newRand = newRand - (int)(Q[st, i]*10);
                }
            }
            st = i;

            labelWeather.Text = st.ToString();
            SetWeather(st);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Step();
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            Initiate();

            int[] weathers = new int[3];
            for (int i = 0; i < weathers.Length; i++) weathers[i] = 0;

            double T = 0;
            Random rand = new Random();

            for (int i = 0; i < editN.Value; i++)
            {
                while(T < (double)editT.Value)
                {
                    double RandomNumber = rand.NextDouble();
                    alpha = Math.Log(RandomNumber) / Q[st, st];
                    T += alpha;
                    int forRand = (int)(-Q[st, st] * 10);
                    int newRand = rand.Next(forRand) + 1; 

                    int ix = -1;

                    while (newRand > 0 && ix < 2)
                    {
                        ix++;
                        if (ix != st)
                        {
                            newRand = newRand - (int)(Q[st, ix] * 10);
                        }
                    }
                    st = ix;
                }
                weathers[st]++;
                st = 0;
                T = 0;
            }
            int all = weathers[0] + weathers[1] + weathers[2];
            labelClear.Text = "Clear: " + weathers[0] + "; " + Math.Round((float)weathers[0] / (float)(all), 3) +"%";
            labelCloudy.Text = "Cloudy: " + weathers[1] + "; " + Math.Round((float)weathers[1] / (float)(all), 3) + "%";
            labelOvercast.Text = "Overcast: " + weathers[2] + "; " + Math.Round((float)weathers[2] / (float)(all), 3) + "%";
        }
    }
}
