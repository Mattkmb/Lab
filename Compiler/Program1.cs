using System;
using System.IO;

namespace Компилятор
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePath = "test.pas";

                if (!System.IO.File.Exists(filePath))
                {
                    Console.WriteLine($"Файл {filePath} не найден!");
                    Console.WriteLine("Рабочая директория: " + Directory.GetCurrentDirectory());
                    return;
                }

                // Инициализация ввода-вывода
                InputOutput.File = new StreamReader(filePath);
                InputOutput.line = "";
                InputOutput.err = new List<Err>();
                InputOutput.positionNow = new TextPosition(1, 0);

                LexicalAnalyzer lexer = new LexicalAnalyzer();
                Parser parser = new Parser(lexer);

                Console.WriteLine("Начинаем компиляцию...\n");
                parser.ParseProgram();

                Console.WriteLine("\nРезультат:");
                Console.WriteLine($"- Строк обработано: {InputOutput.positionNow.lineNumber}");
                Console.WriteLine($"- Ошибок: {InputOutput.ErrCount}");
                
                if (InputOutput.ErrCount == 0)
                {
                    Console.WriteLine("Компиляция завершена успешно!");
                }
                else
                {
                    Console.WriteLine("Обнаружены ошибки компиляции!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Критическая ошибка: {ex.Message}");
            }
            finally
            {
                InputOutput.File?.Close();
            }
        }
    }
}
