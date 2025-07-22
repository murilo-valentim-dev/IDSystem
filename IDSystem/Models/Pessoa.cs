using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDSystem.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        // Identificação Pessoal
        [Required]
        public string NomeCompleto { get; set; }
        public string Apelido { get; set; }
        public string Sexo { get; set; } // M, F, Outro
        public DateTime? DataNascimento { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Nacionalidade { get; set; }
        public string Naturalidade { get; set; }
        public string NomeMae { get; set; }
        public string NomePai { get; set; }

        // Dados Visuais
        public string? CorPele { get; set; } // permitir null
        public string CorOlhos { get; set; }
        public string CorCabelo { get; set; }
        public double? Altura { get; set; } // em metros
        public double? Peso { get; set; } // em kg
        public string TatuagensMarcas { get; set; } // descrição textual
        public byte[]? FotoFrontal { get; set; }
        public byte[]? FotoPerfilDireito { get; set; }
        public byte[]? FotoPerfilEsquerdo { get; set; }

        // Endereço e Contato
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        // Dados Investigativos
        public string CrimesSuspeitos { get; set; }
        public string NivelPericulosidade { get; set; } // Baixo, Médio, Alto
        public bool Foragido { get; set; }
        public bool MandadoPrisao { get; set; }
        public string Status { get; set; } // Preso, Foragido, Solto etc.
        public string Observacoes { get; set; }

        // Relacionamentos
        public string Comparsas { get; set; }
        public string FaccaoCriminosa { get; set; }
        public string LocaisFrequentados { get; set; }
        public string VeiculosUsados { get; set; } // pode ser separado depois

        // Histórico
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimaAtualizacao { get; set; }
        public string UsuarioResponsavel { get; set; }
        public byte[]? Foto { get; set; }
    }
}
