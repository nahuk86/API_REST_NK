Construir un API REST que reciba "documentos" con prioridad 1 - 10 , simule su impresion de forma asincrona en segundo plano, registre las impresiones exitosas en una base simple y permita consultar si un documento se imprimio.


1. Requerimientos funcionales

   * POST /jobs
     Recibe: {name: string, content?: string, priority: number (1-10)}
     Devuelve: {id, status:"PENDING"}. Valida rango de prioridad

   * GET /jobs/{id}
     Devuelve el estado del trabajo: PENDING | PRINTED | ERROR, printedAt si aplica y los datos basicos

   *GET /printed{name}
     Devuelve si un documeto con name fue impreso; si fue impreso, retorna JSON con name, printedAt, insertedAt (registro en "auditoria"); si no, retorna JSON no satisfactorio.

   *Procesamiento asincrono sin colas
   un worker en segundo plano (en el mismo proceso) selecciona periodicamente el pendiente de mayor prioridad y:
       * con probabilidad configurable (p.ej 70%) lo marca PRINTED, guardando en la tabla/log de impresiones: name, printedAt, insertedAt;
       * con 30% lo deja en PENDING (no responde "error", para emular el "silencio ante fallas" del original)
  
   * Reintento manual: POST /jobs/{id}/retry para volver a poner PENDING
  
2. Req adicionales
   a. persistencia en archivo JSON
   b. separar el proyecto en capas: servicio, dominio, api, negocio, persistencia
   c. Respuestas JSON, codigo HTTP correcto, validaciones basicas.

   No es necesario UI
   No usar colas ni brokers externos
