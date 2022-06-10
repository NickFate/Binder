using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Binder
{
    public partial class MainForm : Form
    {
        

        private List<Bitmap> images;
        private int imageWidth, imageHeight, matrixSize;
        private Bitmap newImage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            ImageLoad();
        }


        /// <summary>
        /// Открывает диалоговое окно с выбором изображений для склейки 
        /// </summary>
        private void ImageLoad()
        {

            images = new List<Bitmap>();
            OpenFileDialog files = new OpenFileDialog();
            files.Filter = "Файлы картинок| *.png; *.jpg ";

            // разрешаем выбирать файлы
            files.Multiselect = true;
            files.Title = "Выберите изображения для склейки";
            if (files.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in files.FileNames)
                {
                    images.Add(new Bitmap(file));
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveImage();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            BinderFoo();
        }

        /// <summary>
        /// Склеивает изображения из <param>images</param>
        /// </summary>
        public void BinderFoo()
        {

            imageWidth = images[0].Width;
            imageHeight = images[0].Height;
            matrixSize = images.Count;
            newImage = new Bitmap(imageWidth * matrixSize, imageHeight * 1);
            newImage.SetResolution(72, 72);
           
            Graphics g = Graphics.FromImage(newImage);
            int y = 0, count = 0; 
            for (int x = 0; x < imageWidth * matrixSize; x += imageWidth)
            {
                g.DrawImage(images[count], x, y);
                count++;
            }

        }


       /// <summary>
       /// Сохраняет изображение
       /// </summary>
        private void SaveImage()
        {

            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Png Image|*.png";
            file.Title = "Сохранить";
            file.ShowDialog();

            newImage.Save(file.FileName, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
