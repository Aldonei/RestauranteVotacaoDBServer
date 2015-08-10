using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RestauranteVotacao.Models
{
    public class Colaborador
    {
        #region Variáveis públicas e privadas.
        private static List<Colaborador> Colaboradores;
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        
        #endregion
        
        #region Entrada de dados do Colaborador.
        static Colaborador()
        {
            // 1. Criam-se 5 colaboradores fictícios, já que não existe banco de dados.
            Colaborador colaborador1 = new Colaborador(
                1000, 
                "Colaborador Um", 
                "colaborador1@gmail.com", 
                ObterSenha("senha1"));
            
            Colaborador colaborador2 = new Colaborador(
                2000,
                "Colaborador Dois",
                "colaborador2@gmail.com",
                ObterSenha("senha2"));
            
            Colaborador colaborador3 = new Colaborador(
                3000,
                "Colaborador Três",
                "colaborador3@gmail.com", 
                ObterSenha("senha3"));

            Colaborador colaborador4 = new Colaborador(
                4000,
                "Colaborador Quatro",
                "colaborador4@gmail.com", 
                ObterSenha("senha4"));

            Colaborador colaborador5 = new Colaborador(
                5000,
                "Colaborador Cinco",
                "colaborador5@gmail.com", 
                ObterSenha("senha5"));
            
            // 2. Coloca-se estes 5 colabores numa lista.
            Colaboradores = new List<Colaborador>();
            Colaboradores.Add(colaborador1);
            Colaboradores.Add(colaborador2);
            Colaboradores.Add(colaborador3);
            Colaboradores.Add(colaborador4);
            Colaboradores.Add(colaborador5);
        }
        #endregion
        
        #region Criptografia da senha.
        public static string ObterSenha(string senha)
        {
            // Apenas faz um Hash da senha usando o MD5. 
            HashAlgorithm hash =  MD5.Create();
            return System.Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(senha)));
        }
        #endregion

        #region Autenticação do colaborador.
        public static RestauranteVotacao.Models.Colaborador Autenticar(string email, string senha)
        {
            // Busca-se pelo e-mail e senha do colaborador.
            RestauranteVotacao.Models.Colaborador colaborador = RestauranteVotacao.Models.Colaborador.ObterTodosColaboradores().Find(
                c => c.Email.Equals(email) 
                    && c.Senha.Equals(ObterSenha(senha)));

            return colaborador;
        }
        #endregion

        #region Contrutor do Colaborador.
        public Colaborador(int id, string nome, string email, string senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.Senha = senha;
        }
        #endregion

        #region Método público para buscar todos os colaboradores.
        public static List<Colaborador> ObterTodosColaboradores()
        {
            return Colaboradores;
        }
        #endregion
    }
}