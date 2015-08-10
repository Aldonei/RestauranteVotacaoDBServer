# RestauranteVotacaoDBServer
Sistema de escolha de restaurante para os colaboradores da DB Server
Candidato: Aldonei de Avila Souza (aldoneiavila@gmail.com)
Descrição do Sistema
	Sistema para auxiliar os colaboradores da DB Server na tomada de decisão de onde almoçar usando um sistema de votação de restaurantes pré-selecionados.

Dados Técnicos
	Linguagem: C#
	Ferramenta de desenvolvimento: Microsoft Visual Studio Professional 2013
	Arquitetura: ASP.NET MVC 5
	Framework: .NET Framework 4.5
	Front-end Framework: Bootstrap

Realização de Testes
	Testes realizados no Google Chrome

Instalação
	1. Faça download do arquivo zip: Teste-DBServer.zip.
	2. Descompacte num diretório preferencial. 
3. Abre este o arquivo RestauranteVotacao.sln (solução) no Microsoft Visual Studio Professional 2013 ou Visual Studio 2013 Community Edition.
	4. Execute-o com o comando Run. (http://localhost:52052/) 

Telas do sistema
	1. Formulário de Login para registrar usuários do sistema com E-mail e Senha (Login.cshtml).
	Como não existe uma base de dados, foram criados cinco (5) usuários hardcoded que são carregados quando o programa é executado.
		Id: 1000
		Nome: Colaborador Um
		E-mail: colaborador1@gmail.com 
		Senha: senha1
		
		Id: 2000
		Nome: Colaborador Dois
		E-mail: colaborador2@gmail.com 
		Senha: senha2
		
		Id: 3000
		Nome: Colaborador Três
		E-mail: colaborador3@gmail.com 
		Senha: senha3
		
		Id: 4000
		Nome: Colaborador Quatro
		E-mail: colaborador4@gmail.com 
		Senha: senha4
		Id: 5000
		Nome: Colaborador Cinco
		E-mail: colaborador5@gmail.com 
		Senha: senha5
	
 

2. Formulário para selecionar o restaurante desejado (Index.cshtml) com o nome de seis (6) restaurante pré-cadastrados. 
		Id: 1000
		Nome: Morte Lenta
		
Id: 2000
		Nome: Bandejão
		
		Id: 3000
		Nome: Restaurante da Tia Louca
		
		Id: 4000
		Nome: Gangue da Calça Branca
		
		Id: 5000
		Nome: Você Não Tem Coragem
		
		Id: 6000
		Nome: Carpano Restaurante

Overview do Sistema		
	1. Formulário de Login.
	Com apenas dois campos (e-mail e senha) e sem a pretensão de ser seguro, pois apenas visa manter um registro dos votos para cada restaurante. 
	Sendo assim a autenticação é bem simples. Caso estivéssemos conectados a um banco de dados	dentro do domínio  da DB Server, poder-se-ia usar a própria autenticação do Active Directory (AD) do Windows, por exemplo.
	
	2. Formulário de Seleção dos Restaurantes.
	Após a "autenticação" do colaborador. Exibe-se uma lista com seis (6) restaurantes para que os colaboradores façam a escolha dele.
	
	3. Importante.
	a) O sistema permite através das setas (esquerda e direita) navegar nos dias da semana, para frente e para trás.
	b) Considera-se a data do sistema operacional como padrão.
	c) Não foi implementado nenhum critério de desempate, logo o primeiro restaurante que aparecer na lista com mais votos será eleito.
	d) Sábado e Domingo não são contabilizados
	e) Não trata feriados, sempre cinco dias úteis.

 
 

	4. Testes Unitários.
	Criou-se testes unitários para que fossem cobertas pelo menos as Estórias 1, 2 e 3.

Conclusão Final
	Como é um programa relativamente simples não requer muito processamento mesmo que estivesse conectado a uma base de dados. Logo em termos performáticos ele atende ao que é proposto.
	A maior dificuldade que encontrei foi front-end Boostrap, pois ainda não havia trabalhado com ele. Foi minha primeira experiência. Obtive algumas informações do siste da Microsoft para obter alguma ajuda.  https://code.msdn.microsoft.com/ASPNET-MVC-5-Bootstrap-30-622be454.
	Acredito que este programa poderia ser mais planejado. Se fosse, o grau de dificuldade dele cairia bastante. Com isto melhoraria a construção dos objetos também.
	Faltou a implementação de um critério de desempate. Hoje o programa pega o primeiro mais votado que encontrar na lista.
	O design das telas poderia ser melhorado também, um pouco foi pela minha falta de experiência com o Boostrap. Apesar disto ainda existe um pouco de inteligência em algums momentos. 

