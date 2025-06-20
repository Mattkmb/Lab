using System;
using System.Collections.Generic;

namespace Компилятор
{
    class Parser
    {
        private LexicalAnalyzer lexer;
        private byte currentToken;

        public Parser(LexicalAnalyzer lexer)
        {
            this.lexer = lexer;
            currentToken = lexer.NextToken();
        }

        private void Match(byte expectedToken)
        {
            if (currentToken == expectedToken)
            {
                currentToken = lexer.NextToken();
            }
            else
            {
                InputOutput.Error(ErrorCodes.UNEXPECTED_TOKEN, lexer.TokenPosition);

                // Если достигнут конец файла и мы ожидаем обязательный токен, это критическая ошибка
                if (currentToken == LexicalAnalyzer.EOF)
                {
                    InputOutput.Error(ErrorCodes.UNEXPECTED_END_OF_FILE, lexer.TokenPosition);
                    return;
                }

                while (currentToken != expectedToken &&
                       currentToken != LexicalAnalyzer.semicolon &&
                       currentToken != LexicalAnalyzer.endsy &&
                       currentToken != LexicalAnalyzer.EOF)
                {
                    currentToken = lexer.NextToken();
                }
                if (currentToken == expectedToken)
                    currentToken = lexer.NextToken();
            }
        }

        public void ParseProgram()
        {
            if (currentToken == LexicalAnalyzer.programsy)
            {
                Match(LexicalAnalyzer.programsy);
                Match(LexicalAnalyzer.ident);
                Match(LexicalAnalyzer.semicolon);
            }

            ParseBlock();

            // Обязательная проверка завершающей точки
            if (currentToken != LexicalAnalyzer.point)
            {
                InputOutput.Error(ErrorCodes.MISSING_PROGRAM_DOT, lexer.TokenPosition);
                return;
            }

            Match(LexicalAnalyzer.point);

            // После точки должен быть конец файла
            if (currentToken != LexicalAnalyzer.EOF)
            {
                InputOutput.Error(ErrorCodes.TEXT_AFTER_PROGRAM_END, lexer.TokenPosition);
            }
        }

        private void ParseBlock()
        {
            while (currentToken == LexicalAnalyzer.varsy ||
                   currentToken == LexicalAnalyzer.constsy ||
                   currentToken == LexicalAnalyzer.typesy)
            {
                if (currentToken == LexicalAnalyzer.varsy)
                    ParseVarDeclaration();
                else
                    currentToken = lexer.NextToken(); // Пропускаем другие описания
            }

            if (currentToken == LexicalAnalyzer.EOF)
            {
                InputOutput.Error(ErrorCodes.UNEXPECTED_END_OF_FILE, lexer.TokenPosition);
                return;
            }

            ParseCompoundStatement();
        }

        private void ParseVarDeclaration()
        {
            Match(LexicalAnalyzer.varsy);
            do
            {
                ParseIdentifierList();
                Match(LexicalAnalyzer.colon);
                ParseType();
                Match(LexicalAnalyzer.semicolon);
            } while (currentToken == LexicalAnalyzer.ident);
        }

        private void ParseIdentifierList()
        {
            if (currentToken != LexicalAnalyzer.ident)
                InputOutput.Error(ErrorCodes.EXPECTED_IDENTIFIER, lexer.TokenPosition);
            else
                currentToken = lexer.NextToken();

            while (currentToken == LexicalAnalyzer.comma)
            {
                Match(LexicalAnalyzer.comma);
                if (currentToken != LexicalAnalyzer.ident)
                    InputOutput.Error(ErrorCodes.EXPECTED_IDENTIFIER, lexer.TokenPosition);
                else
                    currentToken = lexer.NextToken();
            }
        }

        private void ParseType()
        {
            if (currentToken == LexicalAnalyzer.arraysy)
            {
                ParseArrayType();
            }
            else
            {
                if (currentToken != LexicalAnalyzer.integer &&
                    currentToken != LexicalAnalyzer.real &&
                    currentToken != LexicalAnalyzer.boolean &&
                    currentToken != LexicalAnalyzer.character &&
                    currentToken != LexicalAnalyzer.ident)
                {
                    InputOutput.Error(ErrorCodes.UNEXPECTED_TOKEN_TYPE, lexer.TokenPosition);
                }
                currentToken = lexer.NextToken();
            }
        }

        private void ParseArrayType()
        {
            Match(LexicalAnalyzer.arraysy);
            Match(LexicalAnalyzer.lbracket);
            ParseIndexRange();
            Match(LexicalAnalyzer.rbracket);
            Match(LexicalAnalyzer.ofsy);
            ParseType();
        }

        private void ParseIndexRange()
        {
            if (currentToken == LexicalAnalyzer.intc)
                currentToken = lexer.NextToken();
            else
                InputOutput.Error(ErrorCodes.UNEXPECTED_TOKEN, lexer.TokenPosition);

            Match(LexicalAnalyzer.twopoints);

            if (currentToken == LexicalAnalyzer.intc)
                currentToken = lexer.NextToken();
            else
                InputOutput.Error(ErrorCodes.UNEXPECTED_TOKEN, lexer.TokenPosition);
        }

        private void ParseCompoundStatement()
        {
            if (currentToken != LexicalAnalyzer.beginsy)
            {
                InputOutput.Error(ErrorCodes.EXPECTED_BEGIN, lexer.TokenPosition);
                return;
            }

            Match(LexicalAnalyzer.beginsy);

            // Разрешить пустой блок
            if (currentToken == LexicalAnalyzer.endsy)
            {
                Match(LexicalAnalyzer.endsy);
                return;
            }

            ParseStatement();
            while (currentToken == LexicalAnalyzer.semicolon)
            {
                Match(LexicalAnalyzer.semicolon);
                if (currentToken != LexicalAnalyzer.endsy)
                    ParseStatement();
            }

            if (currentToken != LexicalAnalyzer.endsy)
            {
                InputOutput.Error(ErrorCodes.EXPECTED_END, lexer.TokenPosition);
                return;
            }

            Match(LexicalAnalyzer.endsy);
        }

        private void ParseStatement()
        {
            // Проверка на конец файла в неожиданном месте
            if (currentToken == LexicalAnalyzer.EOF)
            {
                InputOutput.Error(ErrorCodes.UNEXPECTED_END_OF_FILE, lexer.TokenPosition);
                return;
            }

            switch (currentToken)
            {
                case LexicalAnalyzer.beginsy:
                    ParseCompoundStatement();
                    break;
                case LexicalAnalyzer.ifsy:
                    ParseIfStatement();
                    break;
                case LexicalAnalyzer.whilesy:
                    ParseWhileStatement();
                    break;
                case LexicalAnalyzer.repeatsy:
                    ParseRepeatStatement();
                    break;
                case LexicalAnalyzer.forsy:
                    ParseForStatement();
                    break;
                case LexicalAnalyzer.ident:
                    ParseAssignment();
                    break;
                case LexicalAnalyzer.semicolon:  // Пустой оператор
                    break;
                default:
                    InputOutput.Error(ErrorCodes.UNKNOWN_STATEMENT, lexer.TokenPosition);
                    // Пропустить токен и попытаться продолжить
                    currentToken = lexer.NextToken();
                    break;
            }
        }

        private void ParseAssignment()
        {
            ParseVariable();
            Match(LexicalAnalyzer.assign);
            ParseExpression();
        }

        private void ParseVariable()
        {
            if (currentToken != LexicalAnalyzer.ident)
                InputOutput.Error(ErrorCodes.EXPECTED_IDENTIFIER, lexer.TokenPosition);
            else
                currentToken = lexer.NextToken();

            if (currentToken == LexicalAnalyzer.lbracket)
            {
                Match(LexicalAnalyzer.lbracket);
                ParseExpression();
                Match(LexicalAnalyzer.rbracket);
            }
        }

        private void ParseIfStatement()
        {
            Match(LexicalAnalyzer.ifsy);
            ParseExpression();
            Match(LexicalAnalyzer.thensy);
            ParseStatement();
            if (currentToken == LexicalAnalyzer.elsesy)
            {
                Match(LexicalAnalyzer.elsesy);
                ParseStatement();
            }
        }

        private void ParseWhileStatement()
        {
            Match(LexicalAnalyzer.whilesy);
            ParseExpression();
            Match(LexicalAnalyzer.dosy);
            ParseStatement();
        }

        private void ParseRepeatStatement()
        {
            Match(LexicalAnalyzer.repeatsy);
            ParseStatement();
            while (currentToken == LexicalAnalyzer.semicolon)
            {
                Match(LexicalAnalyzer.semicolon);
                ParseStatement();
            }
            Match(LexicalAnalyzer.untilsy);
            ParseExpression();
        }

        private void ParseForStatement()
        {
            Match(LexicalAnalyzer.forsy);
            ParseVariable();
            Match(LexicalAnalyzer.assign);
            ParseExpression();

            if (currentToken == LexicalAnalyzer.tosy || currentToken == LexicalAnalyzer.downtosy)
            {
                currentToken = lexer.NextToken();
            }
            else
            {
                InputOutput.Error(ErrorCodes.MISSING_TO_DOWNTO, lexer.TokenPosition);
            }

            ParseExpression();
            Match(LexicalAnalyzer.dosy);
            ParseStatement();
        }

        private void ParseExpression()
        {
            ParseSimpleExpression();
            if (currentToken == LexicalAnalyzer.equal ||
                currentToken == LexicalAnalyzer.later ||
                currentToken == LexicalAnalyzer.greater ||
                currentToken == LexicalAnalyzer.laterequal ||
                currentToken == LexicalAnalyzer.greaterequal ||
                currentToken == LexicalAnalyzer.latergreater)
            {
                currentToken = lexer.NextToken();
                ParseSimpleExpression();
            }
        }

        private void ParseSimpleExpression()
        {
            if (currentToken == LexicalAnalyzer.plus || currentToken == LexicalAnalyzer.minus)
                currentToken = lexer.NextToken();

            ParseTerm();
            while (currentToken == LexicalAnalyzer.plus || currentToken == LexicalAnalyzer.minus)
            {
                currentToken = lexer.NextToken();
                ParseTerm();
            }
        }

        private void ParseTerm()
        {
            ParseFactor();
            while (currentToken == LexicalAnalyzer.star || currentToken == LexicalAnalyzer.slash)
            {
                currentToken = lexer.NextToken();
                ParseFactor();
            }
        }

        private void ParseFactor()
        {
            switch (currentToken)
            {
                case LexicalAnalyzer.ident:
                    ParseVariable();
                    break;
                case LexicalAnalyzer.intc:
                case LexicalAnalyzer.floatc:
                case LexicalAnalyzer.charconst:
                    currentToken = lexer.NextToken();
                    break;
                case LexicalAnalyzer.leftpar:
                    Match(LexicalAnalyzer.leftpar);
                    ParseExpression();
                    Match(LexicalAnalyzer.rightpar);
                    break;
                default:
                    InputOutput.Error(ErrorCodes.INVALID_FACTOR, lexer.TokenPosition);
                    currentToken = lexer.NextToken();
                    break;
            }
        }
    }
}
