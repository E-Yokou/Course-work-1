using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Theor1;

namespace laba1_v45_6
{
    public class Token
    {
        public TokenType Type;
        public string Value;

        public Token(TokenType type)
        {
            Type = type;
        }
        public override string ToString()
        {
            if( Type == TokenType.VARIABLE || Type == TokenType.LITERAL)
                return string.Format("{0}, {1}", Type, Value);
            else 
                return string.Format("{0}", Type);
        }
        public enum TokenType
        {
            AS, MAIN, THEN, INT, BOOL, LITERAL, IDENTIFIER, CURLYBRACECLOSE, STRING, NETERM,
            IF, ELSE, TRUE, FALSE, PLUS, MORE, LESS, SEMICOLON, CURLYBRACEOPEN, AND, EXPR,
            MINUS, EQUAL, MULTIPLY, RPAR, LPAR, ENTER, DIVISION, COMMA, VARIABLE, OR
        }

        public static Dictionary<string, TokenType> SpecialWords = new Dictionary<string, TokenType>()
        {
             { "int",  TokenType.INT },
             { "string",  TokenType.STRING },
             { "bool", TokenType.BOOL },
             { "if",   TokenType.IF },
             { "else", TokenType.ELSE },
             { "main", TokenType.MAIN },
             { "as",   TokenType.AS },
             { "then", TokenType.THEN },
        };

        public static bool IsSpecialWord(string word)
        {
            if (string.IsNullOrEmpty(word))
            {
                return false;
            }
            return SpecialWords.ContainsKey(word);
        }

        public static Dictionary<string, TokenType> SpecialSymbols = new Dictionary<string, TokenType>()
        {
             { "#", TokenType.ENTER },
             { "(", TokenType.LPAR },
             { ")", TokenType.RPAR },
             { "+", TokenType.PLUS },
             { "-", TokenType.MINUS },
             { "=", TokenType.EQUAL },
             { ">", TokenType.MORE },
             { "<", TokenType.LESS },
             { "*", TokenType.MULTIPLY },
             { "&&", TokenType.AND },
             { "||", TokenType.OR },
             { "/", TokenType.DIVISION },
             { ",", TokenType.COMMA },
             { ";", TokenType.SEMICOLON },
             { "{", TokenType.CURLYBRACEOPEN },
             { "}", TokenType.CURLYBRACECLOSE }
        };

        public static bool IsSpecialSymbol(string str)
        {
            return SpecialSymbols.ContainsKey(str);
        }

        public static void PrintTokens(RichTextBox box3, RichTextBox box4, List<Token> list, bool IsButton4Enabled, bool IsButton5Enabled)
        {
            int i = 0;
            box3.Clear();
            foreach (var t in list)
            {
                i++;

                box3.Text += $"{i} {t} ";
                box3.Text += Environment.NewLine;
            }

            if (IsButton4Enabled)
            {
                Recognizer recognizer = new Recognizer(list);
                recognizer.Start();

            }

            if (IsButton5Enabled)
            {
                Recognizer recognizer = new Recognizer(list);
                recognizer.Start();

                box4.Clear();

                box4.Text += $"Результат Действие Операнд1 Операнд2" + Environment.NewLine;

                int k = 0;
                foreach (Troyka t in recognizer.operatsii)
                {
                    box4.Text += $"m{k++} {t.deystvie.Value, 25} {t.operand1.Value,20} {t.operand2.Value,20}" + Environment.NewLine;
                }
            }
        }
    }
}
