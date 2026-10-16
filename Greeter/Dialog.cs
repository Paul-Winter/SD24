namespace Greeter
{
    /// <summary>
    /// Обеспечивает короткий диалог с пользователем
    /// </summary>
    public class Dialog
    {
        private string name;
        private int age;
        /// <summary>
        /// Приветствует пользователя
        /// </summary>
        public void SayHello()
        {
            Console.WriteLine("Привет, как тебя зовут?");
        }
        /// <summary>
        /// Возвращает имя пользователя
        /// </summary>
        /// <returns>string name</returns>
        public string GetName()
        {
            name = Console.ReadLine();
            return name;
        }
        /// <summary>
        /// Запрашивает дату рождения пользователя
        /// </summary>
        public void SayBirthday()
        {
            Console.Write($"Привет, {name}! Введи дату своего рождения: ");
        }
        /// <summary>
        /// Принимает дату рождения пользователя
        /// </summary>
        /// <returns>int возраст</returns>
        public int GetBirthday()
        {
            DateTime birthday = DateTime.Parse(Console.ReadLine());
            age = (int)((DateTime.Now - birthday).TotalDays / 365);
            return age;
        }
        /// <summary>
        /// Сообщает возраст пользователя
        /// </summary>
        public void SayAge()
        {
            Console.WriteLine($"{name} тебе {age} лет.");
        }
        /// <summary>
        /// Прощается с пользователем
        /// </summary>
        public void SayBye()
        {
            Console.WriteLine($"До встречи, {name}.");
        }
    }
}
