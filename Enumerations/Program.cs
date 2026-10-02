namespace Enumerations
{
    internal class Program
    {
        enum EnumsName { item1, item2, itemN };
        enum Students
        {
            Akobyan = 1,
            Grishin = 10,
            Zateev = 100,
            Levchenko = 101,
            Pavlyukevich,
            Rosohay,
            Skripan = 110,
            Totskih = 1000,
            Tyupenkin = 1010,
            Shalnev,
            Shimohin_A,
            Shimohin_K = 1100,
            Yartseva,
            Kobozev
        };

        public enum ConveyorAction
        {
            Start,
            Pause,
            Stop,
            Forward,
            Reverse
        };
        public static void ConveyorController(ConveyorAction action)
        {
            switch (action)
            {
                case ConveyorAction.Start:
                    Console.WriteLine("Запустить конвейер");
                    break;
                case ConveyorAction.Pause:
                    Console.WriteLine("Приостановить конвейер");
                    break;
                case ConveyorAction.Stop:
                    Console.WriteLine("Остановить конвейер");
                    break;
                case ConveyorAction.Forward:
                    Console.WriteLine("Переместить конвейер вперёд");
                    break;
                case ConveyorAction.Reverse:
                    Console.WriteLine("Переместить конвейер назад");
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"{EnumsName.item1} имеет значение {(int)EnumsName.item1}");
            Console.WriteLine($"{EnumsName.item2} имеет значение {(int)EnumsName.item2}");
            Console.WriteLine($"{EnumsName.itemN} имеет значение {(int)EnumsName.itemN}");

            while (true)
            {
                ConveyorController(ConveyorAction.Start);
                ConveyorController(ConveyorAction.Pause);
                ConveyorController(ConveyorAction.Forward);
                ConveyorController(ConveyorAction.Forward);
                ConveyorController(ConveyorAction.Reverse);
                ConveyorController(ConveyorAction.Start);
                ConveyorController(ConveyorAction.Stop);

                Console.WriteLine("Введите номер студента:");
                int num = Int32.Parse(Console.ReadLine());
                switch (num)
                {
                    case (int)Students.Akobyan:
                        Console.WriteLine($"Привет, {Students.Akobyan}\t{(int)Students.Akobyan}");
                        break;
                    case (int)Students.Grishin:
                        Console.WriteLine($"Привет, {Students.Grishin}");
                        break;
                    case (int)Students.Zateev:
                        Console.WriteLine($"Привет, {Students.Zateev}");
                        break;
                    case (int)Students.Levchenko:
                        Console.WriteLine($"Привет, {Students.Levchenko}");
                        break;
                    case (int)Students.Pavlyukevich:
                        Console.WriteLine($"Привет, {Students.Pavlyukevich}");
                        break;
                    case (int)Students.Rosohay:
                        Console.WriteLine($"Привет, {Students.Rosohay}");
                        break;
                    case (int)Students.Skripan:
                        Console.WriteLine($"Привет, {Students.Skripan}");
                        break;
                    case (int)Students.Totskih:
                        Console.WriteLine($"Привет, {Students.Totskih}");
                        break;
                    case (int)Students.Tyupenkin:
                        Console.WriteLine($"Привет, {Students.Tyupenkin}");
                        break;
                    case (int)Students.Shalnev:
                        Console.WriteLine($"Привет, {Students.Shalnev}");
                        break;
                    case (int)Students.Shimohin_A:
                        Console.WriteLine($"Привет, {Students.Shimohin_A}");
                        break;
                    case (int)Students.Shimohin_K:
                        Console.WriteLine($"Привет, {Students.Shimohin_K}");
                        break;
                    case (int)Students.Yartseva:
                        Console.WriteLine($"Привет, {Students.Yartseva}");
                        break;
                    case (int)Students.Kobozev:
                        Console.WriteLine($"Привет, {Students.Kobozev}");
                        break;
                    default:
                        Console.WriteLine("ОШИБКА! Нет такого студента!");
                        break;
                }
            }
        }
    }
}
