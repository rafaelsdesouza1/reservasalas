var tbody = document.querySelector('table tbody');
var selectLocal = document.querySelector('#LocalId');
var selectSala = document.querySelector('#SalaId');
var reserva = {};

function CadastrarReserva() {
    reserva.SalaId = document.querySelector('#SalaId').value;
    reserva.Nome = document.querySelector('#Nome').value;
    reserva.DtHrIni = document.querySelector('#DtHrIni').value;
    reserva.DtHrFim = document.querySelector('#DtHrFim').value;
    reserva.Responsavel = document.querySelector('#Responsavel').value;
    reserva.Cafe = document.querySelector('#Cafe').value;
    reserva.QtdePessoas = document.querySelector('#QtdePessoas').value;

    if ((reserva.Nome === undefined || reserva.Nome == "") && (reserva.id === undefined || reserva.id == "")) {
        $('#salvarModalReserva').modal('show');
    } else if (reserva.Nome === undefined || reserva.Nome == "") {
        var msg = document.querySelector('#msgModalReservaText');
        msg.textContent = `Nome é obrigatório!`;
        $('#msgModalReserva').modal('show');
    } else {
        SalvarReserva(reserva.id, reserva);
        $('#cadastrarModalReserva').modal('hide');

        CarregarReservas();
    }
}

function CarregarReservas() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:55886/api/Reserva/ListarReservas`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var reservas = JSON.parse(this.responseText);

                for (var indice in reservas) {
                    AdicionarLinhaReserva(reservas[indice]);
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }

    xhr.send();
}

function CarregarLocais() {
    tbody.innerHTML = '';

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
                    AdicionarOptionSala(salas[indice]);
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }

    xhr.send();
}

function SalvarReserva(Id, reserva) {
    var xhr = new XMLHttpRequest();

    if (Id === undefined)
        Id = 0;

    xhr.open('PUT', `http://localhost:55886/api/Reserva/${Id}`, false);
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(reserva));
}

function ExcluirReserva() {
    var xhr = new XMLHttpRequest();
    xhr.open('DELETE', `http://localhost:55886/api/Reserva/${reserva.id}`, false);
    xhr.send();
}

function ExcluirReservaLinha(reservaLinha) {
    var msg = document.querySelector('#msgExlcuirModalLabel');
    msg.textContent = `Tem certeza que deseja excluir a reserva - ${reservaLinha.nome}?`;

    $('#excluirModalReserva').modal('show');
    reserva = reservaLinha;
}

function ConfirmarExlcusaoReserva() {
    ExcluirReserva();
    CarregarReservas();
}

function NovaReserva() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalReservaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Reserva";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#SalaId').value = "";
    document.querySelector('#Nome').value = "";
    document.querySelector('#DtHrIni').value = "";
    document.querySelector('#DtHrFim').value = "";
    document.querySelector('#Responsavel').value = "";
    document.querySelector('#Cafe').value = "";
    document.querySelector('#QtdePessoas').value = "";

    reserva = {};
    $('#salvarModalReserva').modal('hide');
    $('#cadastrarModalReserva').modal('show');
}

function CancelarReserva() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalReservaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Reserva";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#SalaId').value = "";
    document.querySelector('#Nome').value = "";
    document.querySelector('#DtHrIni').value = "";
    document.querySelector('#DtHrFim').value = "";
    document.querySelector('#Responsavel').value = "";
    document.querySelector('#Cafe').value = "";
    document.querySelector('#QtdePessoas').value = "";

    reserva = {};
    $('#cadastrarModalReserva').modal('hide');
}

function CancelarExclusaoReserva() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalReservaLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar Reserva";

    document.querySelector('#LocalId').value = "";
    document.querySelector('#SalaId').value = "";
    document.querySelector('#Nome').value = "";
    document.querySelector('#DtHrIni').value = "";
    document.querySelector('#DtHrFim').value = "";
    document.querySelector('#Responsavel').value = "";
    document.querySelector('#Cafe').value = "";
    document.querySelector('#QtdePessoas').value = "";

    reserva = {};
    $('#excluirModalReserva').modal('hide');
}

function EditarReserva(reservaLinha) {
    $('#cadastrarModalReserva').modal('show');
    var btnSalvar = document.querySelector('#btnSalvar');
    var titulo = document.querySelector('#cadastrarModalReservaLabel');
    btnSalvar.textContent = "Salvar";
    titulo.textContent = `Editar Reserva - ${reservaLinha.nome}`;

    document.querySelector('#LocalId').value = reservaLinha.sala.localId;
    document.querySelector('#SalaId').value = reservaLinha.salaId;
    document.querySelector('#Nome').value = reservaLinha.nome;
    document.querySelector('#DtHrIni').value = reservaLinha.dtHrIni;
    document.querySelector('#DtHrFim').value = reservaLinha.dtHrFim;
    document.querySelector('#Responsavel').value = reservaLinha.responsavel;
    document.querySelector('#Cafe').value = reservaLinha.cafe;
    document.querySelector('#QtdePessoas').value = reservaLinha.qtdePessoas;
    reserva = reservaLinha;
}

function AdicionarLinhaReserva(reservaLinha) {
    var tr = `<tr>
                <td>${reservaLinha.sala.nome}</td>
                <td>${reservaLinha.nome}</td>
                <td>${reservaLinha.dtHrIni}</td>
                <td>${reservaLinha.dtHrFim}</td>
                <td>${reservaLinha.responsavel}</td>
                <td>
                    <button class='btn btn-info' onclick='EditarReserva(${JSON.stringify(reservaLinha)});'>Editar</button>
                    <button class='btn btn-danger' onclick='ExcluirReservaLinha(${JSON.stringify(reservaLinha)});'>Excluir</button>
                </td>
            </tr>`

    tbody.innerHTML += tr;
}

function AdicionarOptionLocal(localLinha) {
    var opt = `<option value=${localLinha.id}>${localLinha.nome}</option>`

    selectLocal.innerHTML += opt;
}

function AdicionarOptionSala(salaLinha) {
    var opt = `<option value=${salaLinha.id}>${salaLinha.nome}</option>`

    selectSala.innerHTML += opt;
}

CarregarReservas();
CarregarLocais();
CarregarSalas();