var tbody = document.querySelector('table tbody');
var selectLocal = document.querySelector('#LocalId');
var selectSala = document.querySelector('#SalaId');
var reserva = {};

function CadastrarReserva() {
    reserva.SalaId = document.querySelector('#SalaId').value;
    reserva.Nome = document.querySelector('#Nome').value;
    reserva.DtHrIni = document.querySelector('#DtHrIni').value + " " + document.querySelector('#DtHrIniHr').value;
    reserva.DtHrFim = document.querySelector('#DtHrFim').value + " " + document.querySelector('#DtHrFimHr').value;
    reserva.Responsavel = document.querySelector('#Responsavel').value;
    reserva.Cafe = document.querySelector('#Cafe').value;
    reserva.QtdePessoas = document.querySelector('#QtdePessoas').value;

    if (((reserva.SalaId === undefined || reserva.SalaId == "")
        || (reserva.Nome === undefined || reserva.Nome == "")
        || (reserva.DtHrIni === undefined || reserva.DtHrIni == "")
        || (reserva.DtHrFim === undefined || reserva.DtHrFim == "")
        || (reserva.Responsavel === undefined || reserva.Responsavel == ""))
        && (reserva.id === undefined || reserva.id == "")) {
        $('#salvarModalReserva').modal('show');
    } else if ((reserva.SalaId === undefined || reserva.SalaId == "")
        || (reserva.Nome === undefined || reserva.Nome == "")
        || (reserva.DtHrIni === undefined || reserva.DtHrIni == "")
        || (reserva.DtHrFim === undefined || reserva.DtHrFim == "")
        || (reserva.Responsavel === undefined || reserva.Responsavel == "")) {
        var msg = document.querySelector('#msgModalReservaText');
        msg.textContent = `Preencha os dados obrigatórios!`;
        $('#msgModalReserva').modal('show');
    } else {
        var HrIni = document.querySelector('#DtHrIniHr').value;
        var HrFim = document.querySelector('#DtHrFimHr').value;
        var HrIniRpl = HrIni.replace(":", "-").replace(":", "-");
        var HrFimRpl = HrFim.replace(":", "-").replace(":", "-");
        ValidarDisponibilidadeSala(reserva.id, reserva.SalaId, document.querySelector('#DtHrIni').value, HrIniRpl, document.querySelector('#DtHrFim').value, HrFimRpl);
        $('#cadastrarModalReserva').modal('hide');
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

function CarregarSalas(Id) {
    selectSala.innerHTML = '';

    var xhr = new XMLHttpRequest();
    xhr.open(`GET`, `http://localhost:55886/api/Sala/ListarSalasPorLocal/${Id}`, true);
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

function ValidarDisponibilidadeSala(Id, SalaId, DtIni, HrIni, DtFim, HrFim) {
    var xhr = new XMLHttpRequest();
    xhr.open(`GET`, `http://localhost:55886/api/Reserva/ValidarDisponibilidadeSala/${SalaId}/${DtIni}/${HrIni}/${DtFim}/${HrFim}`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var ReservaChoque = JSON.parse(this.responseText);

                if (ReservaChoque.nome == null) {
                    SalvarReserva(reserva.id, reserva);
                } else {
                    var msg = document.querySelector('#msgModalReservaText');
                    msg.textContent = `Reserva não cadastrada por choque de horário.`;
                    $('#msgModalReserva').modal('show');
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

    if (reserva.QtdePessoas === undefined || reserva.QtdePessoas == null || reserva.QtdePessoas == "")
        reserva.QtdePessoas = 0;

    xhr.open('PUT', `http://localhost:55886/api/Reserva/${Id}`, false);
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(reserva));

    CarregarReservas();
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
    document.querySelector('#DtHrIniHr').value = "";
    document.querySelector('#DtHrFimHr').value = "";
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
    document.querySelector('#DtHrIniHr').value = "";
    document.querySelector('#DtHrFimHr').value = "";
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
    document.querySelector('#DtHrIniHr').value = "";
    document.querySelector('#DtHrFimHr').value = "";
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

    CarregarSalas(reservaLinha.sala.localId);
    var DtHrI = reservaLinha.dtHrIni.split("T");
    var DtHrF = reservaLinha.dtHrFim.split("T");

    document.querySelector('#LocalId').value = reservaLinha.sala.localId;
    document.querySelector('#SalaId').value = reservaLinha.salaId;
    document.querySelector('#Nome').value = reservaLinha.nome;
    document.querySelector('#DtHrIni').value = DtHrI[0];
    document.querySelector('#DtHrFim').value = DtHrF[0];
    document.querySelector('#DtHrIniHr').value = DtHrI[1];
    document.querySelector('#DtHrFimHr').value = DtHrF[1];
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