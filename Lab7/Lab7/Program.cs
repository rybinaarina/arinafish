using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cортировка по отделу:");
            Console.WriteLine("Cross Join (Inner Join) с использованием Join");
            var q8 = from x in d2
                     join y in d1 on x.ID_записи_об_отделе equals y.ID_записи_об_отделе
                     orderby x.Наименование_отдела ascending
                     select new { v1 = x.Наименование_отдела, v2 = y.Фамилия_сотрудника };
            foreach (var x in q8) Console.WriteLine(x);
           
            //Фамилия с буквы "А"
            Console.WriteLine("\nФамилия с буквы А:");
            foreach (var x in q8)
            {
                string z = x.v2;
                z = z.Remove(1);
                string a = "А";
                int res = String.Compare(z, a);
                if (res == 0) Console.WriteLine(x);
            }

            //количестов сотрудников
            Console.WriteLine("\nКоличество сотрудников:");
            var q11 = from x in d2
                      join y in d1 on x.ID_записи_об_отделе equals y.ID_записи_об_отделе into temp
                      from t in temp
                      select new { v1 = x.Наименование_отдела, cnt = temp.Count() };
           
            foreach (var x in q11) Console.WriteLine(x);
            //отдел все фамилии на А
            Console.WriteLine("\nОтдел все фамилии на А:");
            var q12 = from x in d2
                      join y in d1 on x.ID_записи_об_отделе equals y.ID_записи_об_отделе into temp
                      select new { v1 = x.Наименование_отдела, d2Group = temp };
            foreach (var x in q12)
            {
                bool flag = true;
                int k = 0;
                foreach (var y in x.d2Group)
                {
                    k++;
                    string отд = y.Фамилия_сотрудника;
                    string z = y.Фамилия_сотрудника;
                    z = z.Remove(1);
                    string a = "А";
                    int res = String.Compare(z, a);
                    if (res != 0) flag = false;
                }
                if (flag == true && k > 0)
                    Console.WriteLine(x.v1);
            }

            //отдел хоть одна фамилия на А
            Console.WriteLine("\nОтдел хоть одна фамилия на А:");
            foreach (var x in q12)
            {
                int k = 0;
                foreach (var y in x.d2Group)
                {
                    string отд = y.Фамилия_сотрудника;
                    string z = y.Фамилия_сотрудника;
                    z = z.Remove(1);
                    string a = "А";
                    int res = String.Compare(z, a);
                    if (res == 0) k++;
                }
                if (k > 0)
                    Console.WriteLine(x.v1);
            }

            Console.WriteLine("\nИмитация связи много-ко-многим:");
            var lnk1 = from x in d1
                       join l in lnk on x.ID_записи_о_сотруднике equals l.ID_записи_о_сотруднике into temp
                       from t1 in temp
                       join y in d2 on t1.ID_записи_об_отделе equals y.ID_записи_об_отделе into temp2
                       from t2 in temp2
                       select new { id2 = t2.Наименование_отдела, id1 = x.Фамилия_сотрудника };
            foreach (var x in lnk1) Console.WriteLine(x);
        }
        public class Сотрудник
        {
            public int ID_записи_о_сотруднике;
            public string Фамилия_сотрудника;
            public int ID_записи_об_отделе;
            public Сотрудник(int i, string g, int v)
            {
                this.ID_записи_о_сотруднике = i;
                this.Фамилия_сотрудника = g;
                this.ID_записи_об_отделе = v;
            }
            public override string ToString()
            {
                return "(id=" + this.ID_записи_о_сотруднике.ToString() + "; Фамилия=" + this.Фамилия_сотрудника + "; ID записи об отделе=" + this.ID_записи_об_отделе + ")";
            }

        }
        public class Отдел
        {
            public int ID_записи_об_отделе;
            public string Наименование_отдела;
            public Отдел(int i, string g)
            {
                this.ID_записи_об_отделе = i;
                this.Наименование_отдела = g;
            }
            public override string ToString()
            {
                return "(id Отдел=" + this.ID_записи_об_отделе.ToString() + "; Наименование отдела=" + this.Наименование_отдела + ")";
            }
        }
        public class Сотрудник_отдела
        {
            public int ID_записи_о_сотруднике;
            public int ID_записи_об_отделе;
            public Сотрудник_отдела(int i, int g)
            {
                this.ID_записи_о_сотруднике = i;
                this.ID_записи_об_отделе = g;
            }
            public override string ToString()
            {
                return "(id Отдел=" + this.ID_записи_об_отделе.ToString() + "; Наименование отдела=" + this.ID_записи_о_сотруднике + ")";
            }
        }

        //Связь между списками
        //Пример данных
        static List<Сотрудник> d1 = new List<Сотрудник>()
        {
            new Сотрудник(1, "Рыбина", 3),
            new Сотрудник(3, "Афанасьева", 8),
            new Сотрудник(5, "Мишура", 5),
            new Сотрудник(8, "Алантьев", 3),
            new Сотрудник(6, "Алин", 8),
            new Сотрудник(6, "Аллигрова", 8)
        };
        static List<Отдел> d2 = new List<Отдел>()
        {
            new Отдел(3, "Бухгалтерия"),
            new Отдел(8, "Администрация"),
            new Отдел(5, "Отдел кадров")
        };
        static List<Сотрудник_отдела> lnk = new List<Сотрудник_отдела>()
        {
            new Сотрудник_отдела(1,3),
            new Сотрудник_отдела(1,8),
            new Сотрудник_отдела(1,5),
            new Сотрудник_отдела(3,1),
            new Сотрудник_отдела(3,3),
            new Сотрудник_отдела(3,5),
            new Сотрудник_отдела(5,3),
            new Сотрудник_отдела(5,8),
            new Сотрудник_отдела(5,5),
            new Сотрудник_отдела(6,3),
            new Сотрудник_отдела(6,8),
            new Сотрудник_отдела(6,5),
            new Сотрудник_отдела(8,3),
            new Сотрудник_отдела(8,8),
            new Сотрудник_отдела(8,5),
        };
    }
}