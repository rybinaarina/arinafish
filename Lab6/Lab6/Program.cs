using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class Program : DeleGate
    {
        public static void TypeInfo()
        {
            EXforRef obj = new EXforRef();
            Type t = obj.GetType();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nКонструкторы:");
            Console.ResetColor();
            foreach (var x in t.GetConstructors())
            {
                Console.WriteLine(x);
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nМетоды:");
            Console.ResetColor();
            foreach (var x in t.GetMethods())
            {
                Console.WriteLine(x);
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nСвойства:");
            Console.ResetColor();
            foreach (var x in t.GetProperties())
            {
                Console.WriteLine(x);
            }

        }
        /// <summary>
        /// Пример использования метода InvokeMember
        /// </summary>
        static void InvokeMemberInfo()
        {
            Type t = typeof(EXforRef);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nВызов метода:");
            Console.ResetColor();

            //Создание объект через рефлексию
            EXforRef fi = (EXforRef)t.InvokeMember(null, BindingFlags.CreateInstance, null, null, new object[] { });

            //Параметры вызова метода
            object[] parameters = new object[] { 3, 2 };
            //Вызов метода
            object Result = t.InvokeMember("Plus", BindingFlags.InvokeMethod, null, fi, parameters);
            Console.WriteLine("Plus(3,2)={0}", Result);
        }
        /// <summary>
        /// Проверка, что у свойства есть атрибут заданного типа
        /// </summary>
        /// <returns>Значение атрибута</returns>
        public static bool GetPropertyAttribute(PropertyInfo checkType, Type attributeType, out object attribute)
        {
            bool Result = false;
            attribute = null;

            //Поиск атрибутов с заданным типом
            var isAttribute = checkType.GetCustomAttributes(attributeType, false);
            if (isAttribute.Length > 0)
            {
                Result = true;
                attribute = isAttribute[0];
            }

            return Result;
        }

        /// <summary>
        /// Работа с атрибутами
        /// </summary>
        static void AttributeInfo()
        {
            Type t = typeof(EXforRef);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nСвойства, помеченные атрибутом:");
            Console.ResetColor();
            foreach (var x in t.GetProperties())
            {
                object attrObj;
                if (GetPropertyAttribute(x, typeof(NewAttribute), out attrObj))
                {
                    NewAttribute attr = attrObj as NewAttribute;
                    Console.WriteLine(x.Name + " - " + attr.Description);
                }
            }
        }

        static void Main(string[] args)
        {
            //ПРИМЕР ИСПОЛЬЗОВАНИЯ "Delegate"//
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n----------ПРИМЕР ИСПОЛЬЗОВАНИЯ Delegate----------");
            Console.ResetColor();
            #region
            double p1 = -12.44;
            float p2 = -14.3f;
            int p3 = 200;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Возьмем: p1= {p1} p2= {p2} p3= {p3}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nПример с методами в виде переменных:");
            Console.ResetColor();
            PosFun("Все ли числа положительны? ", p1, p2, p3, AllPositive_YorN);
            PosFun("Положительно ли произведение? ", p1, p2, p3, CompositionPos_YorN);

            Delegate1 dg1 = (double x, float y, int z) =>// проверим на положительность сумму
            {
                if (x + y + z > 0)
                    return true;
                else return false;
            };

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\nПример с лямбда-выражением в виде переменной:");
            Console.ResetColor();
            PosFun("Положительна ли сумма?: ", p1, p2, p3, dg1);

            double f1 = -123.238723;
            float f2 = -1.1f;
            int f3 = -987;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВозьмем: f1= {f1} f2= {f2} f3= {f3}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nИспользование обощенного делегата Func<>");
            Console.ResetColor();
            PosFunc1("Все ли числа положительны?", f1, f2, f3, AllPositive_YorN);
            PosFunc1("Положительно ли произведение?", f1, f2, f3, CompositionPos_YorN);

            double a1 = -12.238723;
            float a2 = -0.5f;
            int a3 = -98;
            double A1 = -72.333333;
            float A2 = -111.1f;
            int A3 = -7;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВозьмем: a1= {a1} a2= {a2} a3= {a3} A1= {A1} A2= {A2} A3= {A3}");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nИспользование обощенного делегата Action<>");
            Console.ResetColor();
            Action<double, float, int> param;
            param = ActionTest;
            Console.WriteLine("При использование значений а1, а2, а3:");
            PosFunc2(a1, a2, a3, param);
            Console.WriteLine("\nПри использование значений А1, А2, А3:");
            PosFunc2(A1, A2, A3, param);
            #endregion

            //ПРИМЕР ИСПОЛЬЗОВАНИЯ "REFLECTION"//
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n----------ПРИМЕР ИСПОЛЬЗОВАНИЯ Reflection----------");
            Console.ResetColor();
            TypeInfo();
            InvokeMemberInfo();
            AttributeInfo();

            Console.ReadLine();

        }
    }
}
