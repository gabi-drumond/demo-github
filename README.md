# Portal de Autorización de Estudios — Demo Contoso

Repo de demostración para el Tech Day de **Contoso**. Una pequeña API en Node/Express
(un "Portal de Autorización de Estudios de Imagen", en línea con el caso de negocio del cliente)
que sirve a las **tres sesiones de GitHub** del día:

| Sesión | Qué se muestra aquí |
|--------|---------------------|
| **GitHub Enterprise + Advanced Security** (DevSecOps) | `code scanning` con CodeQL detecta XSS y command injection; `Dependabot` alerta la dependencia vulnerable; `secret scanning`/push protection con `.env.example`. |
| **GitHub Copilot + App Modernization** | Copilot moderniza el estilo *legacy* (callbacks + `var`) a `async/await`, agrega validación y genera pruebas. |
| **ADO + GitHub Copilot** | `azure-pipelines.yml` + workflows de Actions: ciclo idea → producción, con Copilot generando el pipeline. |

> ⚠️ Contiene vulnerabilidades **intencionales y rotuladas** para la demo. No usar en producción.

## Estructura
```
demo-github/
├─ src/
│  ├─ server.js              # API Express (XSS + command injection intencionales, estilo legacy)
│  └─ legacy/reportGenerator.js  # callback hell para modernizar con Copilot
├─ test/server.test.js       # pruebas con node:test
├─ .github/
│  ├─ workflows/codeql.yml   # code scanning (CodeQL)
│  ├─ workflows/ci.yml       # build + test
│  ├─ dependabot.yml         # alertas de dependencias
│  └─ copilot-instructions.md# instrucciones de Copilot (español, seguridad)
├─ azure-pipelines.yml       # pipeline ADO idea -> prod
├─ .env.example              # para demo de secret scanning
└─ package.json
```

## Correr localmente
```bash
npm install
npm start      # http://localhost:3000
npm test
```

## Hallazgos esperados de GHAS (al subir a GitHub con Advanced Security activo)
- **Code scanning (CodeQL):** Reflected XSS en `/buscar`; Command injection en `/reporte`.
- **Dependabot:** `lodash 4.17.11` (vulnerable) → sugiere actualizar.
- **Secret scanning / push protection:** demostrar con una cadena de conexión (ver `.env.example`).

El guion paso a paso está en `DEMO_SCRIPT.md`.
