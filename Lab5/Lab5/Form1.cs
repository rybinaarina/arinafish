using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using MyLib;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Список слов
        /// </summary>
        List<string> list = new List<string>();
        // Считывание файла
        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "текстовые файлы|*.txt";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                Stopwatch t = new Stopwatch();
                t.Start();

                //Чтение файла в виде строки
                string text = File.ReadAllText(fd.FileName);

                //Разделительные символы для чтения из файла
                char[] separators = new char[] { ' ', '.', ',', '!', '?', '/', '\t', '\n' };

                string[] textArray = text.Split(separators);

                foreach (string strTemp in textArray)
                {
                    //Удаление пробелов в начале и конце строки
                    string str = strTemp.Trim();
                    //Добавление строки в список, если строка не содержится в списке
                    if (!list.Contains(str)) list.Add(str);
                }

                t.Stop();

                this.textBoxFileReadTime.Text = t.Elapsed.ToString();
                this.textBoxFileReadCount.Text = list.Count.ToString();
                this.textBoxFileName.Text = fd.FileName.ToString();

            }
            else
            {
                MessageBox.Show("Необходимо выбрать файл");
            }
        }

        private void buttonLevenshteinDist_Click(object sender, EventArgs e)
        {
            //Слово для поиска
            string word = this.textBoxFind.Text.Trim();

            //Если слово для поиска не пусто
            if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
            {
                int maxDist;
                if (!int.TryParse(this.textBoxmaxDist.Text.Trim(), out maxDist))
                {
                    MessageBox.Show("Необходимо указать максимальное расстояние");
                    return;
                }

                if (maxDist < 1 || maxDist > 5)
                {
                    MessageBox.Show("Максимальное расстояние должно быть в диапазоне от 1 до 5");
                    return;
                }

                //Подтягиваем буквы вверх, чтобы не было проблем с регистром
                word = word.ToUpper();

                //запуск листбокса
                this.listBoxResult1.BeginUpdate();
                this.listBoxResult1.Items.Clear();
                Stopwatch timer = new Stopwatch();


                if (!string.IsNullOrWhiteSpace(word) && list.Count > 0)
                {
                    //Проверка на случай отсутствия совпадений
                    bool NoMatches = true;
                    //запуск таймера
                    timer.Start();
                    foreach (string w in list)
                    {
                        //идёт проверка слов в верхнем регистре
                        if (EditDistance.Distance(w.ToUpper(), word) <= maxDist)
                        {
                            this.listBoxResult1.Items.Add(w);
                            NoMatches = false;
                        }
                    }
                    //остановка таймера
                    timer.Stop();
                    //Если совпадений всё же не нашлось
                    if (NoMatches) this.listBoxResult1.Items.Add("Нет сопадений");
                }
                else
                {
                    MessageBox.Show("Необходимо отрыть файл и выбрать слово для поиска");
                }

                this.listBoxResult1.EndUpdate();
                //запись даных из таймера
                this.textBoxParallTime.Text = timer.Elapsed.ToString();
            }
        }


        // Выход 
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
