namespace Компилятор
{
    public static class ErrorCodes
    {
        // Лексические ошибки
        public const byte UNRECOGNIZED_CHAR = 1;
        public const byte INT_CONST_OVERFLOW = 203;
        public const byte INVALID_REAL_NUMBER = 204;
        public const byte EMPTY_CHAR_CONST = 205;
        public const byte UNCLOSED_CHAR_CONST = 206;

        // Синтаксические ошибки
        public const byte UNEXPECTED_TOKEN = 207;
        public const byte UNKNOWN_STATEMENT = 208;
        public const byte INVALID_FACTOR = 209;
        public const byte UNEXPECTED_TOKEN_TYPE = 210;
        public const byte MISSING_TO_DOWNTO = 211;
        public const byte EXPECTED_IDENTIFIER = 212;
        public const byte UNEXPECTED_END_OF_FILE = 213;
        public const byte MISSING_PROGRAM_DOT = 214;
        public const byte TEXT_AFTER_PROGRAM_END = 215;
        public const byte EXPECTED_BEGIN = 216;
        public const byte EXPECTED_END = 217;

        // Системные ошибки
        public const byte MAX_ERRORS_REACHED = 255;

        public static string GetErrorMessage(byte errorCode)
        {
            return errorCode switch
            {
                UNRECOGNIZED_CHAR => "Неопознанный символ",
                INT_CONST_OVERFLOW => "Целочисленная константа превышает максимальное значение",
                INVALID_REAL_NUMBER => "Недопустимый формат вещественного числа",
                EMPTY_CHAR_CONST => "Пустая символьная константа",
                UNCLOSED_CHAR_CONST => "Незакрытая символьная константа",
                UNEXPECTED_TOKEN => "Ожидался другой токен",
                UNKNOWN_STATEMENT => "Неизвестный оператор",
                INVALID_FACTOR => "Недопустимый элемент выражения",
                UNEXPECTED_TOKEN_TYPE => "Ожидался другой тип токена",
                MISSING_TO_DOWNTO => "Отсутствует 'to' или 'downto' в цикле for",
                EXPECTED_IDENTIFIER => "Ожидался идентификатор",
                UNEXPECTED_END_OF_FILE => "Неожиданный конец файла",
                MISSING_PROGRAM_DOT => "Отсутствует завершающая точка программы",
                TEXT_AFTER_PROGRAM_END => "Текст после завершения программы",
                EXPECTED_BEGIN => "Ожидалось ключевое слово 'begin'",
                EXPECTED_END => "Ожидалось ключевое слово 'end'",
                MAX_ERRORS_REACHED => "Достигнуто максимальное количество ошибок",
                _ => $"Неизвестная ошибка (код {errorCode})"
            };
        }
    }
}
