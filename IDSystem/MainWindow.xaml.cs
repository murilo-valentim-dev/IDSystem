using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IDSystem.Data;
using IDSystem.Views;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using IDSystem.Models;
// para chamar Migrate()

namespace IDSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                using (var db = new AppDbContext())
                {
                    db.Database.Migrate();

                    if (!db.Usuarios.Any(u => u.IsMaster))
                    {
                        var master = new Usuario
                        {
                            Login = "Master",
                            SenhaHash = BCrypt.Net.BCrypt.HashPassword("Master45"),
                            IsMaster = true
                        };
                        db.Usuarios.Add(master);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao aplicar migrations:\n{ex.Message}", "Erro no Banco de Dados", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }

            TextBlockCapsLock.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Collapsed;

            fpassword_view.Visibility = Visibility.Collapsed;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            TextBlockCapsLock.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Collapsed;
        }


        private void fpassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            CheckCapsLock();
            if (e.Key == Key.Return ||
             e.Key == Key.Enter)
                Enter();
        }

        private void Button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fpassword_view.Text = fpassword.Password;
            fpassword_view.Visibility = Visibility.Visible;
        }

        private void Button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            fpassword_view.Visibility = Visibility.Collapsed;
        }

        private void Command_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            ExecuteCommand(btn.CommandParameter.ToString());
        }

        private void ExecuteCommand(string p_button)
        {
            switch (p_button)
            {
                case "F2":
                    Enter();
                    break;
                case "Escape":
                case "F8":
                    Close();
                    Application.Current.Shutdown();
                    break;
            }
        }
        private void Enter()
        {
            try
            {
                ValidateForm();
                GetCredentials(fusername.Text, fpassword.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(fusername.Text))
                throw new Exception("Informe o usuário.");
            if (!Debugger.IsAttached && string.IsNullOrWhiteSpace(fpassword.Password)) 
                throw new Exception("Informe a senha.");
        }

        protected bool GetCredentials(string username, string password)
        {
            try
            {
                using var db = new AppDbContext();
                var usuario = db.Usuarios.FirstOrDefault(u => u.Login == username);

                if (usuario != null && BCrypt.Net.BCrypt.Verify(password, usuario.SenhaHash))
                {
                    //// Se for master, abre opção de cadastro de usuários
                    //if (usuario.IsMaster)
                    //{
                    //    var telaCadastroUsuario = new CadastroUsuario(); 
                    //    telaCadastroUsuario.ShowDialog();
                    //}

                    var telaPrincipal = new BuscaUsuario(usuario.IsMaster); // sua tela principal
                    telaPrincipal.Show();
                    this.Close();
                    return true;
                }
                else
                {
                    MessageBox.Show("Usuário ou senha inválidos", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao validar usuário: " + ex.Message);
                return false;
            }
        }


        //protected bool GetCredentials(string username, string password)
        //{
        //    try
        //    {

        //        //using var db = new AppDbContext();
        //        //var usuario = db.Usuarios.FirstOrDefault(u => u.Nome == username);

        //        if(fusername.Text == "Master" && fpassword.Password == "Senha123")
        //        {
        //            var telaPrincipal = new BuscaUsuario();
        //            telaPrincipal.Show();
        //            this.Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Usuário ou senha inválidos", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //}

        private void CheckCapsLock()
        {
            TextBlockCapsLock.Visibility = Console.CapsLock ? Visibility.Visible : Visibility.Collapsed;
        }


    }
}