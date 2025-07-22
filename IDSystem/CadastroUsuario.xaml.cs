using System;
using System.Linq;
using System.Windows;
using IDSystem.Data;
using IDSystem.Models;
using BCrypt.Net;

namespace IDSystem.Views
{
    public partial class CadastroUsuario : Window
    {
        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text.Trim();
            var senha = txtSenha.Password;
            var confirmar = txtConfirmarSenha.Password;

            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Informe o login.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(confirmar))
            {
                MessageBox.Show("Informe e confirme a senha.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (senha != confirmar)
            {
                MessageBox.Show("As senhas não coincidem.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using var db = new AppDbContext();

            if (db.Usuarios.Any(u => u.Login.ToLower() == login.ToLower()))
            {
                MessageBox.Show("Já existe um usuário com esse login.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var novoUsuario = new Usuario
            {
                Login = login,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha),
                IsMaster = false
            };

            db.Usuarios.Add(novoUsuario);
            db.SaveChanges();

            MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close(); // ou: limpar campos para novo cadastro
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
