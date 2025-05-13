using System;

namespace Project2
{
    /// <summary>
    /// Класс Money реализует денежную сумму с представлением рублей и копеек.
    /// Предусмотрена перегрузка операторов для:
    /// + (сложение копеек),
    /// - (вычитание копеек),
    /// ++ и -- (увеличение/уменьшение на 1 копейку),
    /// а также операции преобразования типов.
    /// </summary>
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
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            rubles = other.rubles;
            kopeks = other.kopeks;
        }

        // Метод для добавления копеек
        public void AddKopeks(uint additionalKopeks)
        {
            uint totalKopeks = rubles * 100 + kopeks + additionalKopeks;
            rubles = totalKopeks / 100;
            kopeks = (byte)(totalKopeks % 100);
        }

        // Переопределение метода ToString() для вывода суммы
        public override string ToString()
        {
            return $"{rubles} руб. {kopeks:00} коп.";
        }

        // Перегрузка оператора + для добавления копеек
        public static Money operator +(Money m, uint addKopeks)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            Money result = new Money(m);
            result.AddKopeks(addKopeks);
            return result;
        }

        // Перегрузка оператора + (коммутативное действие)
        public static Money operator +(uint addKopeks, Money m)
        {
            return m + addKopeks;
        }

        // Перегрузка оператора - для вычитания копеек
        public static Money operator -(Money m, uint subKopeks)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            uint total = m.rubles * 100 + m.kopeks;
            total = total < subKopeks ? 0 : total - subKopeks;
            return new Money(total / 100, (byte)(total % 100));
        }

        // Перегрузка оператора - для случая, когда число слева
        public static Money operator -(uint subKopeks, Money m)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            uint moneyTotal = m.rubles * 100 + m.kopeks;
            if (subKopeks < moneyTotal)
                return new Money(0, 0);
            uint diff = subKopeks - moneyTotal;
            return new Money(diff / 100, (byte)(diff % 100));
        }

        // Унарный оператор ++
        public static Money operator ++(Money m)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            m.AddKopeks(1);
            return m;
        }

        // Унарный оператор --
        public static Money operator --(Money m)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            uint total = m.rubles * 100 + m.kopeks;
            if (total > 0)
            {
                total--;
                m.rubles = total / 100;
                m.kopeks = (byte)(total % 100);
            }
            return m;
        }

        // Явное преобразование к uint – возвращает рубли
        public static explicit operator uint(Money m)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            return m.rubles;
        }

        // Неявное преобразование к double – возвращает дробную часть копеек в рублях
        public static implicit operator double(Money m)
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            return (double)m.kopeks / 100.0;
        }
    }
}
