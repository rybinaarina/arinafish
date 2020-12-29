using System;


namespace Lab6
{
    delegate bool Delegate1(double p1, float p2, int p3);

    class DeleGate
    {
        public static bool AllPositive_YorN(double p1, float p2, int p3)//  проверка на подложительность всех чисел
        {
            if (p1 > 0 && p2 > 0 && p3 > 0)
                return true;
            else
                return false;
        }

        public static bool CompositionPos_YorN(double p1, float p2, int p3)// проверка на положительность произведения
        {
            if (p1 * p2 * p3 > 0)
                return true;
            else
                return false;
        }

        public static void ActionTest(double i1, float i2, int i3)
        {
            Console.WriteLine("Сумма чисел: " + (i1 + i2 + i3));
        }

        public static void PosFun(string str, double p1, float p2, int p3, Delegate1 Param)
        {
            bool result = Param(p1, p2, p3);
            Console.WriteLine(str + result.ToString());
        }

        public static void PosFunc1(string str, double i1, float i2, int i3, Func<double, float, int, bool> param)
        {
            bool Result = param(i1, i2, i3);
            Console.WriteLine(str + Result.ToString());

        }

        public static void PosFunc2(double i1, float i2, int i3, Action<double, float, int> param)
        {
            if (Math.Abs(i1) > 100 || Math.Abs(i2) > 100 || Math.Abs(i3) > 100)
                param(i1, i2, i3);
            else Console.WriteLine("Все значения меньше 100. Считайте сумму сами!");

        }
    }
}
