using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        string s = "";
        foreach (var inpt in inputs) s += inpt;

        if (string.IsNullOrEmpty(s) || s.Length < 2)
        {
            Console.WriteLine(0);
            return;
        }

        int n = s.Length;

        // Проверяем чётность последней цифры (символ преобразуем в число)
        bool isLastEven = (s[n - 1] - '0') % 2 == 0;

        // Считаем количество чётных цифр среди первых (n - 1) элементов
        int evenCountBeforeLast = 0;
        for (int i = 0; i < n - 1; i++)
        {
            if ((s[i] - '0') % 2 == 0)
            {
                evenCountBeforeLast++;
            }
        }

        int totalWays;
        if (isLastEven)
        {
            // Если последняя чётная: (n - 1) для '*' и evenCountBeforeLast для '+'
            totalWays = (n - 1) + evenCountBeforeLast;
        }
        else
        {
            // Если последняя нечётная: способов всегда ровно n - 1
            totalWays = n - 1;
        }

        Console.WriteLine(totalWays);
    }
}
