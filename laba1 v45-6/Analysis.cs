using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1_v45_6
{
    class Analysis
    {
        Dictionary<string, string> parts = new Dictionary<string, string>();

        public static void PathToFile(TextBox box1, RichTextBox box2)
        {
            string text = "";

            try
            {
                using (StreamReader fs = new StreamReader(box1.Text))
                {
                    while (true)
                    {
                        string temp = fs.ReadLine();
                        if (temp == null) break;
                        text += temp;
                        text += " \n  ";
                    }
                    box2.Text = text;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Указанный файл не найден!");
            }
        }

        public void Gate(RichTextBox box1, RichTextBox box2, out bool IsFinishLexemAnalis)
        {
            box1.Clear();

            IsFinishLexemAnalis = true;
            parts = new Dictionary<string, string>();
            int i = 0;
            string subText = "";

            if(box2.Text.Length == 0) 
                IsFinishLexemAnalis = false;

            foreach (char s in box2.Text)
            {
                try
                {
                    if (Lexems.IsOperator(subText) && (s == ' ' || s == '<' || s == '>' || s == ';'))
                    {
                        i++;
                        parts.Add(i.ToString() + " ", subText + " - Идентификатор - оператор;");
                        subText = "";
                    }
                    else if (Lexems.IsLiteral(subText) && (s == ' ' || s == ';' || s == ')'))
                    {
                        i++;
                        parts.Add(i.ToString() + " ", subText + " - Литератор;");
                        subText = "";
                    }
                    else if (Lexems.IsSeparator(subText) && (s == ' ' || s == ')' || char.IsDigit(s) || char.IsLetter(s)))
                    {
                        i++;
                        if (subText != "\n")
                        {
                            parts.Add(i.ToString() + " ", subText + " - Разделитель;");
                        }
                        else if (subText == "\n")
                        {
                            subText = "'/n'";
                            parts.Add(i.ToString() + " ", subText + " - Разделитель (символ новой строки);");
                        }
                        else if (subText == "&&")
                            parts.Add(i.ToString() + " ", subText + " - Разделитель (логический операнд);");

                        subText = "";
                    }
                    else if (Lexems.IsIDVariable(subText) && !Lexems.IsOperator(subText) && (s == ' ' || s == '<' || s == '>' || s == ';' || s == '+' || s == '-' || s == '*' || s == '/' || s == ',' || s == '(' || s == ')'))
                    {
                        i++;
                        parts.Add(i.ToString() + " ", subText + " - Идентификатор - переменная;");
                        subText = "";
                    }
                    else if (subText == Environment.NewLine || subText == " ")
                    {
                        subText = "";
                    }

                    if (subText.Length > 3)
                        IsFinishLexemAnalis = false;

                    subText += s;
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось закончить лексический анализ.");
                }
            }

            box1.Clear();
            foreach (KeyValuePair<string, string> pair in parts)
            {
                box1.Text += pair.Key.PadRight(10) + " " + pair.Value + Environment.NewLine;
            }
        }
    }
}

