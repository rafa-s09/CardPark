namespace CardPark.Models;

public class ClienteModel
{
    public required int TipoPessoa { get; set; }
    public required string CpfCnpj { get; set; }
    public required string NomeRazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public char? Sexo { get; set; }
    public DateTime? DataNascimento { get; set; }
    public string? Rg { get; set; }
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Telefone { get; set; }
    public string? TelefoneAlt { get; set; }
    public string? Email { get; set; }
    public string? EmailAlt { get; set; }
}
