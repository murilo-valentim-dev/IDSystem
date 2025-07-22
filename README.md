# 📘 IDSystem

## 📄 Descrição

O **IDSystem** é um sistema de gerenciamento de identificação de usuários desenvolvido em **C# com WPF (.NET)**. Ele oferece funcionalidades de cadastro, busca e armazenamento de dados pessoais por meio de uma interface gráfica moderna, limpa e intuitiva.

## ✨ Funcionalidades Principais

### 🧾 Cadastro de Pessoas
Tela dedicada para inserir dados pessoais como nome, CPF, e-mail, entre outros campos relevantes.

### 🔍 Busca de Usuários
Tela com `DataGrid` que permite pesquisas por nome, CPF ou outros identificadores de forma dinâmica.

### 🧱 Estrutura Modular
Arquitetura com arquivos XAML separados por funcionalidade (ex: `CadastroPessoa.xaml`, `BuscaUsuario.xaml`), facilitando manutenção e extensões futuras.

### 🎨 Dicionário de Estilos e Recursos
Uso de `ResourceDictionary` (`IDDictionary.xaml`) para centralizar recursos visuais como cores, estilos e templates, garantindo identidade visual consistente.

### 🧠 Padrão MVVM (presumido)
Separação de responsabilidades entre **Views**, **ViewModels** e **Models**, melhorando testabilidade e organização de código.

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C#
- **Interface Gráfica:** WPF (.NET Framework ou .NET Core/5+)
- **Controle de Versão:** Git + GitHub
- **Armazenamento de Dados:** (a definir) – exemplo: banco SQLite, arquivos JSON/XML

## 🏗️ Estrutura do Projeto

```text 
IDSystem/
├── App.xaml # Ponto de entrada + ResourceDictionary global
├── MainWindow.xaml/.cs # Janela principal
├── CadastroPessoa.xaml/.cs # View e código do cadastro de pessoa
├── BuscaUsuario.xaml/.cs # View e lógica de busca de usuários
├── IDDictionary.xaml # Estilos e recursos compartilhados
├── Models/ # Classes de modelo de dados (ex: Pessoa.cs)
├── ViewModels/ # Classes ViewModel correspondentes às Views
└── Services/ # Serviços para acesso a dados, etc.
``` 

## 🚀 Como Executar o Projeto

### 📥 1. Clone o repositório

git clone https://github.com/murilo-valentim-dev/IDSystem.git
cd IDSystem

### 🧩 2. Abra no Visual Studio 2022

Vá em `File → Open → Project/Solution` e selecione `IDSystem.sln`.

### 📦 3. Restaure dependências NuGet (se houver)

Clique com o botão direito na solução → `Restore NuGet Packages`.

### 🛠️ 4. Configure o banco de dados (se aplicável)

- Se usar **SQLite**, defina o caminho do banco no arquivo `appsettings.json`.
- Se usar arquivos **JSON/XML**, defina o diretório correto onde os dados ficarão armazenados.

### ▶️ 5. Compile e Execute

Pressione `F5` ou clique em **Start** para executar o projeto.

---

## 🧪 Testes

- Atualmente, não há framework de testes configurado.
- Recomenda-se uso de **xUnit**, **NUnit** ou **MSTest** para testes unitários.

### Exemplos de testes a implementar:
- ✅ Validação de CPF  
- ✅ Verificação de campos obrigatórios no cadastro  
- ✅ Funcionamento dos filtros de busca  

---

## ⚡ Contribuições

Contribuições são bem-vindas!  
Siga os passos abaixo para contribuir com melhorias:

# Faça um fork do repositório
git clone https://github.com/seu-usuario/IDSystem.git

# Crie uma nova branch
git checkout -b minha-feature

# Faça suas alterações e commite
git commit -m "Minha contribuição"

# Envie para seu repositório remoto
git push origin minha-feature


