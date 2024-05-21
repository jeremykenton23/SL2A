using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Win32;

namespace MyWordProcessor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewDocument_Click(object sender, RoutedEventArgs e)
        {
            txtDocument.Document.Blocks.Clear();
        }

        private void OpenDocument_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string text = File.ReadAllText(filename);
                txtDocument.Document.Blocks.Clear();
                txtDocument.Document.Blocks.Add(new Paragraph(new Run(text)));
            }
        }

        private void SaveDocument_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName;
                File.WriteAllText(filename, new TextRange(txtDocument.Document.ContentStart, txtDocument.Document.ContentEnd).Text);
            }
        }

        private void PrintDocument_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(txtDocument as Visual, "Printing RichTextBox");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            if (txtDocument.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
            {
                txtDocument.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            else
            {
                txtDocument.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            if (txtDocument.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
            {
                txtDocument.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            else
            {
                txtDocument.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            TextDecorationCollection textDecorations = (TextDecorationCollection)txtDocument.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            if (textDecorations == null)
            {
                txtDocument.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                if (!textDecorations.Contains(TextDecorations.Underline[0]))
                {
                    textDecorations.Add(TextDecorations.Underline[0]);
                    txtDocument.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
                }
            }
        }

        private void InsertImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                Image image = new Image();
                image.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(filename));
                InlineUIContainer container = new InlineUIContainer(image);
                Paragraph paragraph = new Paragraph(container);
                if (txtDocument.Document.Blocks.LastBlock is Paragraph lastParagraph)
                {
                    lastParagraph.Inlines.Add(container);
                }
                else
                {
                    txtDocument.Document.Blocks.Add(paragraph);
                }
            }
        }

        private void InsertTable_Click(object sender, RoutedEventArgs e)
        {
            Table table = new Table();
            for (int i = 0; i < 5; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < 3; j++)
                {
                    TableCell cell = new TableCell(new Paragraph(new Run("Cell")));
                    row.Cells.Add(cell);
                }
                table.RowGroups[0].Rows.Add(row); // Voeg de rij direct toe aan de tabel
            }
            txtDocument.Document.Blocks.Add(table);
        }

        private void FontSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeComboBox.SelectedItem != null)
            {
                ComboBoxItem item = (ComboBoxItem)FontSizeComboBox.SelectedItem;
                if (item != null)
                {
                    string fontSize = item.Content.ToString();
                    txtDocument.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
                }
            }
        }

        private void txtDocument_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = new TextRange(txtDocument.Document.ContentStart, txtDocument.Document.ContentEnd).Text;
            int wordCount = text.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
            wordCountTextBlock.Text = $"Word Count: {wordCount}";
        }
    }
}
