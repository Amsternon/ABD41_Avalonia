using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        string login = TextBox.Text;
        string password = TextBox1.Text;

        // Проверка логина и пароля для врача
        if (login == "admin" && password == "12345")
        {
            AdminWorm admin = new AdminWorm();
            admin.Show();
            this.Close();
        }
        // Проверка логина и пароля для пациента
        else if (login == "client" && password == "54321")
        {
            ClientWorm client = new ClientWorm();
            client.Show();
            this.Close();
        }
    }
}