
WHENEVER SQLERROR EXIT SQL.SQLCODE ROLLBACK;            
ALTER SESSION SET CONTAINER = XEPDB1;
ALTER SESSION SET CURRENT_SCHEMA = RESTAURANTE;
    
BEGIN
  MERGE INTO cliente c
  USING (
    SELECT 'CC10000001' iden, 'Juan' nom, 'Perez' ape, 'Calle 1 #10-20' dir, '3000000001' tel FROM dual UNION ALL
    SELECT 'CC10000002', 'Maria', 'Gomez', 'Carrera 15 #20-30', '3000000002' FROM dual UNION ALL
    SELECT 'CC10000003', 'Carlos', 'Ramirez', 'Av. Siempre Viva #742', '3000000003' FROM dual UNION ALL
    SELECT 'CC10000004', 'Lucia', 'Martinez', 'Diagonal 50 #12-40', '3000000004' FROM dual UNION ALL
    SELECT 'CC10000005', 'Andres', 'Torres', 'Transversal 8 #9-11', '3000000005' FROM dual
  ) s
  ON (c.identificacion = s.iden)
  WHEN NOT MATCHED THEN
    INSERT (identificacion, nombres, apellidos, direccion, telefono)
    VALUES (s.iden, s.nom, s.ape, s.dir, s.tel);

  MERGE INTO mesero m
  USING (
    SELECT 'Sofia' nom, 'Castro' ape, 24 ed, 2 ant FROM dual UNION ALL
    SELECT 'Diego', 'Herrera', 29, 4 FROM dual UNION ALL
    SELECT 'Valentina', 'Rojas', 31, 6 FROM dual UNION ALL
    SELECT 'Mateo', 'Suarez', 22, 1 FROM dual
  ) s
  ON (m.nombres = s.nom AND m.apellidos = s.ape)
  WHEN NOT MATCHED THEN
    INSERT (nombres, apellidos, edad, antiguedad)
    VALUES (s.nom, s.ape, s.ed, s.ant);

  MERGE INTO supervisor sup
  USING (
    SELECT 'Ana' nom, 'Moreno' ape, 35 ed, 8 ant FROM dual UNION ALL
    SELECT 'Jorge', 'Benitez', 41, 10 FROM dual UNION ALL
    SELECT 'Paula', 'Vargas', 38, 9 FROM dual
  ) x
  ON (sup.nombres = x.nom AND sup.apellidos = x.ape)
  WHEN NOT MATCHED THEN
    INSERT (nombres, apellidos, edad, antiguedad)
    VALUES (x.nom, x.ape, x.ed, x.ant);

  MERGE INTO mesa m
  USING (
    SELECT 1 nro, 'Mesa Ventana 1' nom, 0 res, 4 p FROM dual UNION ALL
    SELECT 2, 'Mesa Centro 2', 0, 2 FROM dual UNION ALL
    SELECT 3, 'Mesa Familiar 3', 0, 6 FROM dual UNION ALL
    SELECT 4, 'Mesa Barra 4', 1, 3 FROM dual UNION ALL
    SELECT 5, 'Mesa Terraza 5', 0, 4 FROM dual
  ) s
  ON (m.nro_mesa = s.nro)
  WHEN NOT MATCHED THEN
    INSERT (nro_mesa, nombre, reservada, puestos)
    VALUES (s.nro, s.nom, s.res, s.p);

  INSERT INTO factura (nro_factura, id_cliente, id_mesa, id_mesero, fecha)
  SELECT 1001,
         (SELECT id_cliente FROM cliente WHERE identificacion = 'CC10000001'),
         (SELECT id_mesa FROM mesa WHERE nro_mesa = 1),
         (SELECT id_mesero FROM mesero WHERE nombres = 'Sofia' AND apellidos = 'Castro'),
         TRUNC(SYSDATE) - 3
  FROM dual
  WHERE NOT EXISTS (SELECT 1 FROM factura WHERE nro_factura = 1001);

  INSERT INTO detalle_factura (id_factura, id_supervisor, plato, valor)
  SELECT (SELECT id_factura FROM factura WHERE nro_factura = 1001),
         (SELECT id_supervisor FROM supervisor WHERE nombres = 'Ana' AND apellidos = 'Moreno'),
         'Hamburguesa Clasica', 28.50
  FROM dual
  WHERE EXISTS (SELECT 1 FROM factura WHERE nro_factura = 1001)
    AND NOT EXISTS (
      SELECT 1
      FROM detalle_factura
      WHERE id_factura = (SELECT id_factura FROM factura WHERE nro_factura = 1001)
        AND plato = 'Hamburguesa Clasica'
        AND valor = 28.50
    );

  COMMIT;
EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
    RAISE;
END;
/