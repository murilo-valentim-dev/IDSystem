using System;
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
using System.Windows.Shapes;
using IDSystem.Data;
using IDSystem.Models;

namespace IDSystem.Views
{
    /// <summary>
    /// Interaction logic for UsuarioSistema.xaml
    /// </summary>
    public partial class UsuarioSistema : Window
    {
        private List<Usuario> listaCompleta;

        public UsuarioSistema()
        {
            InitializeComponent();
            CarregarTodasPessoas();
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
                case "Escape":
                case "F8":
                    Close();
                    break;
            }
        }


        private void CarregarTodasPessoas()
        {
            using var db = new AppDbContext();
            listaCompleta = db.Usuarios.ToList();
            dataGridUsuario.ItemsSource = listaCompleta;
        }

    }
}
