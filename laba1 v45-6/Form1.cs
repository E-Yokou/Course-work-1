using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Theor1;

namespace laba1_v45_6
{
    public partial class Form1 : Form
    {
        Analysis analysis = new Analysis();
        AnalysisToken analysisToken = new AnalysisToken();


        bool IsButton4Enabled = false;
        bool IsButton5Enabled = false;
        bool IsFinishLexemAnalis = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //get path to file
        {
            Analysis.PathToFile(textBox1, richTextBox2);
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;

            richTextBox1.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
        }

        private void button2_Click(object sender, EventArgs e) //get analys
        {
            analysis.Gate(richTextBox1, richTextBox2, out IsFinishLexemAnalis);

            if (IsFinishLexemAnalis == true)
                MessageBox.Show("Лексический анализ закончен без ошибок!");

            if (IsFinishLexemAnalis == false)
                MessageBox.Show("ERROR!\nЛексический анализ закончен с ошибкой!");
        }

        private void button3_Click(object sender, EventArgs e) //get token
        {
            analysisToken.ReWork(richTextBox2, richTextBox3, richTextBox4, IsButton4Enabled, IsButton5Enabled);

            MessageBox.Show("Работа закончена без ошибок!");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bool IsButton4Enabled = true;
            bool IsButton5Enabled = false;

            richTextBox3.Clear();
            analysisToken.ReWork(richTextBox2, richTextBox3, richTextBox4, IsButton4Enabled, IsButton5Enabled);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox4.Clear();

            bool IsButton5Enabled = true;
            bool IsButton4Enabled = false;

            richTextBox3.Clear();
            analysisToken.ReWork(richTextBox2, richTextBox3, richTextBox4, IsButton4Enabled, IsButton5Enabled);
        }

        private void richTextBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            try
            {
                int firstcharindex = richTextBox1.GetFirstCharIndexOfCurrentLine();
                int currentline = richTextBox1.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = richTextBox1.Lines[currentline];
                richTextBox1.Select(firstcharindex, currentlinetext.Length + 1);
            }
            catch { MessageBox.Show("ERROR!\nТекстовое поле пустое!"); }
        }

        private void richTextBox3_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int firstcharindex = richTextBox3.GetFirstCharIndexOfCurrentLine();
                int currentline = richTextBox3.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = richTextBox3.Lines[currentline];
                richTextBox3.Select(firstcharindex, currentlinetext.Length + 1);
            }
            catch { MessageBox.Show("ERROR!\nТекстовое поле пустое!"); }
        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int firstcharindex = richTextBox3.GetFirstCharIndexOfCurrentLine();
                int currentline = richTextBox3.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = richTextBox3.Lines[currentline];
                richTextBox3.Select(firstcharindex, currentlinetext.Length + 1);
            }
            catch { MessageBox.Show("ERROR!\nТекстовое поле пустое!"); }
        }

        private void richTextBox4_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int firstcharindex = richTextBox4.GetFirstCharIndexOfCurrentLine();
                int currentline = richTextBox4.GetLineFromCharIndex(firstcharindex);
                string currentlinetext = richTextBox4.Lines[currentline];
                richTextBox4.Select(firstcharindex, currentlinetext.Length + 1);
            }
            catch { MessageBox.Show("ERROR!\nТекстовое поле пустое!"); }
        }
    }
}
