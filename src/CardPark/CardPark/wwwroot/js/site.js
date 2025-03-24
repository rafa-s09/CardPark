function exibirCampos() {
    const tipoCliente = document.getElementById('tipoCliente').value;
    const fisica = document.getElementById('pessoaFisica');
    const juridica = document.getElementById('pessoaJuridica');

    if (tipoCliente === 'fisica') {
        fisica.classList.remove('d-none');
        juridica.classList.add('d-none');
        document.getElementById('cpf').required = true;
        document.getElementById('cnpj').required = false;
    } else {
        fisica.classList.add('d-none');
        juridica.classList.remove('d-none');
        document.getElementById('cpf').required = false;
        document.getElementById('cnpj').required = true;
    }
}

document.getElementById('cadastroForm').addEventListener('submit', async function (event) {
    event.preventDefault();

    const formData = new FormData(event.target);

    const cliente = {
        TipoPessoa: formData.get('tipoCliente') === 'fisica' ? 1 : 2,
        CpfCnpj: formData.get('tipoCliente') === 'fisica' ? formData.get('cpf') : formData.get('cnpj'),
        NomeRazaoSocial: formData.get('tipoCliente') === 'fisica' ? formData.get('nome') : formData.get('razaoSocial'),
        NomeFantasia: formData.get('nomeFantasia') || '',
        Sexo: formData.get('sexo') || null,
        DataNascimento: formData.get('dataNascimento') || null,
        Rg: formData.get('rg') || '',
        Cep: formData.get('cep') || '',
        Logradouro: formData.get('logradouro') || '',
        Numero: formData.get('numero') || '',
        Complemento: formData.get('complemento') || '',
        Bairro: formData.get('bairro') || '',
        Cidade: formData.get('cidade') || '',
        Estado: formData.get('estado') || '',
        Telefone: formData.get('telefone') || '',
        TelefoneAlt: formData.get('telefonealt') || '',
        Email: formData.get('email') || '',
        EmailAlt: formData.get('emailalt') || ''
    };

    try {
        const response = await fetch('/Home/RegisterClient', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cliente)
        });

        if (response.ok) {
            alert('Cadastro realizado com sucesso!');
            window.location.href = '/Home/Index';
        } else {
            const error = await response.json();
            alert('Erro ao cadastrar: ' + (error.message || 'Erro desconhecido'));
        }
    } catch (error) {
        console.error('Erro na requisição:', error);
        alert('Ocorreu um erro ao enviar os dados.');
    }
});

function validarCampos() {
    const tipoCliente = document.getElementById('tipoCliente').value;

    if (tipoCliente === 'fisica') {
        const cpf = document.getElementById('cpf').value;
        if (!validarCPF(cpf)) return false;
    } else {
        const cnpj = document.getElementById('cnpj').value;
        if (!validarCNPJ(cnpj)) return false;
    }

    const rg = document.getElementById('rg').value;
    if (rg && !/^(\d{2}\.\d{3}\.\d{3}-\d{1})$/.test(rg)) {
        alert('RG inválido. Formato correto: 00.000.000-0');
        return false;
    }

    return true;
}

function validarCPF(cpf) {
    cpf = cpf.replace(/[\D]/g, '');
    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) {
        alert('CPF inválido!');
        return false;
    }

    let soma = 0;
    for (let i = 0; i < 9; i++) soma += parseInt(cpf.charAt(i)) * (10 - i);
    let resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.charAt(9))) return false;

    soma = 0;
    for (let i = 0; i < 10; i++) soma += parseInt(cpf.charAt(i)) * (11 - i);
    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.charAt(10))) return false;

    return true;
}

function validarCNPJ(cnpj) {
    cnpj = cnpj.replace(/[\D]/g, '');
    if (cnpj.length !== 14) {
        alert('CNPJ inválido!');
        return false;
    }

    let tamanho = cnpj.length - 2;
    let numeros = cnpj.substring(0, tamanho);
    const digitos = cnpj.substring(tamanho);

    let soma = 0;
    let pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    let resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== parseInt(digitos.charAt(0))) return false;

    tamanho = tamanho + 1;
    numeros = cnpj.substring(0, tamanho);
    soma = 0;
    pos = tamanho - 7;
    for (let i = tamanho; i >= 1; i--) {
        soma += numeros.charAt(tamanho - i) * pos--;
        if (pos < 2) pos = 9;
    }

    resultado = soma % 11 < 2 ? 0 : 11 - (soma % 11);
    if (resultado !== parseInt(digitos.charAt(1))) return false;

    return true;
}

function aplicarMascara(input, mascara) {
    input.addEventListener('input', function (e) {
        let i = 0;
        const valor = e.target.value.replace(/\D/g, '');
        e.target.value = mascara.replace(/#/g, () => valor[i++] || '');
    });
}

aplicarMascara(document.getElementById('cpf'), '###.###.###-##');
aplicarMascara(document.getElementById('cnpj'), '##.###.###/####-##');
aplicarMascara(document.getElementById('rg'), '##.###.###-#');
aplicarMascara(document.getElementById('telefone'), '(##) #####-####');
aplicarMascara(document.getElementById('telefonealt'), '(##) #####-####');
aplicarMascara(document.getElementById('cep'), '#####-###');