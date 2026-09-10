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

        }
    }
}
