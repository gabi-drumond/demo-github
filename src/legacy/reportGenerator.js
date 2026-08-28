// Generador de reportes de autorizacion - estilo LEGACY (callback hell + var).
// Objetivo en la demo: pedir a GitHub Copilot que lo modernice a async/await,
// agregue validacion de entrada y genere pruebas.

var fs = require('fs');

function generarReporte(idEstudio, callback) {
  cargarEstudio(idEstudio, function (err, estudio) {
    if (err) {
      callback(err);
    } else {
      cargarPaciente(estudio.pacienteId, function (err2, paciente) {
        if (err2) {
          callback(err2);
        } else {
          cargarPolitica(estudio.tipo, function (err3, politica) {
            if (err3) {
              callback(err3);
            } else {
              var reporte = {
                estudio: estudio,
                paciente: paciente,
                politica: politica,
                generadoEn: new Date().toISOString()
              };
              callback(null, reporte);
            }
          });
        }
      });
    }
  });
}

function cargarEstudio(id, cb) { setTimeout(function () { cb(null, { id: id, tipo: 'TAC', pacienteId: 'P1' }); }, 5); }
function cargarPaciente(id, cb) { setTimeout(function () { cb(null, { id: id, nombre: 'Paciente Demo 001' }); }, 5); }
function cargarPolitica(tipo, cb) { setTimeout(function () { cb(null, { tipo: tipo, requiereConsentimiento: true }); }, 5); }

module.exports = { generarReporte: generarReporte };
