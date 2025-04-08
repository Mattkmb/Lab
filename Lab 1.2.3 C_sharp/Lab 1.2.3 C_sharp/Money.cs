using System;

namespace Project2
{
    public class Money
    {
        private uint rubles;
        private byte kopeks; 

        // Конструктор по умолчанию
        public Money()
        {
            rubles = 0;
            kopeks = 0;
        }

        // Конструктор с параметрами
        public Money(uint rubles, byte kopeks)
        {
            if (kopeks >= 100)
                throw new ArgumentException("Копеек должно быть меньше 100.");
            this.rubles = rubles;
            this.kopeks = kopeks;
        }

        // Конструктор копирования
        public Money(Money other)
        {
            rubles = other.rubles;
            kopeks = other.kopeks;
        }

        // Метод ввода денежной суммы
        public void InputMoney()
        {
            Console.Write("Введите рубли (целое неотрицательное число): ");
            string input = Console.ReadLine();
            uint r;
            while (!uint.TryParse(input, out r))
            {
                Console.WriteLine("Ошибка ввода! Введите корректное число рублей.");
                Console.Write("Введите рубли: ");
                input = Console.ReadLine();
            }

            Console.Write("Введите копейки (от 0 до 99): ");
            input = Console.ReadLine();
            byte k;
            while (!byte.TryParse(input, out k) || k >= 100)
            {
                Console.WriteLine("Ошибка ввода! Копейки должны быть от 0 до 99.");
                Console.Write("Введите копейки: ");
                input = Console.ReadLine();
            }
            rubles = r;
            kopeks = k;
        }

        // Метод для добавления копеек
        public void AddKopeks(uint additionalKopeks)
        {
            uint totalKopeks = rubles * 100 + kopeks + additionalKopeks;
            rubles = totalKopeks / 100;
            kopeks = (byte)(totalKopeks % 100);
        }

        // Переопределение метода ToString() 
        public override string ToString()
        {
            return $"{rubles} руб. {kopeks:00} коп.";
        }

        // Перегрузка бинарного оператора + для сложения с беззнаковым числом (копейки)
        public static Money operator +(Money m, uint addKopeks)
        {
            Money result = new Money(m);
            result.AddKopeks(addKopeks);
            return result;
        }

        // Перегрузка бинарного оператора + 
        public static Money operator +(uint addKopeks, Money m)
        {
            return m + addKopeks;
        }

        // Перегрузка бинарного оператора – для вычитания копеек
        private static Money operator -(Money m, uint subKopeks)
        {
            uint total = m.rubles * 100 + m.kopeks;
            total = total < subKopeks ? 0 : total - subKopeks;
            return new Money(total / 100, (byte)(total % 100));
        }

        // Перегрузка бинарного оператора – для случая, когда число слева
        public static Money operator -(uint subKopeks, Money m)
        {
            uint moneyTotal = m.rubles * 100 + m.kopeks;
            if (subKopeks < moneyTotal)
                return new Money(0, 0);
            uint diff = subKopeks - moneyTotal;
            return new Money(diff / 100, (byte)(diff % 100));
        }

        // Унарный оператор ++
        public static Money operator ++(Money m)
        {
            m.AddKopeks(1);
            return m;
        }

        // вычитание 1 копейки 
        public static Money operator --(Money m)
        {
            uint total = m.rubles * 100 + m.kopeks;
            if (total > 0)
            {
                total--;
                m.rubles = total / 100;
                m.kopeks = (byte)(total % 100);
            }
            return m;
        }

        // Операция явного преобразования к uint
        public static explicit operator uint(Money m)
        {
            return m.rubles;
        }

        // Операция неявного преобразования к double
        public static implicit operator double(Money m)
        {
            return (double)m.kopeks / 100.0;
        }
    }
}
