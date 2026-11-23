using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Lab3_Maksimenko
{
    public class Lab3Maksimenko : Form
    {
        private Button btnWrite;
        private Button btnRead;
        private Button btnClear;

        private Panel panel1;

        private string filePath;

        private bool showText = false;

        public Lab3Maksimenko()
        {
            Text = "Lab3 Maksimenko";
            Width = 1200;
            Height = 750;
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = SystemColors.Control;

            filePath = Path.Combine(
                Application.StartupPath,
                "text.txt");

            CreateInterface();
        }

        private void CreateInterface()
        {
            btnWrite = new Button();
            btnWrite.Text = "Записать";
            btnWrite.Size = new Size(150, 35);
            btnWrite.Location = new Point(10, 10);
            btnWrite.Click += BtnWrite_Click;
            Controls.Add(btnWrite);

            btnRead = new Button();
            btnRead.Text = "Считать";
            btnRead.Size = new Size(150, 35);
            btnRead.Location = new Point(175, 10);
            btnRead.Click += BtnRead_Click;
            Controls.Add(btnRead);

            btnClear = new Button();
            btnClear.Text = "Очистить";
            btnClear.Size = new Size(150, 35);
            btnClear.Location = new Point(340, 10);
            btnClear.Click += BtnClear_Click;
            Controls.Add(btnClear);

            panel1 = new Panel();
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(10, 55);
            panel1.Size = new Size(1160, 650);

            panel1.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            panel1.Paint += Panel1_Paint;

            Controls.Add(panel1);
        }

        private void BtnWrite_Click(object sender, EventArgs e)
        {
            string[] lines =
            {
                "Первая строка лабораторной работы",
                "Вторая строка лабораторной работы",
                "Третья строка лабораторной работы",
                "Четвертая строка лабораторной работы",
                "Пятая строка лабораторной работы",
                "Шестая строка лабораторной работы",
                "Седьмая строка лабораторной работы",
                "Восьмая строка лабораторной работы",
                "Девятая строка лабораторной работы",
                "Десятая строка лабораторной работы"
            };

            try
            {
                File.WriteAllLines(filePath, lines);

                MessageBox.Show(
                    "Файл успешно записан!\n" +
                    "Количество строк: " + lines.Length,
                    "Запись файла",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка записи файла:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnRead_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                    "Файл не найден.\nСначала нажмите «Записать».",
                    "Нет файла",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            string[] lines;

            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка чтения файла:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (lines.Length < 10)
            {
                MessageBox.Show(
                    "В файле должно быть 10 строк.",
                    "Ошибка данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            showText = true;

            panel1.Invalidate();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            showText = false;

            panel1.Invalidate();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!showText)
                return;

            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length < 10)
                return;

            Graphics g = e.Graphics;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.TextRenderingHint =
                System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            float panelWidth = panel1.ClientSize.Width;
            float panelHeight = panel1.ClientSize.Height;

            // ПЕРВЫЕ 6 СТРОК
            // Consolas, Bold, 36 pt, вертикально

            using (Font font1 = new Font(
                "Consolas",
                36,
                FontStyle.Bold,
                GraphicsUnit.Point))
            {
                using (StringFormat format1 = new StringFormat())
                {
                    format1.Alignment = StringAlignment.Center;
                    format1.LineAlignment = StringAlignment.Center;

                    float groupX = 10;
                    float groupWidth = Math.Min(
                        540, panelWidth * 0.48f);

                    float columnWidth = groupWidth / 6;

                    for (int i = 0; i < 6; i++)
                    {
                        GraphicsState state = g.Save();

                        float centerX =
                            groupX +
                            columnWidth * i +
                            columnWidth / 2;

                        float centerY = panelHeight / 2;

                        g.TranslateTransform(centerX, centerY);
                        g.RotateTransform(-90);

                        RectangleF textRect = new RectangleF(
                            -panelHeight / 2 + 10,
                            -columnWidth / 2,
                            panelHeight - 20,
                            columnWidth);

                        g.DrawString(
                            lines[i],
                            font1,
                            Brushes.Black,
                            textRect,
                            format1);

                        g.Restore(state);
                    }
                }
            }

            // СТРОКИ 7-9
            // Calibri, Strikeout, 24 pt

            using (Font font2 = new Font(
                "Calibri",
                24,
                FontStyle.Strikeout,
                GraphicsUnit.Point))
            {
                using (StringFormat format2 = new StringFormat())
                {
                    format2.Alignment = StringAlignment.Far;
                    format2.LineAlignment = StringAlignment.Near;

                    float group2X = Math.Max(
                        560, panelWidth * 0.48f);

                    float group2Width =
                        panelWidth - group2X - 20;

                    float startY = 40;
                    float lineHeight = 65;

                    for (int i = 0; i < 3; i++)
                    {
                        RectangleF textRect = new RectangleF(
                            group2X,
                            startY + i * lineHeight,
                            group2Width,
                            lineHeight);

                        g.DrawString(
                            lines[i + 6],
                            font2,
                            Brushes.Black,
                            textRect,
                            format2);
                    }
                }
            }

            // 10 СТРОКА
            // Corbel, Underline, 24 pt

            using (Font font3 = new Font(
                "Corbel",
                24,
                FontStyle.Underline,
                GraphicsUnit.Point))
            {
                using (StringFormat format3 = new StringFormat())
                {
                    format3.Alignment = StringAlignment.Near;
                    format3.LineAlignment = StringAlignment.Far;

                    float group3X = Math.Max(
                        560, panelWidth * 0.48f);

                    float group3Y = panelHeight * 0.52f;

                    float group3Width =
                        panelWidth - group3X - 20;

                    float group3Height =
                        panelHeight * 0.35f;

                    RectangleF textRect = new RectangleF(
                        group3X,
                        group3Y,
                        group3Width,
                        group3Height);

                    g.DrawString(
                        lines[9],
                        font3,
                        Brushes.Black,
                        textRect,
                        format3);
                }
            }

            using (Pen pen = new Pen(Color.Gray, 1))
            {
                g.DrawRectangle(
                    pen,
                    5,
                    5,
                    panelWidth - 10,
                    panelHeight - 10);
            }
        }
    }
}