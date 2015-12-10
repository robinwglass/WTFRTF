using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace WTFRTF
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void AsPlainTextButton_Click(object sender, RoutedEventArgs e)
    {
      var Text = GetSourceAsPlainText();

      if (AsBase64CheckBox.IsChecked.Value)
        Text = EncodeBase64(Text);

      SetTargetContent(Text);
    }
    private void AsRTFButton_Click(object sender, RoutedEventArgs e)
    {
      var RtfText = GetSourceAsRTF();

      if (AsBase64CheckBox.IsChecked.Value)
        RtfText = EncodeBase64(RtfText);

      SetTargetContent(RtfText);
    }
    private void CopyTargetToClipboardButton_Click(object sender, RoutedEventArgs e)
    {
      Clipboard.SetText(GetTargetAsPlainText());
    }

    private string GetSourceAsPlainText()
    {
      return new TextRange(SourceRichTextBox.Document.ContentStart, SourceRichTextBox.Document.ContentEnd).Text;
    }
    private string GetTargetAsPlainText()
    {
      return new TextRange(TargetRichTextBox.Document.ContentStart, TargetRichTextBox.Document.ContentEnd).Text;
    }
    private string GetSourceAsRTF()
    {
      var TextRange = new TextRange(SourceRichTextBox.Document.ContentStart, SourceRichTextBox.Document.ContentEnd);
      var MemoryStream = new MemoryStream();

      TextRange.Save(MemoryStream, DataFormats.Rtf);

      return ASCIIEncoding.Default.GetString(MemoryStream.ToArray());
    }
    private void SetTargetContent(string Text)
    {
      TargetRichTextBox.Document = new FlowDocument(new Paragraph(new Run(Text)));
    }
    private string EncodeBase64(string Text)
    {
      return Convert.ToBase64String(Encoding.UTF8.GetBytes(Text));
    }
    private string DecodeBase64(string Base64Text)
    {
      return Encoding.UTF8.GetString(Convert.FromBase64String(Base64Text));
    }
  }
}
