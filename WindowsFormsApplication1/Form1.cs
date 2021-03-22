using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Функция умножения матриц
        private void button1_Click(object sender, EventArgs e)
        {
            //Массив для ответа
            int[,] matrixC = new int[Convert.ToInt32(dataGridView1.RowCount),Convert.ToInt32(dataGridView2.ColumnCount)];
            dataGridView3.ColumnCount = dataGridView2.ColumnCount;
            dataGridView3.RowCount = dataGridView1.RowCount;
            // проверка на то, что число столбцов матрицы 1 равно строк в матрице 2
            if (dataGridView1.ColumnCount != dataGridView2.RowCount)
            {
               MessageBox.Show("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }
            // цикл умножения элементов матриц
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    matrixC[i, j] = 0;

                    for (int k = 0; k < dataGridView1.ColumnCount; k++)
                    {
                        matrixC[i, j] += Convert.ToInt32(dataGridView1.Rows[i].Cells[k].Value) * Convert.ToInt32(dataGridView2.Rows[k].Cells[j].Value);
                    }
                }
            }
            // заполнение матрицы 3
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Value = matrixC[i, j];
                }
            }
        }
        //функция вывода содержимого текстовых файлов
        private void button2_Click(object sender, EventArgs e)
        {
            // считывание строк с текстовых файлов
            string[] line = File.ReadAllLines(@"1_1.txt").ToArray();
            string[] lines = File.ReadAllLines(@"1_2.txt").ToArray();
            // считывание первых строк с текстовых файлов
            string Line = File.ReadLines(@"1_1.txt").First();
            string Line_S = File.ReadLines(@"1_2.txt").First();
            // разбиение на символы матриц  с разделителями \n
            string[] str = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string[] str_S = Line_S.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // считывание длины строк
            int rowss = File.ReadAllLines(@"1_1.txt").Length, rows_s = File.ReadAllLines(@"1_2.txt").Length;
            // присваивание значений переменной
            int columns = str.Length, columns_s = str_S.Length;
            // создание строчек и столбцов
            dataGridView1.ColumnCount = columns;
            dataGridView1.RowCount = rowss;
            // создание строчек и столбцов
            dataGridView2.ColumnCount = columns_s;
            dataGridView2.RowCount = rows_s;
            // заполнение матрицы 1 
            for (int i = 0; i < rowss; i++)
            {
                int[] row = line[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
                for (int j = 0; j < columns; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = row[j];
                }
            }
            // заполнение матрицы 2
            for (int i = 0; i < rows_s; i++)
            {
                int[] rows = lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(Int32.Parse).ToArray();
                for (int j = 0; j < columns_s; j++)
                {
                    dataGridView2.Rows[i].Cells[j].Value = rows[j];
                }
            }
        }
        // функция вывода ответа в файл
        private void button3_Click(object sender, EventArgs e)
        { // открытие и запмсь в файл 
            StreamWriter file = new StreamWriter("1_3.txt");
            // цикл записи в файл
            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                {
                    file.Write(Convert.ToInt32(dataGridView3.Rows[i].Cells[j].Value) + " ");
                }
                file.Write(Environment.NewLine);
            } 
            //закрытые файла
            file.Close();
        }
        // функция печати
        private void button4_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog1 = new PrintDialog();
            PrintDocument printDocument1 = new PrintDocument();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDialog1.ShowDialog();
            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            printDocument1.Print();
        }
        // функция печати
        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            StreamReader TF = new StreamReader("1_3.txt", Encoding.Default);
            string TFF = TF.ReadToEnd();
            TF.Dispose();
            e.Graphics.DrawString(TFF, Font, new SolidBrush(Color.Black), new RectangleF(20, 20, 800, 600));
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
