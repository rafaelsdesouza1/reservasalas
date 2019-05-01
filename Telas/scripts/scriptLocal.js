var tbody = document.querySelector('table tbody');
var selectLocal = document.querySelector('#LocalId');
var local = {};

function CadastrarLocal() {
    local.Nome = document.querySelector('#Nome').value;

    if ((local.Nome === undefined || local.Nome == "") && (local.id === undefined || local.id == "")) {
        $('#salvarModalLocal').modal('show');
    } else if (local.Nome === undefined || local.Nome == "") {
        var msg = document.querySelector('#msgModalLocalText');
        msg.textContent = `Nome é obrigatório!`;
        $('#msgModalLocal').modal('show');
    } else {
        SalvarLocal(local.id, local);
        $('#cadastrarModalLocal').modal('hide');

        CarregarLocais();
    }
}

function CarregarLocais() {
    tbody.innerHTML = '';

    var xhr = new XMLHttpRequest();

    xhr.open(`GET`, `http://localhost:55886/api/Local/ListarLocais`, true);
    xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));

    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var Locais = JSON.parse(this.responseText);

                for (var indice in Locais) {
                    AdicionarLinhaLocal(Locais[indice]);
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }

    xhr.send();
}

function SalvarLocal(Id, local) {
    var xhr = new XMLHttpRequest();

    if (Id === undefined)
        Id = 0;

    xhr.open('PUT', `http://localhost:55886/api/local/${Id}`, false);
    xhr.setRequestHeader('content-type', 'application/json');
    xhr.send(JSON.stringify(local));
}

function ExcluirLocal() {
    var xhr = new XMLHttpRequest();
    xhr.open('DELETE', `http://localhost:55886/api/local/${local.id}`, false);
    xhr.send();
}

function ExcluirLocalLinha(LocalLinha) {
    var msg = document.querySelector('#msgExlcuirModalLabel');
    msg.textContent = `Tem certeza que deseja excluir a local - ${LocalLinha.nome}?`;

    $('#excluirModalLocal').modal('show');
    local = LocalLinha;
}

function ConfirmarExlcusaoLocal() {
    var xhr = new XMLHttpRequest();
    xhr.open(`GET`, `http://localhost:55886/api/Sala/ListarSalaPorLocal/${local.id}`, true);
    xhr.onreadystatechange = function () {
        if (this.readyState == 4) {
            if (this.status == 200) {
                var sala = JSON.parse(this.responseText);

                if (sala.nome == null) {
                    ExcluirLocal();
                    CarregarLocais();
                } else {
                    var msg = document.querySelector('#msgModalLocalText');
                    msg.textContent = `Você não pode excluir este local/filial porque ele possui salas cadastradas.`;
                    $('#msgModalLocal').modal('show');
                }
            } else if (this.status == 500) {
                var erro = JSON.parse(this.responseText);
            }
        }
    }
    xhr.send();
}

function NovoLocal() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#cadastrarModalLocalLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar local";

    document.querySelector('#Nome').value = "";

    local = {};
    $('#salvarModalLocal').modal('hide');
    $('#cadastrarModalLocal').modal('show');
}

function CancelarLocal() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#cadastrarModalLocalLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar local";

    document.querySelector('#Nome').value = "";
    local = {};
    $('#cadastrarModalLocal').modal('hide');
}

function CancelarExclusaoLocal() {
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#cadastrarModalLocalLabel');
    btnSalvar.textContent = "Cadastrar";
    titulo.textContent = "Cadastrar local";

    document.querySelector('#Nome').value = "";
    local = {};
    $('#excluirModalLocal').modal('hide');
}

function EditarLocal(LocalLinha) {
    $('#cadastrarModalLocal').modal('show');
    var btnSalvar = document.querySelector('#btnSalvar');
    var btnCancelar = document.querySelector('#btnCancelar');
    var titulo = document.querySelector('#cadastrarModalLocalLabel');
    btnSalvar.textContent = "Salvar";
    titulo.textContent = `Editar local - ${LocalLinha.nome}`;

    document.querySelector('#Nome').value = LocalLinha.nome;
    local = LocalLinha;
}

function AdicionarLinhaLocal(LocalLinha) {
    var tr = `<tr>
                <td>${LocalLinha.nome}</td>
                <td>
                    <button class='btn btn-info' onclick='EditarLocal(${JSON.stringify(LocalLinha)});'>Editar</button>
                    <button class='btn btn-danger' onclick='ExcluirLocalLinha(${JSON.stringify(LocalLinha)});'>Excluir</button>
                </td>
            </tr>`

    tbody.innerHTML += tr;
}

CarregarLocais();