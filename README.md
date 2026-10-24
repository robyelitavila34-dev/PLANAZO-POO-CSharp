PLANAZO — Sistema de eventos y reservas

Descripción

PLANAZO es una aplicación de consola desarrollada en C# y .NET 8 para gestionar eventos de entretenimiento y reservas de entradas.

El sistema permite consultar eventos, calcular precios, crear reservas, cancelar reservas y consultar un resumen de las operaciones activas.

Tecnologías utilizadas

C#.

.NET 8.

Aplicación de consola.

Colecciones List<T>.

Programación orientada a objetos.

Funcionalidades

Consultar eventos.

Buscar eventos por identificador.

Calcular precios según el tipo de evento.

Reservar entradas.

Controlar los cupos disponibles.

Cancelar reservas.

Devolver los cupos de las reservas canceladas.

Consultar las reservas realizadas.

Consultar el resumen de reservas activas.

Validar las operaciones y controlar errores.

Estructura del proyecto

Program.cs: menú e interacción con el usuario.

Modelos/Evento.cs: clase abstracta con las características comunes.

Modelos/Cine.cs: evento que mantiene el precio base.

Modelos/Concierto.cs: evento que aumenta el precio un 15 %.

Modelos/Taller.cs: evento que aplica un descuento del 10 %.

Modelos/Reserva.cs: representa las reservas.

Servicios/GestorReservas.cs: administra eventos y reservas.

Conceptos de programación orientada a objetos

Abstracción

La clase abstracta Evento representa las características comunes de todos los eventos.

Encapsulamiento

Las propiedades con private set protegen los datos y permiten controlar sus modificaciones.

Herencia

Las clases Cine, Concierto y Taller heredan de Evento.

Polimorfismo

Cada tipo de evento implementa CalcularPrecioFinal() con su propia fórmula.

Asociaciones

La clase Reserva mantiene una referencia al evento reservado.

Colecciones

El gestor utiliza listas para almacenar eventos y reservas.

Excepciones

El programa controla operaciones inválidas mediante validaciones y excepciones.

Ejecución

Es necesario tener instalado el SDK de .NET 8.

Desde la terminal, ejecutar:

dotnet build
dotnet run

Pruebas realizadas

Incluir capturas de las siguientes operaciones:

Consulta de eventos.

Reserva válida.

Cantidad negativa.

Cupos insuficientes.

Cancelación de reserva.

Intento de doble cancelación.

Consulta de evento inexistente.

Resumen de reservas activas.

Pregunta de sustentación

¿Por qué utilizar una clase abstracta Evento en lugar de crear clases independientes?

Se utiliza una clase abstracta porque Cine, Concierto y Taller comparten características como el identificador, el nombre, la fecha, el precio base y la capacidad.

La clase Evento permite definir estos elementos una sola vez y establecer un método común para calcular el precio final.

Cada clase derivada implementa su propia regla. Cine mantiene el precio base, Concierto aumenta el precio un 15 % y Taller aplica un descuento del 10 %.

Esta estructura reduce la duplicación de código y facilita agregar nuevos tipos de eventos mediante herencia y polimorfismo.