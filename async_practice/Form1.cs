using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace async_practice
{
    public partial class Form1 : Form
    {
        static List<Label> labelList = new List<Label>();
        List<Thread> threadsList = new List<Thread>();

        static readonly object _locker = new object();
        static bool keepGoing = true;

        public Form1()
        {
            InitializeComponent();
            label1.Text = "0";
            label2.Text = "0";
            label3.Text = "0";
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
        }

       

        private void btnTikTok_Click(object sender, EventArgs e)
        {
            keepGoing = true;
            Thread t = new Thread( ()=> tik(0));
            Thread t2 = new Thread(() => tik(1));
            Thread t3 = new Thread(() => tik(2));
            threadsList.Add(t);
            threadsList.Add(t2);
            threadsList.Add(t3);
            foreach (Thread th in threadsList)
            {
                if (th.ThreadState == ThreadState.Unstarted) { th.Start(); }
            }
            
        }

        void tik(int labelIndex)
        {
            while (keepGoing)
            {
                int j = int.Parse(labelList[labelIndex].Text);
                j += 1;
                Thread.Sleep((1000*(labelIndex + 1)/3));
                update(j, labelIndex);
            }
        }

        void update(int Label, int labelIndex)
        {
            if (keepGoing)
            {
                Action action = () =>
                {
                    labelList[labelIndex].Text = Label.ToString();
                    Console.WriteLine(Label.ToString());
                };
                this.Invoke(action);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            keepGoing = false;

        }



    }
}
