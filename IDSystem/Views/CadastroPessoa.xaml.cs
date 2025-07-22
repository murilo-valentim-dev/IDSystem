using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using IDSystem.Models;
using IDSystem.Data;

namespace IDSystem.Views
{
    public partial class CadastroPessoa : Window
    {
        private byte[] fotoFrontal;
        private byte[] fotoPerfilDireito;
        private byte[] fotoPerfilEsquerdo;
        private readonly int? pessoaId = null;
        private Pessoa pessoaEditando = null;
        public CadastroPessoa()
        {
            InitializeComponent();
        }

        public CadastroPessoa(int idPessoa)
        {
            InitializeComponent();
            pessoaId = idPessoa;
            CarregarDados();
        }

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using var db = new AppDbContext();
                Pessoa pessoa;

                if (pessoaId.HasValue)
                {
                    pessoa = db.Pessoas.Find(pessoaId.Value);
                    if (pessoa == null)
                    {
                        MessageBox.Show("Pessoa não encontrada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    pessoa = new Pessoa
                    {
                        UsuarioResponsavel = Environment.UserName,
                        DataCadastro = DateTime.Now
                    };
                    db.Pessoas.Add(pessoa);
                }

                // Preencher/atualizar campos
                pessoa.NomeCompleto = txtNomeCompleto.Text;
                pessoa.Apelido = txtApelido.Text;
                pessoa.Sexo = (cmbSexo.SelectedItem as ComboBoxItem)?.Content.ToString();
                pessoa.DataNascimento = dpNascimento.SelectedDate;
                pessoa.CPF = txtCPF.Text;
                pessoa.RG = txtRG.Text;
                pessoa.Nacionalidade = txtNacionalidade.Text;
                pessoa.Naturalidade = txtNaturalidade.Text;
                pessoa.NomeMae = txtNomeMae.Text;
                pessoa.NomePai = txtNomePai.Text;

                pessoa.Altura = double.TryParse(txtAltura.Text, out var altura) ? altura : null;
                pessoa.Peso = double.TryParse(txtPeso.Text, out var peso) ? peso : null;
                pessoa.CorOlhos = txtCorOlhos.Text;
                pessoa.CorCabelo = txtCorCabelo.Text;
                pessoa.TatuagensMarcas = txtTatuagens.Text;

                pessoa.Endereco = txtEndereco.Text;
                pessoa.Bairro = txtBairro.Text;
                pessoa.Cidade = txtCidade.Text;
                pessoa.Estado = txtEstado.Text;
                pessoa.Telefone = txtTelefone.Text;
                pessoa.Email = txtEmail.Text;

                pessoa.CrimesSuspeitos = txtCrimes.Text;
                pessoa.NivelPericulosidade = (cmbPericulosidade.SelectedItem as ComboBoxItem)?.Content.ToString();
                pessoa.Foragido = chkForagido.IsChecked ?? false;
                pessoa.MandadoPrisao = chkMandado.IsChecked ?? false;
                pessoa.Status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
                pessoa.Observacoes = txtObservacoes.Text;

                pessoa.Comparsas = txtComparsas.Text;
                pessoa.FaccaoCriminosa = txtFaccao.Text;
                pessoa.LocaisFrequentados = txtLocais.Text;
                pessoa.VeiculosUsados = txtVeiculos.Text;

                pessoa.FotoFrontal = fotoFrontal;
                pessoa.FotoPerfilDireito = fotoPerfilDireito;
                pessoa.FotoPerfilEsquerdo = fotoPerfilEsquerdo;

                pessoa.DataUltimaAtualizacao = DateTime.Now;

                db.SaveChanges();

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelecionarFoto(out byte[] destino, Image imagemAlvo)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Imagens (*.jpg;*.png)|*.jpg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                destino = File.ReadAllBytes(dialog.FileName);
                imagemAlvo.Source = new BitmapImage(new Uri(dialog.FileName));
            }
            else
            {
                destino = null;
            }
        }

        private void BtnFotoFrontal_Click(object sender, RoutedEventArgs e)
        {
            SelecionarFoto(out fotoFrontal, FotoFrontal);
        }

        private void BtnFotoDireito_Click(object sender, RoutedEventArgs e)
        {
            SelecionarFoto(out fotoPerfilDireito, FotoPerfilDireito);
        }

        private void BtnFotoEsquerdo_Click(object sender, RoutedEventArgs e)
        {
            SelecionarFoto(out fotoPerfilEsquerdo, FotoPerfilEsquerdo);
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CarregarDados()
        {
            using var db = new AppDbContext();
            pessoaEditando = db.Pessoas.Find(pessoaId);

            if (pessoaEditando == null)
            {
                MessageBox.Show("Pessoa não encontrada.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }

            // Dados pessoais
            txtNomeCompleto.Text = pessoaEditando.NomeCompleto;
            txtApelido.Text = pessoaEditando.Apelido;
            cmbSexo.SelectedIndex = ObterIndiceCombo(cmbSexo, pessoaEditando.Sexo);
            dpNascimento.SelectedDate = pessoaEditando.DataNascimento;
            txtCPF.Text = pessoaEditando.CPF;
            txtRG.Text = pessoaEditando.RG;
            txtNacionalidade.Text = pessoaEditando.Nacionalidade;
            txtNaturalidade.Text = pessoaEditando.Naturalidade;
            txtNomeMae.Text = pessoaEditando.NomeMae;
            txtNomePai.Text = pessoaEditando.NomePai;

            // Físico
            txtAltura.Text = pessoaEditando.Altura?.ToString();
            txtPeso.Text = pessoaEditando.Peso?.ToString();
            txtCorOlhos.Text = pessoaEditando.CorOlhos;
            txtCorCabelo.Text = pessoaEditando.CorCabelo;
            txtTatuagens.Text = pessoaEditando.TatuagensMarcas;

            // Endereço
            txtEndereco.Text = pessoaEditando.Endereco;
            txtBairro.Text = pessoaEditando.Bairro;
            txtCidade.Text = pessoaEditando.Cidade;
            txtEstado.Text = pessoaEditando.Estado;
            txtTelefone.Text = pessoaEditando.Telefone;
            txtEmail.Text = pessoaEditando.Email;

            // Investigativo
            txtCrimes.Text = pessoaEditando.CrimesSuspeitos;
            cmbPericulosidade.SelectedIndex = ObterIndiceCombo(cmbPericulosidade, pessoaEditando.NivelPericulosidade);
            chkForagido.IsChecked = pessoaEditando.Foragido;
            chkMandado.IsChecked = pessoaEditando.MandadoPrisao;
            cmbStatus.SelectedIndex = ObterIndiceCombo(cmbStatus, pessoaEditando.Status);
            txtObservacoes.Text = pessoaEditando.Observacoes;

            // Relações
            txtComparsas.Text = pessoaEditando.Comparsas;
            txtFaccao.Text = pessoaEditando.FaccaoCriminosa;
            txtLocais.Text = pessoaEditando.LocaisFrequentados;
            txtVeiculos.Text = pessoaEditando.VeiculosUsados;

            // Fotos
            if (pessoaEditando.FotoFrontal != null)
            {
                fotoFrontal = pessoaEditando.FotoFrontal;
                FotoFrontal.Source = ByteArrayParaImagem(fotoFrontal);
            }
            if (pessoaEditando.FotoPerfilDireito != null)
            {
                fotoPerfilDireito = pessoaEditando.FotoPerfilDireito;
                FotoPerfilDireito.Source = ByteArrayParaImagem(fotoPerfilDireito);
            }
            if (pessoaEditando.FotoPerfilEsquerdo != null)
            {
                fotoPerfilEsquerdo = pessoaEditando.FotoPerfilEsquerdo;
                FotoPerfilEsquerdo.Source = ByteArrayParaImagem(fotoPerfilEsquerdo);
            }
        }

        private int ObterIndiceCombo(ComboBox combo, string valor)
        {
            for (int i = 0; i < combo.Items.Count; i++)
            {
                if ((combo.Items[i] as ComboBoxItem)?.Content.ToString() == valor)
                    return i;
            }
            return -1;
        }

        private BitmapImage ByteArrayParaImagem(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            var image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }


    }
}

