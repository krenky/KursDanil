using System;
using System.IO;
using Microsoft.Win32;
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
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Text.Json;

namespace KursDanil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bank NewBank = new Bank(10);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients(NameClient.Text,
                Convert.ToInt32(SumOper.Text),
                Date.SelectedDate ?? DateTime.Now);
            NewBank.Add(client);
            ClientsData.ItemsSource = ConvertMassToList();
            TransactionsData.ItemsSource = ConvertOperToList(client);
        }
        

        private void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            NewBank.Delete();
            ClientsData.ItemsSource = ConvertMassToList();
        }

        private void AddOperations_Click(object sender, RoutedEventArgs e)
        {
            Clients client = (Clients)ClientsData.SelectedItem;
            try
            {
                client = NewBank.Search(client.Name);
                client.AddOperation(Convert.ToInt32(SumOper.Text),
                Date.SelectedDate ?? DateTime.Now);
                TransactionsData.ItemsSource = ConvertOperToList(client);
                ClientsData.ItemsSource = ConvertMassToList();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select Client");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error {ex}");
            }
        }

        private List<Clients> ConvertMassToList()
        {
            List<Clients> clients = new List<Clients>();
            foreach (Clients i in NewBank.Clients)
            {
                if (i != null)
                {
                    clients.Add(i);
                }
            }
            return clients;
        }
        private List<Operations> ConvertOperToList(Clients clients)
        {
            List<Operations> operations = new List<Operations>();
            Operations operations1 = clients.Operations;
            while (operations1 != null)
            {
                operations.Add(operations1);
                operations1 = operations1.Next;
            }
            return operations;
        }

        private void ClientsData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsData.SelectedItem != null)
            {
                Clients client = (Clients)ClientsData.SelectedItem;
                client = NewBank.Search(client.Name);
                TransactionsData.ItemsSource = ConvertOperToList(client);
            }
        }

        private void NameClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsLetter(e.Text, 0));
        }

        private void NameClient_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void SumOper_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        private void SumOper_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if ((bool)saveFileDialog.ShowDialog())
                using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
                {
                    JsonSerializer.Serialize<Bank>(new Utf8JsonWriter(fs), NewBank);
                }
            //XmlSerializer xmlFormatter = new XmlSerializer(typeof(Bank));
            //if ((bool)saveFileDialog.ShowDialog())
            //    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
            //    {
            //        xmlFormatter.Serialize(fs, NewBank);
            //    }

                //BinaryFormatter formatter = new BinaryFormatter();
                //if ((bool)saveFileDialog.ShowDialog())
                //    using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
                //    {
                //        formatter.Serialize(fs, NewBank);
                //    }
        }

        private async void Open_Button_Click(object sender, RoutedEventArgs e)
        {
            Bank bank = new Bank();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
                using (FileStream fs = (FileStream)openFileDialog.OpenFile())
                {
                    NewBank = await JsonSerializer.DeserializeAsync<Bank>(fs);
                }
            ClientsData.ItemsSource = ConvertMassToList();
            //foreach (Clients i in bank.Clients)
            //{
            //    NewBank.Add(i);
            //    NewBank.
            //}
        }
    }
}
