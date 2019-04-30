var tbody = document.querySelector('table tbody');
var sala = {};

function Cadastrar() {
    sala.Nome = document.querySelector('#Nome').value;

    SalvarSala(sala.id, sala);
    $('#exampleModal').modal('hide');

    CarregarSalas();
}

function CarregarSalas() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:55886/api/Sala/ListarSalas`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function() {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var salas = JSON.parse(this.responseText);

                for (var indice in salas) {
                    AdicionarLinha(salas[indice]);
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }  
        } 
    }

    xhr.send();    
}

function SalvarSala(Id, sala) {
    var xhr = new XMLHttpRequest();

    if (Id === undefined)
        Id = 0;

    xhr.open('PUT', `http://localhost:55886/api/Sala/${Id}`, false);
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(sala));
}

function ExcluirSala(Id) {
    var xhr = new XMLHttpRequest();
    xhr.open('DELETE', `http://localhost:55886/api/Sala/${Id}`, false);
    xhr.send();
}

function Excluir(salaLinha) {
    if (confirm(`Tem certeza que deseja excluir a sala - ${salaLinha.nome}?`)) {
        ExcluirSala(salaLinha.id);
        CarregarSalas();
    }
}

function NovaSala() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#exampleModalLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Sala";

    document.querySelector('#Nome').value = "";
    sala = {};
    $('#exampleModal').modal('show');
}

function Cancelar() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#exampleModalLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Sala";

    document.querySelector('#Nome').value = "";
    sala = {};
    $('#exampleModal').modal('hide');
}

function Editar(salaLinha) {
    $('#exampleModal').modal('show');
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#exampleModalLabel');
    btnSalvar.textContent = "Salvar";
    titulo.textContent = `Editar Sala - ${salaLinha.nome}`;

    document.querySelector('#Nome').value = salaLinha.nome;
    sala = salaLinha;
}

function AdicionarLinha(salaLinha) {
    var tr = `<tr>
                <td>${salaLinha.nome}</td>
                <td>
                    <button class='btn btn-info' onclick='Editar(${JSON.stringify(salaLinha)});'>Editar</button>
                    <button class='btn btn-danger' onclick='Excluir(${JSON.stringify(salaLinha)});'>Excluir</button>
                </td>
            </tr>`

    tbody.innerHTML += tr;
}

CarregarSalas();