# CardPark
Projeto de teste


## Cenário
Um estacionamento possui um acervo físico de cartões com dados de seus clientes, esses cartões podem conter informações de pessoas físicas e pessoas jurídicas, os cartões são organizados em ordem alfabética para facilitar a localização:

>O cartão de Pessoa Física tem as seguintes informações:
- CPF (*)
- Nome (*)
- Sexo
- Data de Nascimento
- RG
- Endereço
- Telefones
- E-mails

>O cartão de Pessoa Jurídica tem as seguintes informações:
- CNPJ (*)
- Razão Social (*)
- Nome Fantasia
- Endereço
- Telefones
- E-mail

Obs.: Campos com (*) são obrigatórios, não pode ter mais de uma pessoa com o mesmo CPF (Pessoa Física) ou CNPJ (Pessoa Jurídica)

## Problema
Quando algum funcionário, guarda o cartão fora da ordem, fica impossível localizar o cliente, quando isso ocorre, os funcionários do estacionamento criam um outro cartão, gerando duplicidade de informações no arquivo e com isso perde-se todo o histórico de pagamento do cliente. Só e possível localizar o cartão pelo nome, se tiver 100 pessoas com o mesmo nome, fica muito difícil o trabalho de localização.

## Necessidade
O estacionamento necessita que seja criado um cadastro, para que ele possa cadastrar os dados contidos no cartão, e que também consiga fazer pesquisa pelos seguintes campos:

>Pessoa Física:
- CPF, Nome e RG

>Pessoa Jurídica
- CNPJ, Razão Social e Nome Fantasia