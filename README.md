# Curso de arquitetura back-end com .NET Core e configuração da Arquitetura do front-end


![image](https://github.com/AmauriDallOglio/Curso/assets/13471113/5eb3c6e2-4414-4c27-83ae-8b714f7803f4)



Cadastro de usuário:
Durante o cadastro, o usuário fornece informações pessoais, como nome, endereço de e-mail e senha.
Essas informações são validadas pelo sistema para garantir que sejam fornecidas de forma correta e completa.
O sistema verifica se o endereço de e-mail fornecido pelo usuário já está registrado no sistema para evitar duplicatas.
Se todas as informações estiverem corretas e únicas, o sistema cria uma nova conta de usuário e armazena os dados do usuário no banco de dados.
Após o cadastro bem-sucedido, o usuário recebe uma confirmação e pode acessar sua nova conta.

Login de usuário:
O usuário acessa a página de login e insere suas credenciais, incluindo o endereço de e-mail e a senha.
O sistema verifica as credenciais inseridas pelo usuário em relação aos dados armazenados no banco de dados.
Se as credenciais estiverem corretas e correspondentes, o sistema autentica o usuário e concede acesso à sua conta.
Caso contrário, se as credenciais estiverem incorretas ou não correspondentes, o sistema notifica o usuário e solicita que ele tente novamente.

Cadastro de curso autêntico:
Após fazer login, o usuário acessa a seção de cadastro de curso.
O usuário preenche um formulário com informações detalhadas sobre o novo curso, como título, descrição, conteúdo, duração e preço.
O sistema valida os dados fornecidos pelo usuário para garantir que todas as informações necessárias estejam presentes e sejam formatadas corretamente.
Se o formulário estiver completo e correto, o sistema cria um novo registro para o curso no banco de dados e o associa ao usuário autenticado.
Após o cadastro bem-sucedido do curso, o usuário recebe uma confirmação e o curso é adicionado à sua lista de cursos disponíveis.

Lista de cursos do usuário autenticado:
Após fazer login, o usuário acessa a página de sua conta ou perfil, onde pode visualizar uma lista de cursos disponíveis.
O sistema consulta o banco de dados em busca de cursos associados à conta do usuário autenticado.
Os cursos são exibidos em uma lista na página do usuário, mostrando detalhes como título, descrição, autor/instrutor e preço.
O usuário pode visualizar os detalhes de cada curso e selecionar aqueles que deseja acessar ou comprar.
Se houver muitos cursos, o sistema pode fornecer recursos de filtragem ou pesquisa para facilitar a navegação e a seleção.
