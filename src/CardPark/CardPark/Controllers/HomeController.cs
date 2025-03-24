using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CardPark.Models;
using CardPark.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CardPark.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private CardParkDBContext _dBContext;

    public HomeController(ILogger<HomeController> logger, CardParkDBContext context)
    {
        _logger = logger;
        _dBContext = context;
    }

    #region Views    

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Juridico()
    {
        return View();
    }

    public IActionResult Cadastro()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion Views

    #region HttpGet    

    [HttpGet]
    public IActionResult GetClientesFisicos()
    {
        using (var connection = _dBContext.Database.GetDbConnection())
        {
            connection.Open();

            string sql = @"SELECT tipo_pessoa, cpf_cnpj, nome_razao_social, sexo, data_nascimento, rg, cep, 
                         logradouro, numero, complemento, bairro, cidade, estado, telefone, telefone_alt, email, email_alt
                         FROM clientes WHERE tipo_pessoa = 1;";

            var clientes = new List<ClienteModel>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new ClienteModel
                        {
                            TipoPessoa = Convert.ToInt32(reader["tipo_pessoa"]),
                            CpfCnpj = reader["cpf_cnpj"].ToString(),
                            NomeRazaoSocial = reader["nome_razao_social"].ToString(),
                            Sexo = reader["sexo"] != DBNull.Value ? Convert.ToChar(reader["sexo"]) : null,
                            DataNascimento = reader["data_nascimento"] != DBNull.Value ? Convert.ToDateTime(reader["data_nascimento"]) : null,
                            Rg = reader["rg"].ToString(),
                            Cep = reader["cep"].ToString(),
                            Logradouro = reader["logradouro"].ToString(),
                            Numero = reader["numero"].ToString(),
                            Complemento = reader["complemento"].ToString(),
                            Bairro = reader["bairro"].ToString(),
                            Cidade = reader["cidade"].ToString(),
                            Estado = reader["estado"].ToString(),
                            Telefone = reader["telefone"].ToString(),
                            TelefoneAlt = reader["telefone_alt"].ToString(),
                            Email = reader["email"].ToString(),
                            EmailAlt = reader["email_alt"].ToString()
                        });
                    }
                }
            }

            return Json(clientes);
        }
    }

    [HttpGet]
    public IActionResult GetClientesJuridicos()
    {
        using (var connection = _dBContext.Database.GetDbConnection())
        {
            connection.Open();

            string sql = @"SELECT tipo_pessoa, cpf_cnpj, nome_razao_social, nome_fantasia, cep, 
                         logradouro, numero, complemento, bairro, cidade, estado, telefone, telefone_alt, email, email_alt
                         FROM clientes WHERE tipo_pessoa = 2;";

            var clientes = new List<ClienteModel>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(new ClienteModel
                        {
                            TipoPessoa = Convert.ToInt32(reader["tipo_pessoa"]),
                            CpfCnpj = reader["cpf_cnpj"].ToString(),
                            NomeRazaoSocial = reader["nome_razao_social"].ToString(),
                            NomeFantasia = reader["nome_fantasia"].ToString(),
                            Cep = reader["cep"].ToString(),
                            Logradouro = reader["logradouro"].ToString(),
                            Numero = reader["numero"].ToString(),
                            Complemento = reader["complemento"].ToString(),
                            Bairro = reader["bairro"].ToString(),
                            Cidade = reader["cidade"].ToString(),
                            Estado = reader["estado"].ToString(),
                            Telefone = reader["telefone"].ToString(),
                            TelefoneAlt = reader["telefone_alt"].ToString(),
                            Email = reader["email"].ToString(),
                            EmailAlt = reader["email_alt"].ToString()
                        });
                    }
                }
            }

            return Json(clientes);
        }
    }

    #endregion HttpGet

    #region HttpPost

    [HttpPost]
    public IActionResult RegisterClient([FromBody] ClienteModel cliente)
    {
        if (ModelState.IsValid)
        {
            using (var connection = _dBContext.Database.GetDbConnection())
            {
                connection.Open();

                string checkSql = @"SELECT COUNT(*) FROM clientes WHERE cpf_cnpj = @CpfCnpj;";
                using (var checkCommand = connection.CreateCommand())
                {
                    checkCommand.CommandText = checkSql;
                    AddParameter(checkCommand, "@CpfCnpj", cliente.CpfCnpj);

                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                        return BadRequest(new { message = cliente.TipoPessoa == 1 ? "CPF já cadastrado." : "CNPJ já cadastrado." });

                }

                string sql = @"INSERT INTO clientes (tipo_pessoa, cpf_cnpj, nome_razao_social, nome_fantasia, sexo, data_nascimento, rg, cep, logradouro, numero, complemento, bairro, cidade, estado, telefone, telefone_alt, email, email_alt)
                    VALUES (@TipoPessoa, @CpfCnpj, @NomeRazaoSocial, @NomeFantasia, @Sexo, @DataNascimento, @Rg, @Cep, @Logradouro, @Numero, @Complemento, @Bairro, @Cidade, @Estado, @Telefone, @TelefoneAlt, @Email, @EmailAlt);";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    AddParameter(command, "@TipoPessoa", cliente.TipoPessoa);
                    AddParameter(command, "@CpfCnpj", cliente.CpfCnpj);
                    AddParameter(command, "@NomeRazaoSocial", cliente.NomeRazaoSocial);
                    AddParameter(command, "@NomeFantasia", cliente.NomeFantasia);
                    AddParameter(command, "@Sexo", cliente.Sexo);
                    AddParameter(command, "@DataNascimento", cliente.DataNascimento);
                    AddParameter(command, "@Rg", cliente.Rg);
                    AddParameter(command, "@Cep", cliente.Cep);
                    AddParameter(command, "@Logradouro", cliente.Logradouro);
                    AddParameter(command, "@Numero", cliente.Numero);
                    AddParameter(command, "@Complemento", cliente.Complemento);
                    AddParameter(command, "@Bairro", cliente.Bairro);
                    AddParameter(command, "@Cidade", cliente.Cidade);
                    AddParameter(command, "@Estado", cliente.Estado);
                    AddParameter(command, "@Telefone", cliente.Telefone);
                    AddParameter(command, "@TelefoneAlt", cliente.TelefoneAlt);
                    AddParameter(command, "@Email", cliente.Email);
                    AddParameter(command, "@EmailAlt", cliente.EmailAlt);

                    command.ExecuteNonQuery();
                }
            }

            return Ok(new { message = "Cadastro realizado com sucesso!" });
        }

        return BadRequest(ModelState);
    }

    #endregion HttpPost

    #region Private

    private void AddParameter(DbCommand command, string name, object? value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
    }

    #endregion Private
}
