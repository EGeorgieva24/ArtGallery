using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace SpotifyPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string connectionString = "Data Source=YOUR_SERVER;Initial Catalog=SpotifyPrototype;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string usernameOrEmail = txtUsernameOrEmail.Text;
            string password = txtPassword.Text;

            // Basic validation
            if (string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Username/email and password are required.";
                return;
            }

            // Authenticate user against the database
            if (AuthenticateUser(usernameOrEmail, password))
            {
                // Navigate to main window or perform other actions
                MessageBox.Show("Login successful!");
            }
            else
            {
                lblMessage.Text = "Invalid username/email or password.";
            }
        }
        private bool AuthenticateUser(string usernameOrEmail, string password)
    {
    bool isAuthenticated = false;

        using (SqlConnection connection = new SqlConnection(connectionString))
     {
        string query = "SELECT COUNT(*) FROM Users WHERE (Username = @UsernameOrEmail OR Email = @UsernameOrEmail) AND PasswordHash = @PasswordHash";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@UsernameOrEmail", usernameOrEmail);
        command.Parameters.AddWithValue("@PasswordHash", HashPassword(password));

        try
        {
            connection.Open();
            int userCount = (int)command.ExecuteScalar();
            isAuthenticated = userCount == 1;
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
     }

    return isAuthenticated;
    }

    private string HashPassword(string password)
    {
    // Implement password hashing algorithm (e.g., using bcrypt or SHA-256)
    // For simplicity, let's assume the password is stored as plaintext in the database
    return password;
    }
    
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Owner = this;
            signUp.ShowDialog();
        }
    
    }
}
