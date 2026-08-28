// Prueba minima con el runner nativo de Node (node --test). En la demo, pide a
// Copilot: "genera mas casos de prueba para buscarEstudio, incluyendo id inexistente".
const test = require('node:test');
const assert = require('node:assert');
const { buscarEstudio } = require('../src/server');

test('buscarEstudio devuelve el estudio existente', (t, done) => {
  buscarEstudio('1001', (err, estudio) => {
    assert.strictEqual(err, null);
    assert.strictEqual(estudio.paciente, 'Paciente Demo 001');
    done();
  });
});

test('buscarEstudio devuelve null si no existe', (t, done) => {
  buscarEstudio('9999', (err, estudio) => {
    assert.strictEqual(err, null);
    assert.strictEqual(estudio, null);
    done();
  });
});
