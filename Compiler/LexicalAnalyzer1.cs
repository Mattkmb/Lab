using System;

namespace Компилятор
{
    class LexicalAnalyzer
    {
        public const byte
            star = 21, // *
            slash = 60, // /
            equal = 16, // =
            comma = 20, // ,
            semicolon = 14, // ;
            colon = 5, // :
            point = 61,    // .
            arrow = 62,    // ^
            leftpar = 9,    // (
            rightpar = 4,    // )
            lbracket = 11,    // [
            rbracket = 12,    // ]
            flpar = 63,    // {
            frpar = 64,    // }
            later = 65,    // <
            greater = 66,    // >
            laterequal = 67,    //  <=
            greaterequal = 68,    //  >=
            latergreater = 69,    //  <>
            plus = 70,    // +
            minus = 71,    // –
            lcomment = 72,    //  (*
            rcomment = 73,    //  *)
            assign = 51,    //  :=
            twopoints = 74,    //  ..
            ident = 2,    // идентификатор
            floatc = 82,    // вещественная константа
            intc = 15,    // целая константа
            charconst = 75,    // символьная константа
            EOF = 76,          // конец файла
            casesy = 31,
            elsesy = 32,
            filesy = 57,
            gotosy = 33,
            thensy = 52,
            typesy = 34,
            untilsy = 53,
            dosy = 54,
            withsy = 37,
            ifsy = 56,
            insy = 100,
            ofsy = 101,
            orsy = 102,
            tosy = 103,
            endsy = 104,
            varsy = 105,
            divsy = 106,
            andsy = 107,
            notsy = 108,
            forsy = 109,
            modsy = 110,
            nilsy = 111,
            setsy = 112,
            beginsy = 113,
            whilesy = 114,
            arraysy = 115,
            constsy = 116,
            labelsy = 117,
            downtosy = 118,
            packedsy = 119,
            recordsy = 120,
            repeatsy = 121,
            programsy = 122,
            functionsy = 123,
            procedurensy = 124,
            integer = 125,
            real = 126,
            boolean = 127,
            character = 128;

        public byte Symbol { get; private set; }
        public TextPosition TokenPosition { get; private set; }
        public string? AddrName { get; private set; }
        public int NmbInt { get; private set; }
        public float NmbFloat { get; private set; }
        public char CharValue { get; private set; }

        private Keywords keywords = new Keywords();

        public LexicalAnalyzer()
        {
            InputOutput.NextCh();
        }

        public byte NextToken()
        {
            while (char.IsWhiteSpace(InputOutput.Ch))
                InputOutput.NextCh();

            TokenPosition = InputOutput.positionNow;

            if (InputOutput.File != null && InputOutput.File.EndOfStream)
                return Symbol = EOF;

            switch (InputOutput.Ch)
            {
                case char c when char.IsLetter(c):
                    ProcessIdentifier();
                    break;
                case char c when char.IsDigit(c):
                    ProcessNumber();
                    break;
                case '\'':
                    ProcessCharConstant();
                    break;
                case '<':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        Symbol = laterequal; InputOutput.NextCh();
                    }
                    else if (InputOutput.Ch == '>')
                    {
                        Symbol = latergreater; InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = later;
                    }
                    break;
                case '>':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        Symbol = greaterequal; InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = greater;
                    }
                    break;
                case ':':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '=')
                    {
                        Symbol = assign; InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = colon;
                    }
                    break;
                case ';':
                    Symbol = semicolon;
                    InputOutput.NextCh();
                    break;
                case '.':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '.')
                    {
                        Symbol = twopoints; InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = point;
                    }
                    break;
                case '+':
                    Symbol = plus;
                    InputOutput.NextCh();
                    break;
                case '-':
                    Symbol = minus;
                    InputOutput.NextCh();
                    break;
                case '*':
                    Symbol = star;
                    InputOutput.NextCh();
                    break;
                case '/':
                    Symbol = slash;
                    InputOutput.NextCh();
                    break;
                case '=':
                    Symbol = equal;
                    InputOutput.NextCh();
                    break;
                case ',':
                    Symbol = comma;
                    InputOutput.NextCh();
                    break;
                case '(':
                    InputOutput.NextCh();
                    if (InputOutput.Ch == '*')
                    {
                        ProcessComment();
                        return NextToken();
                    }
                    else
                    {
                        Symbol = leftpar;
                    }
                    break;
                case ')':
                    Symbol = rightpar;
                    InputOutput.NextCh();
                    break;
                case '[':
                    Symbol = lbracket;
                    InputOutput.NextCh();
                    break;
                case ']':
                    Symbol = rbracket;
                    InputOutput.NextCh();
                    break;
                case '{':
                    ProcessComment();
                    return NextToken();
                case '\r': 
                case '\n':
                    InputOutput.NextCh();
                    return NextToken();
                default:
                    InputOutput.Error(ErrorCodes.UNRECOGNIZED_CHAR, TokenPosition);
                    InputOutput.NextCh();
                    return NextToken();
            }
            return Symbol;
        }

        private void ProcessIdentifier()
        {
            string name = "";
            while (char.IsLetterOrDigit(InputOutput.Ch) || InputOutput.Ch == '_')
            {
                name += InputOutput.Ch;
                InputOutput.NextCh();
            }

            byte len = (byte)name.Length;
            if (keywords.Kw.ContainsKey(len) &&
                keywords.Kw[len].ContainsKey(name.ToLower()))
            {
                Symbol = keywords.Kw[len][name.ToLower()];
            }
            else
            {
                Symbol = ident;
                AddrName = name;
            }
        }

        private void ProcessNumber()
        {
            NmbInt = 0;
            while (char.IsDigit(InputOutput.Ch))
            {
                int digit = InputOutput.Ch - '0';
                if (NmbInt <= (int.MaxValue - digit) / 10)
                {
                    NmbInt = NmbInt * 10 + digit;
                }
                else
                {
                    InputOutput.Error(ErrorCodes.INT_CONST_OVERFLOW, InputOutput.positionNow);
                    while (char.IsDigit(InputOutput.Ch))
                        InputOutput.NextCh();
                    return;
                }
                InputOutput.NextCh();
            }

            // Проверка на вещественное число
            if (InputOutput.Ch == '.' && char.IsDigit(InputOutput.Peek()))
            {
                InputOutput.NextCh();
                ProcessRealNumber(NmbInt);
            }
            else
            {
                Symbol = intc;
            }
        }

        private void ProcessRealNumber(int integerPart)
        {
            if (!char.IsDigit(InputOutput.Ch))
            {
                InputOutput.Error(ErrorCodes.INVALID_REAL_NUMBER, InputOutput.positionNow);
                Symbol = intc;
                return;
            }

            float fraction = 0.1f;
            NmbFloat = integerPart;
            while (char.IsDigit(InputOutput.Ch))
            {
                int digit = InputOutput.Ch - '0';
                NmbFloat += digit * fraction;
                fraction *= 0.1f;
                InputOutput.NextCh();
            }
            Symbol = floatc;
        }

        private void ProcessCharConstant()
        {
            InputOutput.NextCh();
            if (InputOutput.Ch == '\'')
            {
                InputOutput.Error(ErrorCodes.EMPTY_CHAR_CONST, InputOutput.positionNow);
                InputOutput.NextCh();
                Symbol = charconst;
                return;
            }

            CharValue = InputOutput.Ch;
            InputOutput.NextCh();

            if (InputOutput.Ch != '\'')
            {
                InputOutput.Error(ErrorCodes.UNCLOSED_CHAR_CONST, InputOutput.positionNow);
                while (InputOutput.Ch != '\'' &&
                       InputOutput.Ch != '\n' &&
                       InputOutput.Ch != '\r' &&
                       !InputOutput.File.EndOfStream)
                {
                    InputOutput.NextCh();
                }
                if (InputOutput.Ch == '\'')
                    InputOutput.NextCh();
            }
            else
            {
                InputOutput.NextCh();
            }
            Symbol = charconst;
        }

        private void ProcessComment()
        {
            if (InputOutput.Ch == '{')
            {
                InputOutput.NextCh();
                while (InputOutput.Ch != '}' &&
                       InputOutput.File != null &&
                       !InputOutput.File.EndOfStream)
                {
                    InputOutput.NextCh();
                }
                if (InputOutput.Ch == '}')
                    InputOutput.NextCh();
            }
            else if (InputOutput.Ch == '(')
            {
                InputOutput.NextCh();
                if (InputOutput.Ch == '*')
                {
                    InputOutput.NextCh();
                    while (!(InputOutput.Ch == '*' && InputOutput.Peek() == ')') &&
                          InputOutput.File != null &&
                          !InputOutput.File.EndOfStream)
                    {
                        InputOutput.NextCh();
                    }
                    if (InputOutput.Ch == '*')
                    {
                        InputOutput.NextCh();
                        if (InputOutput.Ch == ')')
                            InputOutput.NextCh();
                    }
                }
            }
        }
    }
}