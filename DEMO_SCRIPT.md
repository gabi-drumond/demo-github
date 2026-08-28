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

## Sesión 2 (parte del bloque 11:05–11:45) — GitHub Copilot + App Modernization
Objetivo: mostrar **GitHub Copilot upgrade** como un proceso gobernado de modernización.

1. (2 min) Abrir Copilot Chat y seleccionar el agente **Upgrade**. Conectar con la Sesión 1:
   Dependabot encontró `lodash 4.17.11`; ahora convertiremos el hallazgo en un plan ejecutable.
2. (3 min) Pedir el assessment sin autorizar cambios todavía:
   **"Moderniza las dependencias de este proyecto JavaScript. Prioriza corregir lodash 4.17.11,
   conserva el comportamiento de la API Express y primero presenta el assessment y el plan;
   no ejecutes cambios hasta que yo los apruebe."**
3. (2 min) Revisar el inventario, la ruta de actualización y las tareas propuestas. Mostrar que
   cada tarea se puede aprobar, editar o excluir antes de modificar el repositorio.
4. (4 min) Aprobar la actualización necesaria. Revisar el diff de `package.json` y
   `package-lock.json` y, si se genera, la trazabilidad bajo `.github/upgrades/`.
5. (1 min) Ejecutar `npm test` y confirmar 2/2 pruebas en verde. Ejecutar `npm audit` para
   comprobar que el riesgo de lodash fue remediado o reducido.
6. (1 min) Cierre: modernizar es evaluar → planificar → aprobar → transformar → validar.

**Plan B:** llevar capturas del assessment y del diff. Si la extensión tarda, mostrar el plan
ya generado y ejecutar solamente la validación.

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
