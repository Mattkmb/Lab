using System;
using System.Windows;
using Project2;

namespace Project2
{
    public partial class MainWindow : Window
    {
        // Глобальная переменная для хранения текущей суммы
        private Money currentMoney;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Установить сумму". Считывается ввод рублей и копеек,
        /// создаётся объект Money и отображается текущее значение.
        /// </summary>
        private void SetMoney_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!uint.TryParse(txtRubles.Text, out uint rubles))
                {
                    MessageBox.Show("Неверное значение рублей. Введите целое неотрицательное число.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!byte.TryParse(txtKopeks.Text, out byte kopeks) || kopeks >= 100)
                {
                    MessageBox.Show("Неверное значение копеек. Введите число от 0 до 99.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentMoney = new Money(rubles, kopeks);
                txtCurrentMoney.Text = currentMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при установке суммы: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить". Считывается количество копеек для добавления,
        /// выполняется операция сложения и обновляется отображаемое значение.
        /// </summary>
        private void AddKopeks_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney == null)
            {
                MessageBox.Show("Сначала установите начальную сумму.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (!uint.TryParse(txtAddKopeks.Text, out uint addValue))
                {
                    MessageBox.Show("Недопустимое значение для добавления копеек.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentMoney = currentMoney + addValue; // Перегруженный оператор +
                txtCurrentMoney.Text = currentMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении копеек: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вычесть". Считывается количество копеек для вычитания,
        /// выполняется операция вычитания и обновляется отображаемое значение.
        /// </summary>
        private void SubtractKopeks_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney == null)
            {
                MessageBox.Show("Сначала установите начальную сумму.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                if (!uint.TryParse(txtSubKopeks.Text, out uint subValue))
                {
                    MessageBox.Show("Недопустимое значение для вычитания копеек.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentMoney = currentMoney - subValue; // Перегруженный оператор -
                txtCurrentMoney.Text = currentMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при вычитании копеек: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Увеличить (++)". Применяется унарный оператор ++.
        /// </summary>
        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney == null)
            {
                MessageBox.Show("Сначала установите начальную сумму.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                currentMoney++;
                txtCurrentMoney.Text = currentMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при увеличении суммы: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Уменьшить (--)". Применяется унарный оператор --.
        /// </summary>
        private void Decrement_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney == null)
            {
                MessageBox.Show("Сначала установите начальную сумму.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                currentMoney--;
                txtCurrentMoney.Text = currentMoney.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при уменьшении суммы: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Преобразовать". Выполняется явное и неявное преобразование
        /// объекта Money и выводятся результаты преобразований.
        /// </summary>
        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney == null)
            {
                MessageBox.Show("Сначала установите начальную сумму.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                uint rublesPart = (uint)currentMoney; // Явное преобразование
                double fraction = currentMoney;       // Неявное преобразование
                txtExplicit.Text = rublesPart.ToString();
                txtImplicit.Text = fraction.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при преобразовании типов: " + ex.Message,
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
