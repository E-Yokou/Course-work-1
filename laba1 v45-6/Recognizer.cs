using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Theor1;
using static laba1_v45_6.Token;

namespace laba1_v45_6
{
    internal class Recognizer
    {
        //throw new Exception($"{i + 1} : {tokens[i].Value}");

        public List<Troyka> operatsii = new List<Troyka>();
        Stack<Token> lexemStack = new Stack<Token>();
        public List<Token> tokens;

        int nextLex = 0;
        int lastEXPRind;
        bool firstEXPR = true;
        int i;

        public Recognizer(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Start()
        {
            i = 0;
            
            try
            {
                Program();
                MessageBox.Show($"Работа нисходящего анализатора закончена без ошибок!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR!\nNUMBER: {ex.Message}");
            }
        }

        public void Program()
        {
            if (tokens[i].Type != Token.TokenType.MAIN)
                throw new Exception($"1\nSTRING: {i + 1} - Ожидалось: MAIN, а получено: {tokens[i].Value}");
            Next();
            if (tokens[i].Type != Token.TokenType.LPAR)
                throw new Exception($"2\nSTRING: {i + 1} - Ожидалось: '(', а получено: {tokens[i].Value}");
            Next();
            if (tokens[i].Type != Token.TokenType.RPAR)
                throw new Exception($"3\nSTRING: {i + 1} - Ожидалось: ')', а получено: {tokens[i].Value}");
            Next();
            if (tokens[i].Type != Token.TokenType.CURLYBRACEOPEN)
                throw new Exception($"4\nSTRING: {i + 1} - Ожидалось: 'CURLYBRACEOPEN', а получено: {tokens[i].Value}");
            Next();
            
            SpisOpis();
            SpisOper();
               
            if (tokens[i].Type != Token.TokenType.CURLYBRACECLOSE)
                throw new Exception($"6\nSTRING: {i + 1} - Ожидалось: CURLYBRACECLOSE, а получено: {tokens[i].Value}");
        }

        public void SpisOpis()
        {
            if (tokens[i].Type != Token.TokenType.INT &&
               tokens[i].Type != Token.TokenType.BOOL &&
               tokens[i].Type != Token.TokenType.STRING)
                throw new Exception($"7\nSTRING: {i + 1} - Ожидалось: INT, BOOL, STRING, а получено: {tokens[i].Value}");
            Opis();
            
            DopOpis();
            
        }

        public void Opis()
        {
            Type();
            SpisPerem();
            
        }

        public void Type()
        {
            if (tokens[i].Type != Token.TokenType.INT &&
               tokens[i].Type != Token.TokenType.BOOL &&
               tokens[i].Type != Token.TokenType.STRING)
                throw new Exception($"8\nSTRING: {i + 1} - Ожидалось: INT, BOOL, STRING, а получено: {tokens[i].Value}");
            Next();
        }

        public void SpisPerem()
        {
            if (tokens[i].Type == Token.TokenType.VARIABLE)
            {
                Next();
                X();
            }
            else throw new Exception($"9\nSTRING: {i + 1} - Ожидалось: IDENTIFIER, а получено: {tokens[i].Value}");
            
        }

        public void X()
        {
            switch (tokens[i].Type)
            {
                case Token.TokenType.COMMA:
                    alt2();
                    break;

                case Token.TokenType.SEMICOLON:
                    Next();
                    
                    if (tokens[i].Type == Token.TokenType.INT ||
                        tokens[i].Type == Token.TokenType.STRING ||
                        tokens[i].Type == Token.TokenType.BOOL)
                    {
                        Opis();
                    }
                    Next();
                    break;

                default: throw new Exception($"10\nSTRING: {i + 1} - Ожидалось: ';', а получено: {tokens[i].Value}");
            }
            
        }

        public void alt2()
        {
            if (tokens[i].Type != Token.TokenType.COMMA)
                throw new Exception($"11\nSTRING: {i + 1} - Ожидалось: ',', а получено: {tokens[i].Value}");
            Next();
            if (tokens[i].Type != Token.TokenType.VARIABLE)
                throw new Exception($"12\nSTRING: {i + 1} - Ожидалось: IDENTIFIER, а получено: {tokens[i].Value}");
            Next();
            X();

            
        }

        public void DopOpis()
        {
            Next();
            switch (tokens[i].Type)
            {
                case Token.TokenType.COMMA:
                    DopOpis2();
                    Next();
                    break;

                case Token.TokenType.SEMICOLON:
                    Next();
                    
                    break;

                default: throw new Exception($"13\nSTRING: {i + 1} - Ошибка в присвоении! получено {tokens[i].Value}");
            }
            
        }

        public void DopOpis2()
        {
            if (tokens[i].Type != Token.TokenType.COMMA)
                throw new Exception($"14\nSTRING: {i + 1} - Ожидалось: ',', а получено: {tokens[i].Value}");
            Next();

            if(tokens[i].Type != Token.TokenType.VARIABLE)
                throw new Exception($"15\nSTRING: {i + 1} - Ожидалось: идентификатор, а получено: {tokens[i].Value}");
            Oper();
            DopOper();
            
        }

        public void Oper()
        {
            if (tokens[i].Type == Token.TokenType.CURLYBRACEOPEN)
                Next();

            switch (tokens[i].Type)
            {
                case Token.TokenType.IF:
                    _If();
                    break;

                case Token.TokenType.VARIABLE:
                    Prisv();
                    break;

                case Token.TokenType.ELSE:
                    break;

                case Token.TokenType.CURLYBRACECLOSE:
                    break;

                //case Token.TokenType.ENTER:
                //    
                //    break;

                default: throw new Exception($"16\nSTRING: {i + 1} - Ожидалось: условие или идентификатор, а получено: {tokens[i].Value}");
            }

            if (tokens[i].Type == Token.TokenType.CURLYBRACECLOSE)
                Next();
        }

        public void _If()
        {
            if (tokens[i].Type != Token.TokenType.IF)
                throw new Exception($"17\nSTRING: {i + 1} - Ожидалось: условие, а получено: {tokens[i].Value}");
            Next();

            if (tokens[i].Type != Token.TokenType.LPAR)
                throw new Exception($"18\nSTRING: {i + 1} - Ожидалось: '(', а получено: {tokens[i].Value}");
            Next();

            Expr();

            if (tokens[i].Type != Token.TokenType.RPAR)
                throw new Exception($"18\nSTRING: {i + 1} - Ожидалось: '(', а получено: {tokens[i].Value}");
            Next();

            if (tokens[i].Type != Token.TokenType.CURLYBRACEOPEN)
                Next();

            BlockOper();

            if (tokens[i].Type != Token.TokenType.CURLYBRACECLOSE)
                Next();

            DopYslov();
            
        }

        public void Expr() 
        {
            List<Token> inmet = new List<Token>();
            while (tokens[i].Type != Token.TokenType.RPAR)
            {
                inmet.Add(tokens[i]);
                Next();
            }

            if (firstEXPR == true)
            {
                Bower a = new Bower(inmet);
                a.Start();
                foreach (Troyka troyka in a.troyka)
                    operatsii.Add(troyka);
                lastEXPRind = a.Lastindex;
                firstEXPR = false;
            }
            else
            {
                Bower a = new Bower(inmet, lastEXPRind);
                a.Start();
                foreach (Troyka troyka in a.troyka)
                    operatsii.Add(troyka);
                lastEXPRind = a.Lastindex;
            }
            Token k = new Token(TokenType.EXPR);
            lexemStack.Push(k);

            //while (tokens[i].Type != Token.TokenType.RPAR)
            //{
            //    Next();
            //}
            //Next();
            //
        }

        public void BlockOper()
        {
            Oper();
            Next();
            
            if (tokens[i].Type == Token.TokenType.CURLYBRACEOPEN)
                Next();
                SpisOper();
            
        }

        public void DopYslov()
        {
            switch (tokens[i].Type)
            {
                case Token.TokenType.VARIABLE:
                    DopYslov2();
                    break;

                case Token.TokenType.SEMICOLON:
                    break;

                case Token.TokenType.CURLYBRACECLOSE: 
                    break;

                case Token.TokenType.ENTER:
                    break;

                default : throw new Exception($"19\nОшибка в условии, получено {tokens[i].Value}");
            }
        }

        public void DopYslov2()
        {
            if (tokens[i].Type == Token.TokenType.ELSE)
                Expr();
            
        }

        public void Prisv()
        {
            if (tokens[i].Type != Token.TokenType.VARIABLE)
                throw new Exception($"20\nSTRING: {i + 1} - Ожидалось: идентификатор, а получено: {tokens[i].Value}");
            Next();
            if (tokens[i].Type != Token.TokenType.EQUAL)
                throw new Exception($"21\nSTRING: {i + 1} - Ожидалось: '=', а получено: {tokens[i].Value}");
            Next();

            Operand();
            DopPrisv();
            
        }

        public void SpisOper()
        {
            
            if (tokens[i].Type != Token.TokenType.IF && tokens[i].Type != Token.TokenType.VARIABLE 
                && tokens[i].Type != Token.TokenType.CURLYBRACECLOSE)
                throw new Exception($"22\nSTRING: {i + 1} - Ожидалось: условие, идентификатор или открывающаяся фигурная скобка, а получено: {tokens[i].Value}");
            //Next();
            Oper();
            DopOper();
            
        }

        public void DopPrisv()
        {
            switch (tokens[i].Type)
            {
                case Token.TokenType.SEMICOLON:
                    Next();
                    
                    Oper();
                    break;

                case Token.TokenType.VARIABLE:
                    Next();
                    DopPrisv2();
                    break;

                case Token.TokenType.PLUS:
                    DopPrisv2();
                    break;

                case Token.TokenType.MINUS:
                    DopPrisv2();
                    break;

                case Token.TokenType.DIVISION:
                    DopPrisv2();
                    break;

                case Token.TokenType.MULTIPLY:
                    DopPrisv2();
                    break;

                default: throw new Exception($"23\nSTRING: {i + 1} - Ожидалось: ';', '+', '-', '/' или идентификатор, а получено: {tokens[i].Value}");
            }
        }

        public void DopPrisv2()
        {
            Sign();
            Operand();

            if (tokens[i + 1].Type == Token.TokenType.VARIABLE)
                Next();
                Prisv();

        }

        public void DopOper()
        {
            switch (tokens[i].Type)
            {
                case Token.TokenType.SEMICOLON:
                    break;

                case Token.TokenType.COMMA:
                    Next();
                    DopOper2();
                    break;

                case Token.TokenType.CURLYBRACECLOSE:
                    break;

                case Token.TokenType.IF:
                    _If();
                    break;

                default: throw new Exception($"24\nSTRING: {i + 1} - Ожидалось: ';' или идентификатор, а получено: {tokens[i].Value}");
            }
        }

        public void DopOper2()
        {
            if (tokens[i].Type != Token.TokenType.COMMA)
                throw new Exception($"25\nSTRING: {i + 1} - Ожидалось: ',', а получено: {tokens[i].Value}");
            Next();
            Oper();
            DopOper();
            
        }

        public void Operand()
        {
            if (tokens[i].Type != Token.TokenType.VARIABLE && tokens[i].Type != Token.TokenType.LITERAL)
                throw new Exception($"26\nSTRING: {i + 1} - Ожидалось: идентификатор или число, а получено: {tokens[i].Value}");
            Next();
            
        }

        public void Sign() //знак
        {
            if (tokens[i].Type != Token.TokenType.AND && tokens[i].Type != Token.TokenType.OR &&
                tokens[i].Type != Token.TokenType.PLUS && tokens[i].Type != Token.TokenType.MINUS)
                throw new Exception($"27\nSTRING: {i + 1} - Ожидалось: &&, ||, +, -, а получено: {tokens[i].Value}");
            Next();
        }

        public void Next()
        {
            if (i < tokens.Count - 1)
            {
                i++;
            }

            
        }

        public void SkipEnter()
        {
            while (tokens[i].Type == Token.TokenType.ENTER && i < tokens.Count - 1)
            {
                Next();
            }
        }
    }
}
