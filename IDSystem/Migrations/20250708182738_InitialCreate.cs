using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IDSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    Apelido = table.Column<string>(type: "TEXT", nullable: false),
                    Sexo = table.Column<string>(type: "TEXT", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    RG = table.Column<string>(type: "TEXT", nullable: false),
                    Nacionalidade = table.Column<string>(type: "TEXT", nullable: false),
                    Naturalidade = table.Column<string>(type: "TEXT", nullable: false),
                    NomeMae = table.Column<string>(type: "TEXT", nullable: false),
                    NomePai = table.Column<string>(type: "TEXT", nullable: false),
                    CorPele = table.Column<string>(type: "TEXT", nullable: false),
                    CorOlhos = table.Column<string>(type: "TEXT", nullable: false),
                    CorCabelo = table.Column<string>(type: "TEXT", nullable: false),
                    Altura = table.Column<double>(type: "REAL", nullable: true),
                    Peso = table.Column<double>(type: "REAL", nullable: true),
                    TatuagensMarcas = table.Column<string>(type: "TEXT", nullable: false),
                    FotoFrontal = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FotoPerfilDireito = table.Column<byte[]>(type: "BLOB", nullable: true),
                    FotoPerfilEsquerdo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Endereco = table.Column<string>(type: "TEXT", nullable: false),
                    Bairro = table.Column<string>(type: "TEXT", nullable: false),
                    Cidade = table.Column<string>(type: "TEXT", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CrimesSuspeitos = table.Column<string>(type: "TEXT", nullable: false),
                    NivelPericulosidade = table.Column<string>(type: "TEXT", nullable: false),
                    Foragido = table.Column<bool>(type: "INTEGER", nullable: false),
                    MandadoPrisao = table.Column<bool>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    Observacoes = table.Column<string>(type: "TEXT", nullable: false),
                    Comparsas = table.Column<string>(type: "TEXT", nullable: false),
                    FaccaoCriminosa = table.Column<string>(type: "TEXT", nullable: false),
                    LocaisFrequentados = table.Column<string>(type: "TEXT", nullable: false),
                    VeiculosUsados = table.Column<string>(type: "TEXT", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsuarioResponsavel = table.Column<string>(type: "TEXT", nullable: false),
                    Foto = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
