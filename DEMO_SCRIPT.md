# Guion de demo — GitHub (Contoso)

3 sesiones de 15 min. El repo `demo-github` es el mismo para las tres.
**Antes del evento:** sube el repo a una org con GitHub Advanced Security activo, deja que
CodeQL y Dependabot corran una vez (para que las alertas ya existan), y ten Copilot Chat listo.

---

## Sesión 1 (11:25–11:40) — GitHub Enterprise + Advanced Security (DevSecOps)
Objetivo: seguridad de código como parte del flujo, no un paso aparte.

1. (2 min) Pestaña **Security** del repo: panorama (code scanning, Dependabot, secret scanning).
2. (4 min) **Code scanning**: abrir la alerta de **XSS** en `/buscar` y la de **command injection**
   en `/reporte`. Explicar el data-flow que CodeQL muestra (de la entrada al sink).
3. (3 min) **Copilot arregla**: en el archivo, Copilot Chat → "corrige esta vulnerabilidad y
   explica la mitigación". Mostrar el `diff` seguro (sanitizar / `execFile` con argumentos).
4. (3 min) **Dependabot**: alerta de `lodash 4.17.11`; mostrar el PR automático de actualización.
5. (2 min) **Secret scanning / push protection**: intentar commitear una cadena de conexión
   (ver `.env.example`) y mostrar cómo GitHub bloquea el push.
6. (1 min) Cierre: "seguridad desplazada a la izquierda, dentro del PR".

## Sesión 2 (11:40–11:55) — GitHub Copilot + App Modernization
Objetivo: Copilot como *peer programmer* que acelera y moderniza.

1. (2 min) Abrir `src/legacy/reportGenerator.js` (callback hell + `var`).
2. (4 min) Copilot Chat: **"moderniza este archivo a async/await, con const/let y manejo de
   errores"**. Revisar el resultado.
3. (3 min) **"agrega validación de entrada a las rutas de `server.js`"**.
4. (3 min) **"genera casos de prueba para `buscarEstudio`, incluyendo id inexistente"** → correr `npm test`.
5. (2 min) Mostrar `/explain` sobre una función y las **custom instructions**
   (`.github/copilot-instructions.md`, respuestas en español + foco en seguridad).
6. (1 min) Cierre: menos trabajo repetitivo, más foco en el negocio.

## Sesión 3 (11:55–12:10) — ADO + GitHub Copilot
Objetivo: ciclo completo idea → producción.

1. (3 min) Narrativa: del commit al deploy. Mostrar `.github/workflows/ci.yml` (build + test).
2. (4 min) Abrir `azure-pipelines.yml`; con Copilot: **"agrega un stage de análisis de seguridad
   antes del deploy"**. Mostrar cómo Copilot escribe el YAML.
3. (3 min) Mostrar la integración GitHub ↔ Azure DevOps (repos/boards/pipelines) y el gate a
   `producción` con aprobación.
4. (3 min) Ligar de vuelta a la Sesión 1: el pipeline **falla** si CodeQL encuentra algo crítico
   → seguridad como *quality gate*.
5. (2 min) Cierre: idea → PR → checks (test + seguridad) → deploy gobernado.

---

## Plan B
- Si no hay GHAS en la org: mostrar CodeQL corriendo en **Actions** (el workflow `codeql.yml`)
  y los resultados en el log; usar capturas de la pestaña Security de respaldo.
- Tener el repo ya clonado y `npm install` hecho localmente por si la red falla.
- Prompts de Copilot escritos aquí para copiar/pegar (no improvisar bajo el reloj).
