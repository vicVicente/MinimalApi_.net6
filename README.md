# Minimal Api

Neste projeto usamos o .net 6 com ef core, swagger e migrations, criamos um web service, com o sistema de minimal api "não possuí controllers", podendo estar tudo dentro da classe principal, "program.cs", porém, como ficou um pouco comprida a classe foi fragmentada em partes

O projeo possuí um sistema JWT, na qual tem que ser feita uma autenticação para poder fazer a requisição na lista de categorias e na lista de produtos, essa autenticação é feita no endpoint Login, onde você irá passar o user: root e a senha: a123. Você pode também criar um endpoint para adicionar user, e deixar esta parte do projeto mais dinâmica.

Não esqueça de fazer o migrations e colocar sua conexão do banco.
