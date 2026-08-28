// Portal de Autorizacion de Estudios de Imagen - Contoso (DEMO Tech Day)
// -----------------------------------------------------------------------------
// AVISO: este archivo contiene vulnerabilidades INTENCIONALES y estilo "legacy"
// para la demo de GitHub Advanced Security (code scanning) y GitHub Copilot
// (modernizacion). NO usar en produccion.
// -----------------------------------------------------------------------------

var express = require('express');
var _ = require('lodash');
var exec = require('child_process').exec;
var app = express();

// "Base de datos" en memoria (mock) para la demo
var estudios = [
  { id: '1001', paciente: 'Paciente Demo 001', tipo: 'TAC', contraste: true, estado: 'pendiente' },
  { id: '1002', paciente: 'Paciente Demo 002', tipo: 'RM', contraste: false, estado: 'autorizado' }
];

app.get('/', function (req, res) {
  res.send('Portal de Autorizacion de Estudios - Contoso');
});

// [DEMO CodeQL: Reflected XSS] el parametro del usuario se refleja sin sanitizar
app.get('/buscar', function (req, res) {
  var q = req.query.q;
  res.send('<h1>Resultados para: ' + q + '</h1>');
});

// [DEMO CodeQL: Command Injection] entrada del usuario concatenada a un comando
app.get('/reporte', function (req, res) {
  var nombre = req.query.nombre;
  exec('echo Generando reporte para ' + nombre, function (err, stdout) {
    if (err) { res.status(500).send('error'); return; }
    res.send(stdout);
  });
});

// Estilo legacy: callbacks anidados y var; candidato para modernizar con Copilot
app.get('/estudios/:id', function (req, res) {
  buscarEstudio(req.params.id, function (err, estudio) {
    if (err) {
      res.status(500).send('error interno');
    } else {
      if (estudio == null) {
        res.status(404).send('no encontrado');
      } else {
        res.json(estudio);
      }
    }
  });
});

function buscarEstudio(id, cb) {
  setTimeout(function () {
    var found = _.find(estudios, function (e) { return e.id === id; });
    cb(null, found || null);
  }, 10);
}

var PORT = process.env.PORT || 3000;
if (require.main === module) {
  app.listen(PORT, function () {
    console.log('Portal escuchando en http://localhost:' + PORT);
  });
}

module.exports = { app: app, buscarEstudio: buscarEstudio };
