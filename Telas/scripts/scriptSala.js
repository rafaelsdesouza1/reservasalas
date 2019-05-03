var tbody = document.querySelector('table tbody');
var selectLocal = document.querySelector('#LocalId');
var sala = {};

function CadastrarSala() {
    sala.Nome = document.querySelector('#Nome').value;
    sala.LocalId = document.querySelector('#LocalId').value;

    if (((sala.LocalId === undefined || sala.LocalId == "")
        || (sala.Nome === undefined || sala.Nome == "")) 
    && (sala.id === undefined || sala.id == "")) {
        $('#salvarModalSala').modal('show');
    } else if ((sala.LocalId === undefined || sala.LocalId == "")
    || (sala.Nome === undefined || sala.Nome == "")) {
        var msg = document.querySelector('#msgModalSalaText');
        msg.textContent = `Preencha os dados obrigatórios!`;
        $('#msgModalSala').modal('show');
    } else {
        SalvarSala(sala.id, sala);
        $('#cadastrarModalSala').modal('hide');

        CarregarSalas();
    }
}

function CarregarSalas() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:55886/api/Sala/ListarSalas`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var salas = JSON.parse(this.responseText);

                for (var indice in salas) {
                    AdicionarLinhaSala(salas[indice]);
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }

    xhr.send();
}

function CarregarLocais() {
    selectLocal.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:55886/api/Local/ListarLocais`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var locais = JSON.parse(this.responseText);

                for (var indice in locais) {
                    AdicionarOptionLocal(locais[indice]);
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

function ExcluirSala() {
    var xhr = new XMLHttpRequest();
    xhr.open('DELETE', `http://localhost:55886/api/Sala/${sala.id}`, false);
    xhr.send();
}

function ExcluirSalaLinha(salaLinha) {
    var msg = document.querySelector('#msgExlcuirModalLabel');
    msg.textContent = `Tem certeza que deseja excluir a sala - ${salaLinha.nome}?`;

    $('#excluirModalSala').modal('show');
    sala = salaLinha;
}

function ConfirmarExlcusaoSala() {
    var xhr = new XMLHttpRequest();
    xhr.open(`GET`, `http://localhost:55886/api/Reserva/ListarReservaPorSala/${sala.id}`, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var reserva = JSON.parse(this.responseText);

                if (reserva.nome == null) {
                    ExcluirSala();
                    CarregarSalas();
                } else {
                    var msg = document.querySelector('#msgModalSalaText');
                    msg.textContent = `Você não pode excluir esta sala porque ela possui reservas cadastradas.`;
                    $('#msgModalSala').modal('show');
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }
    xhr.send();
}

function NovaSala() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalSalaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Sala";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#Nome').value = "";

    sala = {};
    $('#salvarModalSala').modal('hide');
    $('#cadastrarModalSala').modal('show');
}

function CancelarSala() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalSalaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Sala";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#Nome').value = "";
    
    sala = {};
    $('#cadastrarModalSala').modal('hide');
}

function CancelarExclusaoSala() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalSalaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Sala";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#Nome').value = "";

    sala = {};
    $('#excluirModalSala').modal('hide');
}

function EditarSala(salaLinha) {
    $('#cadastrarModalSala').modal('show');
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalSalaLabel');
    btnSalvar.textContent = "Salvar";
    titulo.textContent = `Editar Sala - ${salaLinha.nome}`;

    document.querySelector('#LocalId').value = salaLinha.localId;
    document.querySelector('#Nome').value = salaLinha.nome;
    sala = salaLinha;
}

function AdicionarLinhaSala(salaLinha) {
    var tr = `<tr>
                <td>${salaLinha.local.nome}</td>
                <td>${salaLinha.nome}</td>
                <td>
                    <button class='btn btn-info' onclick='EditarSala(${JSON.stringify(salaLinha)});'>Editar</button>
                    <button class='btn btn-danger' onclick='ExcluirSalaLinha(${JSON.stringify(salaLinha)});'>Excluir</button>
                </td>
            </tr>`

    tbody.innerHTML += tr;
}

function AdicionarOptionLocal(localLinha) {
    var opt = `<option value=${localLinha.id}>${localLinha.nome}</option>`

    selectLocal.innerHTML += opt;
}

CarregarSalas();
CarregarLocais();