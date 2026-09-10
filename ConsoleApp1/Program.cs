using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            while (true)
            {
                Console.Write("Введите количество операций (от 2 до 40): ");
                if (int.TryParse(Console.ReadLine(), out count) && count >= 2 && count <= 40)
                {
                    break;
                }
                Console.WriteLine("Ошибка! Нужно ввести целое число от 2 до 40.");
            }

            string[] names = new string[count];
            double[] prices = new double[count];
            
            Console.WriteLine("\nВводите траты по шаблону: (Название; Цена)");
            Console.WriteLine("Пример: (Влажные салфетки \"Лента\"; 235)");

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    Console.Write($"Запись {i + 1}: ");
                    string input = Console.ReadLine();

                    try
                    {
                       
                        string trimmed = input.Trim().TrimStart('(').TrimEnd(')');
                        string[] parts = trimmed.Split(';');

                        if (parts.Length != 2)
                        {
                            throw new Exception("Неверный формат. Используйте точку с запятой ';'.");
                        }

                        string name = parts[0].Trim();
                        double price = double.Parse(parts[1].Trim());

                        if (price < 0)
                        {
                            throw new Exception("Цена не может быть отрицательной.");
                        }

                        names[i] = name;
                        prices[i] = price;
                        break; 
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка ввода: {ex.Message} Попробуйте снова.");
                    }
                }
            }
            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
                Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт меню: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintData(names, prices);
                        break;

                    case "2":
                        ShowStatistics(prices);
                        break;

                    case "3":
                        BubbleSort(names, prices);
                        Console.WriteLine("Сортировка успешно выполнена!");
                        PrintData(names, prices);
                        break;

                    case "4":
                        ConvertCurrency(prices);
                        break;

                    case "5":
                        SearchByName(names, prices);
                        break;

                    case "0":
                        Console.WriteLine("Программа завершена. До свидания!");
                        return;

                    default:
                        Console.WriteLine("Неверный пункт меню. Попробуйте еще раз.");
                        break;
                }
            }
        }
        static void PrintData(string[] names, double[] prices)
        {
            Console.WriteLine("\nСписок зарегистрированных трат:");
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]} — {prices[i]} руб.");
            }
        }
        static void ShowStatistics(double[] prices)
        {
            double sum = 0;
            double max = prices[0];
            double min = prices[0];

            for (int i = 0; i < prices.Length; i++)
            {
                sum += prices[i];
                if (prices[i] > max) max = prices[i];
                if (prices[i] < min) min = prices[i];
            }

            double average = sum / prices.Length;

            Console.WriteLine("\n--- СТАТИСТИКА ТРАТ ---");
            Console.WriteLine($"Сумма всех трат:  {sum} руб.");
            Console.WriteLine($"Среднее значение: {average:F2} руб.");
            Console.WriteLine($"Максимальная:     {max} руб.");
            Console.WriteLine($"Минимальная:      {min} руб.");
        }
        static void BubbleSort(string[] names, double[] prices)
        {
            int n = prices.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (prices[j] > prices[j + 1])
                    {
                        double tempPrice = prices[j];
                        prices[j] = prices[j + 1];
                        prices[j + 1] = tempPrice;

                        string tempName = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = tempName;
                    }
                }
            }
        }
        static void ConvertCurrency(double[] prices)
        {
            double rate = 0;
            Console.WriteLine("\nВыберите вариант задания курса:");
            Console.WriteLine("1. Выбрать из списка (USD: 90, EUR: 100, CNY: 13)");
            Console.WriteLine("2. Ввести свой курс вручную");
            Console.Write("Ваш выбор: ");
            string mode = Console.ReadLine();

            if (mode == "1")
            {
                Console.WriteLine("Выберите валюту: 1 - USD (90), 2 - EUR (100), 3 - CNY (13)");
                string currency = Console.ReadLine();
                if (currency == "1") rate = 90;
                else if (currency == "2") rate = 100;
                else if (currency == "3") rate = 13;
                else
                {
                    Console.WriteLine("Неверный выбор. Отмена конвертации.");
                    return;
                }
            }
            else if (mode == "2")
            {
                Console.Write("Введите курс валюты к рублю (сколько рублей в 1 ед. валюты): ");
                if (!double.TryParse(Console.ReadLine(), out rate) || rate <= 0)
                {
                    Console.WriteLine("Некорректный курс. Отмена конвертации.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Неверный вариант.");
                return;
            }

            Console.WriteLine($"\nПересчет стоимости (по курсу {rate} руб.):");
            for (int i = 0; i < prices.Length; i++)
            {
                double converted = prices[i] / rate;
                Console.WriteLine($"Операция {i + 1}: {prices[i]} руб. = {converted:F2} ед. валюты");
            }
        }
    }
}
