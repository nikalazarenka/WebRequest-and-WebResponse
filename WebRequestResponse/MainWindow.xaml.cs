using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WebRequestResponse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Request_Click(object sender, RoutedEventArgs e)
        {
            WebRequest request = WebRequest.Create(txb_url.Text);
            WebResponse response = request.GetResponse();

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                    txb_sourceCode.Text += line + "\n";
            }

            string messageServer = "Целевой URL: \t" + request.RequestUri + "\nМетод запроса: \t" +
                request.Method +
                "\nТип полученных данных: \t" + response.ContentType + "\nДлина ответа: \t" +
                response.ContentLength + "\nЗаголовки";
            WebHeaderCollection whc = response.Headers;
            var headers = Enumerable.Range(0, whc.Count)
                .Select(p =>
                {
                    return new
                    {
                        Key = whc.GetKey(p),
                        Names = whc.GetValues(p)
                    };
                });
            foreach(var item in headers)
            {
                messageServer += "\n " + item.Key + ":";
                foreach (var n in item.Names)
                    messageServer += "\t" + n;
            }
            txb_serverInfo.Text = messageServer;
        }
    }
}
