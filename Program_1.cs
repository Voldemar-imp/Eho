using System;

namespace Eho
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Джуффин Халли", "сэр Макс", "Мелифаро Младший", "Кофа Йох", "Шурф Лонли-Локли", "Меламори Блимм", "Луукфи Пэнц" };
            string[] posts = { "Почтеннейший Начальник", "Ночное Лицо Почтеннейшего Начальника",
                "Дневное Лицо Почтеннейшего Начальника", "Мастер Слышащий",
                "Мастер Пресекающий Ненужные Жизни", "Мастер Преследования Затаившихся и Бегущих",
                "Мастер Хранитель Знаний" };
            bool isMenuWork = true; 

            while (isMenuWork)
            {
                Console.Clear();
                Console.WriteLine("Досье. Малое Тайное Сыскное Войско г.Ехо. (Персонажи книг Макса Фрая)");
                Console.WriteLine("1) добавить досье \n2) вывести все досье " +
                    " \n3) удалить досье \n4) поиск по имени/фамилии \n5) выход");
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                switch (key.KeyChar)
                {
                    case '1':                        
                        AddDossier(ref names, ref posts);
                        break;
                    case '2':
                        WriteDossiers(names, posts);
                        break;
                    case '3':                        
                        DeleteDossier(ref names, ref posts);
                        break;
                    case '4':                        
                        FindName(names, posts);
                        break;
                    case '5':
                        isMenuWork = false;
                        break;
                        default:
                        СhangeMessageСolor("Неверно выбрана команда. Для продолжения нажмите любую клавишу...");
                        break;
                }         
                
                Console.ReadKey(true);                          
            }           
        }

        static void AddDossier(ref string[] nameArray, ref string[] postArray)
        {
            Console.WriteLine("Добавить досье.");
            Console.WriteLine("Введите имя:");
            string addName = Console.ReadLine();
            Console.WriteLine("Введите должность:");
            string addPost = Console.ReadLine();
            Console.WriteLine("Введите под каким номером вы хотите вставить досье:");
            int indexToAdd = InputIndex(nameArray.Length +1);       
            nameArray = AddToArray (nameArray, addName, indexToAdd);
            postArray = AddToArray(postArray, addPost, indexToAdd);
        }

        static string[] AddToArray(string[] array, string addValue, int indexToAdd)
        {
            string[] tempArray = new string[array.Length + 1];            
            int counter = 0;

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (i == indexToAdd)
                {
                    tempArray[i] = addValue;
                    continue;
                }
                tempArray[i] = array[counter];                
                counter++;
            }

            return tempArray;
        }

        static void WriteDossiers(string[] nameArray, string[] postArray)
        {
            Console.WriteLine("Досье. Малое Тайное Сыскное Войско г.Ехо. (Персонажи книг Макса Фрая)");
            Console.WriteLine("№. Имя - Должность");

            for (int i = 0; i < nameArray.Length; i++)
            {
                WriteIndexNameAndPost(i + 1, nameArray[i], postArray[i]);
            }
        }

        static void DeleteDossier(ref string[] nameArray, ref string[] postArray)
        {
            if (nameArray.Length == 0)
            {
                СhangeMessageСolor("Ошибка! База данных пуста!");
                return;
            }
                        
            Console.WriteLine("Введите номер досье для удаления:");
            int indexDelete = InputIndex(nameArray.Length);
            nameArray = DeleteInArray (nameArray, indexDelete);
            postArray = DeleteInArray(postArray, indexDelete);
        }

        static string[] DeleteInArray(string[] array, int indexToDelete)
        {
            string[] tempArray = new string[array.Length - 1];
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (i == indexToDelete)
                {
                    continue;
                }
                tempArray[counter] = array[i];
                counter++;
            }

            return tempArray;
        }

        static void FindName(string[] nameArray, string[] postArray)
        {
            Console.WriteLine("Найти досье");
            Console.WriteLine("Введите имя и/или фамилию:");
            string wantedName = Console.ReadLine();
            bool isWanted = false;

            for (int i = 0; i < nameArray.Length; i++)
            {
                if (nameArray[i].Equals(wantedName))
                {
                    WriteIndexNameAndPost(i + 1, nameArray[i], postArray[i]);
                    isWanted = true;
                }
                else
                {
                    foreach (string word in nameArray[i].Split(' '))
                    {
                        if (word.Equals(wantedName))
                        {
                            WriteIndexNameAndPost(i + 1, nameArray[i], postArray[i]);                           
                            isWanted = true;
                        }
                    }
                }
            }

            if (!isWanted)
            {
                СhangeMessageСolor("Заданное имя - не найдено", ConsoleColor.Yellow);
            }
        }

        static int InputIndex(int arreySize)
        {
            bool indexIsRight = false;
            int index = arreySize;

            while (!indexIsRight)
            {
                index = UserInput() - 1;

                if (index >= 0 && index < arreySize)
                {
                    indexIsRight = true;
                }
                else
                {
                    СhangeMessageСolor("Такого номера не сеществует, попробуете еще раз", ConsoleColor.Yellow);
                }
            }

            return index;
        }

        static int UserInput()
        {
            int index = 0;
            bool isContinue = true;

            while (isContinue)
            {
                string userInput = Console.ReadLine();
                bool success = int.TryParse(userInput, out index);

                if (!success)
                {
                    СhangeMessageСolor($"Введенное значение: '{userInput}' не явлеяется числом, поробуйте еще раз.");                
                }

                isContinue = !success;
            }

            return index;
        }

        static void СhangeMessageСolor(string message, ConsoleColor сolor = ConsoleColor.Red)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = сolor;
            Console.WriteLine(message);
            Console.ForegroundColor = defaultColor;
        }

        static void WriteIndexNameAndPost(int index, string name, string post)
        {
            Console.WriteLine(index + ". " + name + " - " + post);          
        }
    }
}
