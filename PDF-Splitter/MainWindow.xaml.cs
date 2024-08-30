using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Windows.Forms;


namespace PDF_Splitter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> pathFiles = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImagePanel_Drop(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);

                foreach(string file in files)
                {
                    pathFiles.Add(file);
                }

                foreach (string file in files)
                {
                    // Проверяем, является ли файл PDF
                    if (System.IO.Path.GetExtension(file).ToLower() == ".pdf")
                    {
                        string nameFile = GetFileNameFromPath(file);
                        CreateFileVisual(nameFile);
                    }
                    else
                    {
                        // Выводим сообщение об ошибке, если файл не PDF
                        System.Windows.MessageBox.Show("Можно перетаскивать только PDF файлы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }
                }
            }
        }

        public string GetFileNameFromPath(string filePath)
        {
            // Разбиваем путь на части, используя разделитель пути
            string[] parts = filePath.Split(System.IO.Path.DirectorySeparatorChar);

            // Возвращаем последний элемент массива - имя файла
            return parts[parts.Length - 1];
        }

        private void CreateFileVisual(string nameFile)
        {
            Grid grid = new Grid();
            RowDefinition rowImage = new RowDefinition();
            RowDefinition rowText = new RowDefinition();
            System.Windows.Controls.Label label = new System.Windows.Controls.Label();
            System.Windows.Controls.Image pdfImage = new System.Windows.Controls.Image();

            grid.Height = 100;
            grid.Width = 100;

            pdfImage.Source = new BitmapImage(new Uri("Images/pdf.png", UriKind.Relative)); ;

            rowImage.Height = new GridLength(70);

            label.Content = nameFile;

            grid.RowDefinitions.Add(rowImage);
            grid.RowDefinitions.Add(rowText);

            Grid.SetRow(pdfImage, 0);
            Grid.SetRow(label, 1);

            grid.Children.Add(label);
            grid.Children.Add(pdfImage);
            ImagePanel.Children.Add(grid);
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно для выбора папки
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Выберите папку";
            string folderPath = "";
            // Показывает диалоговое окно
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Получаем путь к выбранной папке
                folderPath = folderBrowserDialog.SelectedPath;
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }

            if(folderPath != "")
            {
                foreach(var pathFile in pathFiles)
                {
                    Splitter.SplitPdfToImages(pathFile, folderPath + "//" + GetFileNameFromPath(pathFile));
                }
            }
        }
    }
}
