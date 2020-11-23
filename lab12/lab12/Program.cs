using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab12
{
    public partial class Phone
    {
        private string surname;
        private string name;
        private string adress;
        public string Surname { get { return this.surname; } set { this.surname = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public string Adress { get { return this.adress; } set { this.adress = value; } }
        public Phone(string Name, string Surname, string Adress)
        {
            name = Name;
            surname = Surname;
            adress = Adress;
        }
    }
    interface ITVprogram
    {
        void Watch();
    }
    abstract class TVprogram
    {
        public int agelimit=18;
        public double time;
    }

    class Film : TVprogram, ITVprogram
    {
        public string name;
        public int year;
        public int limit = 16;
        public string Name { get { return this.name; } set { this.name = value; } }
        public int Year { get { return this.year; } set { this.year = value; } }
        public Film() { }
        public Film(string name, int year, double time, int agelimit)
        {
            this.year = year;
            this.name = name;
            this.time = time;
            this.agelimit = agelimit;
        }
        public void Watch()
        {
            if (agelimit > limit)
            {
                Console.WriteLine("Вам разрешено смотреть этот фильм");
            }
            else
            {
                Console.WriteLine("Вам рано еще смотреть этот фильм");
            }
        }
        public override string ToString()
        {
            return $"Возрастное ограничение на просмотр этого фильма {agelimit}";
        }
        public void Show(string s)
        {
            Console.WriteLine(s);
        }
    }

    class Reflector
    {
        public void AllClass(string str) //Всё содержимое класса
        {
            Type type = Type.GetType(str);
            var all = type.GetMembers();
            StreamWriter write = new StreamWriter(@"D:\proga\Lab12\1.txt", true);
            write.WriteLine($"Все содержимое класса {type}:");
            foreach (var info in all)
            {
                write.WriteLine(info.MemberType + " - " + info.Name);
            }
            write.WriteLine();
            Console.WriteLine("Информация записана в файл!");
            write.Close();
        }

        public void PublicMethods(string str) // Публичные методы 
        {
            Type type = Type.GetType(str);
            var methods = type.GetMethods();
            StreamWriter write = new StreamWriter(@"D:\proga\Lab12\2.txt", true);
            write.WriteLine($"Все публичные методы класса {type}:");
            foreach (var info in methods)
            {
                    write.WriteLine("Method Name - " + info.Name + ". Method Return Type - " + info.ReturnType);
            }
            write.WriteLine();
            Console.WriteLine("Информация записана в файл!");
            write.Close();
        }

        public void FieldsAndProperties(string str) // Информацию о полях и свойствах
        {
            Type type = Type.GetType(str);
            var fields = type.GetFields();
            var properties = type.GetProperties();
            StreamWriter write = new StreamWriter(@"D:\proga\Lab12\3.txt", true);
            write.WriteLine($"Вся информация о полях и свойствах {type}:");
            foreach (var info in fields)
            {
                write.WriteLine("Type - " + info.MemberType + ". Name - " + info.Name);
            }
            foreach (var info in properties)
            {
                write.WriteLine("Type - " + info.MemberType + ". Name - " + info.Name);
            }
            write.WriteLine();
            Console.WriteLine("Информация записана в файл!");
            write.Close();
        }

        public void Interf(string str) // Реализованные интерфейсы
        {
            Type type = Type.GetType(str);
            var interfaces = type.GetInterfaces();
            StreamWriter write = new StreamWriter(@"D:\proga\Lab12\4.txt", true);
            write.WriteLine($"Все реализованный классом интерфейсы {type}:");
            foreach (var info in interfaces)
            {
                write.WriteLine("Name - " + info.Name);
            }
            write.WriteLine();
            Console.WriteLine("Информация записана в файл!");
            write.Close();
        }

        public void ParMethod(string str)//по имени класса параметры
        {
            Type type = Type.GetType(str);
            Console.Write("Ввести тип параметра: ");
            string parametrS = Console.ReadLine();
            var methodsP = type.GetMethods();
            Console.WriteLine($"Имена методов, содержащих параметр типа {parametrS}: ");
            foreach (var info in methodsP)
            {
                foreach (var i in info.GetParameters())
                    if (i.ParameterType.Name.ToLower() == parametrS)
                    {
                        Console.WriteLine("Name - " + info.Name);
                    }
            }
        }
        public void ReadMethod(string str, string method)
        {
            Type type = Type.GetType(str);
            StreamReader reading = new StreamReader(@"D:\proga\Lab12\5.txt");
            string parametrs = reading.ReadLine();
            var Method = type.GetMethod(method);
            object obj = Activator.CreateInstance(type);
            object result = Method.Invoke(obj, new object[] { parametrs });
            Console.WriteLine((result));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Reflector reflector = new Reflector();
            reflector.AllClass("lab12.Film");
            reflector.PublicMethods("lab12.Film");
            reflector.FieldsAndProperties("lab12.Film");
            reflector.Interf("lab12.Film");
            reflector.ParMethod("lab12.Film");
            reflector.ReadMethod("lab12.Film", "Show");

            reflector.AllClass("lab12.Phone");
            reflector.PublicMethods("lab12.Phone");
            reflector.FieldsAndProperties("lab12.Phone");
            reflector.Interf("lab12.Phone");
            reflector.ParMethod("lab12.Phone");
        }
    }
}


