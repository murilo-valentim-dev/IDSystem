using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using IDSystem.Data;
using IDSystem.Models;

namespace IDSystem.Views
{
    public partial class BuscaUsuario : Window
    {
        private List<Pessoa> listaCompleta;
        private readonly string placeholderPesquisar = "[PESQUISAR]";
        bool master = false;
        public BuscaUsuario(bool isMaster)
        {
            InitializeComponent();
            ButtonCadastroUsuario.Visibility = Visibility.Collapsed;
            ButtonUsuarios.Visibility = Visibility.Collapsed;
            if(isMaster == true)
            {
                master = true;
                ButtonCadastroUsuario.Visibility = Visibility.Visible;
                ButtonUsuarios.Visibility = Visibility.Visible;
            }
            CarregarTodasPessoas();
            ClearFilter();
        }

        private void CarregarTodasPessoas()
        {
            using var db = new AppDbContext();
            listaCompleta = db.Pessoas.ToList();
            dataGridPessoas.ItemsSource = listaCompleta;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string termo = txtBusca.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(termo) || termo == placeholderPesquisar.ToLower())
            {
                dataGridPessoas.ItemsSource = listaCompleta;
                return;
            }

            var filtrado = listaCompleta
                .Where(p =>
                    (!string.IsNullOrEmpty(p.NomeCompleto) && p.NomeCompleto.ToLower().Contains(termo)) ||
                    (!string.IsNullOrEmpty(p.CPF) && p.CPF.ToLower().Contains(termo)))
                .ToList();

            dataGridPessoas.ItemsSource = filtrado;
        }

        private void ButtonClearFilter_Click(object sender, RoutedEventArgs e)
        {
            ClearFilter();
        }

        private void ClearFilter()
        {
            txtBusca.Text = placeholderPesquisar;
            dataGridPessoas.ItemsSource = listaCompleta;
            imgFoto.Source = null;
        }

        private void txtBusca_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBusca.Text == placeholderPesquisar)
                txtBusca.Text = "";
        }

        private void fpesquisar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBusca.Text))
                txtBusca.Text = placeholderPesquisar;

            BtnBuscar_Click(sender, e);
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var cadastro = new CadastroPessoa(); // Cadastro novo (sem ID)
            bool? resultado = cadastro.ShowDialog();

            CarregarTodasPessoas();
            ClearFilter();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPessoas.SelectedItem is Pessoa pessoaSelecionada)
            {
                var janelaCadastro = new CadastroPessoa(pessoaSelecionada.Id);
                janelaCadastro.ShowDialog();

                //// Por enquanto, apenas abrir em modo normal
                //MessageBox.Show("Modo de edição ainda não implementado.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                CarregarTodasPessoas();
                ClearFilter();
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa para editar.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void buttonExcluir_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridPessoas.SelectedItem is Pessoa pessoaSelecionada)
            {
                var confirmacao = MessageBox.Show(
                    $"Deseja realmente excluir o registro de {pessoaSelecionada.NomeCompleto}?",
                    "Confirmação de exclusão", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (confirmacao == MessageBoxResult.Yes)
                {
                    using var db = new AppDbContext();
                    var pessoa = db.Pessoas.Find(pessoaSelecionada.Id);

                    if (pessoa != null)
                    {
                        db.Pessoas.Remove(pessoa);
                        db.SaveChanges();

                        MessageBox.Show("Suspeito excluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        CarregarTodasPessoas();
                        ClearFilter();
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma pessoa para excluir.", "Atenção", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dataGridPessoas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPessoas.SelectedItem is Pessoa pessoa && pessoa.FotoFrontal != null)
            {
                using var ms = new MemoryStream(pessoa.FotoFrontal);
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                imgFoto.Source = image;
            }
            else
            {
                imgFoto.Source = null;
            }
        }

        private void dataGridPessoas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            buttonUpd_Click(sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCadastroUsuario_Click(object sender, RoutedEventArgs e)
        {
            // Se for master, abre opção de cadastro de usuários
            if (master)
            {
                var telaCadastroUsuario = new CadastroUsuario();
                telaCadastroUsuario.ShowDialog();
            }
        }

        private void ButtonUsuarioso_Click(object sender, RoutedEventArgs e)
        {
            // Se for master, abre opção de cadastro de usuários
            if (master)
            {
                var telaUsuarioSistema = new UsuarioSistema();
                telaUsuarioSistema.ShowDialog();
            }
        }
    }
}
