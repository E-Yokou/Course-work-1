using laba1_v45_6;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;
using static laba1_v45_6.Token;

namespace Theor1
{
    public struct Troyka
    {
        public Token operand1;
        public Token operand2;
        public Token deystvie;
        public Troyka(Token dy, Token op2, Token op1)
        {
            operand1 = op1;
            operand2 = op2;
            deystvie = dy;
        }
    }
    public class Bower
    {
        int index = 0;
        public List<Troyka> troyka = new List<Troyka>();
        List<Token> tokens = new List<Token>();
        Stack<Token> E = new Stack<Token>();
        Stack<Token> T = new Stack<Token>();
        int nextlex = 0;
        public Bower(List<Token> inmet)
        {
            tokens = inmet;
        }
        public Bower(List<Token> inmet, int index)
        {
            tokens = inmet;
            this.index = index;
        }
        public int Lastindex { get { return index; } }
        private Token GetLexeme(int nextLex)
        {
            return tokens[nextLex];
        }
        private void Operand()
        {
            E.Push(tokens[nextlex]);
            nextlex++;
        }
        private void Deystv()
        {
            Troyka k = new Troyka(T.Pop(), E.Pop(), E.Pop());
            troyka.Add(k);
            Token l = new Token(TokenType.VARIABLE);
            l.Value = $"m{index}";
            E.Push(l);
            index++;
        }
        private void PlusMinusOr()
        {
            if (T.Count == 0)
                D1();
            else
                switch (T.Peek().Type)
                {
                    case TokenType.LPAR:
                        D1();
                        break;
                    case TokenType.MORE:
                        D1();
                        break;
                    case TokenType.LESS:
                        D1();
                        break;
                    case TokenType.EQUAL:
                        D1();
                        break;
                    case TokenType.PLUS:
                        D2();
                        break;
                    case TokenType.MINUS:
                        D2();
                        break;
                    case TokenType.OR:
                        D2();
                        break;
                    case TokenType.MULTIPLY:
                        D4();
                        break;
                    case TokenType.DIVISION:
                        D4();
                        break;
                    case TokenType.AND:
                        D4();
                        break;
                    default:
                        throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
                }
        }
        private void MultiplyDivideAnd()
        {
            if (T.Count == 0)
                D1();
            else
                switch (T.Peek().Type)
                {
                    case TokenType.LPAR:
                        D1();
                        break;
                    case TokenType.MORE:
                        D1();
                        break;
                    case TokenType.LESS:
                        D1();
                        break;
                    case TokenType.EQUAL:
                        D1();
                        break;
                    case TokenType.PLUS:
                        D1();
                        break;
                    case TokenType.MINUS:
                        D1();
                        break;
                    case TokenType.OR:
                        D1();
                        break;
                    case TokenType.MULTIPLY:
                        D2();
                        break;
                    case TokenType.DIVISION:
                        D2();
                        break;
                    case TokenType.AND:
                        D2();
                        break;
                    default:
                        throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
                }
        }
        private void MoreLessEqual()
        {
            {
                if (T.Count == 0)
                    D1();
                else
                    switch (T.Peek().Type)
                    {
                        case TokenType.LPAR:
                            D1();
                            break;
                        case TokenType.MORE:
                            D2();
                            break;
                        case TokenType.LESS:
                            D2();
                            break;
                        case TokenType.EQUAL:
                            D2();
                            break;
                        case TokenType.PLUS:
                            D4();
                            break;
                        case TokenType.MINUS:
                            D4();
                            break;
                        case TokenType.OR:
                            D4();
                            break;
                        case TokenType.MULTIPLY:
                            D4();
                            break;
                        case TokenType.DIVISION:
                            D4();
                            break;
                        case TokenType.AND:
                            D4();
                            break;
                        default:
                            throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
                    }
            }
        }
        private void Lpar()
        {
            {
                if (T.Count == 0)
                    D1();
                else
                    switch (T.Peek().Type)
                    {
                        case TokenType.LPAR:
                            D1();
                            break;
                        case TokenType.MORE:
                            D1();
                            break;
                        case TokenType.LESS:
                            D1();
                            break;
                        case TokenType.EQUAL:
                            D1();
                            break;
                        case TokenType.PLUS:
                            D1();
                            break;
                        case TokenType.MINUS:
                            D1();
                            break;
                        case TokenType.OR:
                            D1();
                            break;
                        case TokenType.MULTIPLY:
                            D1();
                            break;
                        case TokenType.DIVISION:
                            D1();
                            break;
                        case TokenType.AND:
                            D1();
                            break;
                        default:
                            throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
                    }
            }
        }
        private void Rpar()
        {
            {
                if (T.Count == 0)
                    D5();
                else
                    switch (T.Peek().Type)
                    {
                        case TokenType.LPAR:
                            D3();
                            break;
                        case TokenType.MORE:
                            D4();
                            break;
                        case TokenType.LESS:
                            D4();
                            break;
                        case TokenType.EQUAL:
                            D4();
                            break;
                        case TokenType.PLUS:
                            D4();
                            break;
                        case TokenType.MINUS:
                            D4();
                            break;
                        case TokenType.OR:
                            D4();
                            break;
                        case TokenType.MULTIPLY:
                            D4();
                            break;
                        case TokenType.DIVISION:
                            D4();
                            break;
                        case TokenType.AND:
                            D4();
                            break;
                        default:
                            throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
                    }
            }
        }
        private void EndList()
        {
            switch (T.Peek().Type)
            {
                case TokenType.LPAR:
                    D5();
                    break;
                case TokenType.MORE:
                    D4();
                    break;
                case TokenType.LESS:
                    D4();
                    break;
                case TokenType.EQUAL:
                    D4();
                    break;
                case TokenType.PLUS:
                    D4();
                    break;
                case TokenType.MINUS:
                    D4();
                    break;
                case TokenType.OR:
                    D4();
                    break;
                case TokenType.MULTIPLY:
                    D4();
                    break;
                case TokenType.DIVISION:
                    D4();
                    break;
                case TokenType.AND:
                    D4();
                    break;
                default:
                    throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, ( или )");
            }
        }
        public void Start()
        {
            try
            {
                if (nextlex == tokens.Count)
                {
                    if (T.Count == 0)
                        return;
                    else
                    {
                        EndList();
                    }
                }
                else
                {
                    switch (GetLexeme(nextlex).Type)
                    {
                        case TokenType.VARIABLE:
                            Operand();
                            break;
                        case TokenType.LITERAL:
                            Operand();
                            break;
                        case TokenType.PLUS:
                            PlusMinusOr();
                            break;
                        case TokenType.MINUS:
                            PlusMinusOr();
                            break;
                        case TokenType.MULTIPLY:
                            MultiplyDivideAnd();
                            break;
                        case TokenType.DIVISION:
                            MultiplyDivideAnd();
                            break;
                        case TokenType.MORE:
                            MoreLessEqual();
                            break;
                        case TokenType.LESS:
                            MoreLessEqual();
                            break;
                        case TokenType.EQUAL:
                            MoreLessEqual();
                            break;
                        case TokenType.OR:
                            PlusMinusOr();
                            break;
                        case TokenType.AND:
                            MultiplyDivideAnd();
                            break;
                        case TokenType.LPAR:
                            Lpar();
                            break;
                        case TokenType.RPAR:
                            Rpar();
                            break;
                        default:
                            throw new Exception($"Ожидалось: +, -, *, /, >, <, =, or, and, (, ), идентификатор или литерал");
                    }
                }
                Start();
                //MessageBox.Show($"Разбор сложно-логического выражения завершён!");
            }
            catch (Exception)
            {
                MessageBox.Show("Непредвиденная ошибка разбор сложно-логического выражения не может быть завершён!");
            }
            
        }
        private void D1()
        {
            T.Push(tokens[nextlex]);
            nextlex++;
        }
        private void D2()
        {
            Deystv();
            T.Push(tokens[nextlex]);
            nextlex++;
        }
        private void D3()
        {
            T.Pop();
            nextlex++;
        }
        private void D4()
        {
            Deystv();
        }
        private void D5()
        {
            throw new Exception($"Ошибка в выражении. Конец разбора {tokens[nextlex]}");
        }
    }
}