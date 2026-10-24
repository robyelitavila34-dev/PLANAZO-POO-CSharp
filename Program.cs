
using PLANAZO.Modelos;
using PLANAZO.Servicios;

GestorReservas gestor = new GestorReservas();

// EVENTOS DE EJEMPLO
gestor.RegistrarEvento(new Cine(
    1, "Interestelar", DateTime.Today.AddDays(5),
    20000m, 50));

gestor.RegistrarEvento(new Cine(
    2, "El origen", DateTime.Today.AddDays(8),
    18000m, 40));

gestor.RegistrarEvento(new Concierto(
    3, "Concierto de Rock", DateTime.Today.AddDays(15),
    50000m, 100));

gestor.RegistrarEvento(new Concierto(
    4, "Noche de Pop", DateTime.Today.AddDays(20),
    40000m, 80));

gestor.RegistrarEvento(new Taller(
    5, "Taller de Pintura", DateTime.Today.AddDays(10),
    40000m, 25));

gestor.RegistrarEvento(new Taller(
    6, "Taller de Programación", DateTime.Today.AddDays(12),
    60000m, 30));

bool ejecutando = true;

while (ejecutando)
{
    Console.WriteLine("\n================================");
    Console.WriteLine("       PLANAZO - EVENTOS");
    Console.WriteLine("================================");
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("7. Registrar usuario");
    Console.WriteLine("8. Consultar reservas por usuario");
    Console.WriteLine("9. Ver usuarios registrados");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                VerEventos(gestor);
                break;

            case "2":
                ConsultarEvento(gestor);
                break;

            case "3":
                ReservarEntradas(gestor);
                break;

            case "4":
                VerReservas(gestor);
                break;

            case "5":
                CancelarReserva(gestor);
                break;

            case "6":
                VerResumen(gestor);
                break;

            case "7":
                RegistrarUsuario(gestor);
                break;

            case "8":
                ConsultarReservasUsuario(gestor);
                break;

            case "9":
                VerUsuarios(gestor);
                break;

            case "0":
                ejecutando = false;
                Console.WriteLine(
                    "Gracias por utilizar PLANAZO.");
                break;

            default:
                Console.WriteLine(
                    "Opción inválida. Intente nuevamente.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(
            $"Error de validación: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(
            $"Operación no permitida: {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"Ocurrió un error: {ex.Message}");
    }
}

// VER EVENTOS
static void VerEventos(GestorReservas gestor)
{
    Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");

    bool hayEventos = false;

    foreach (Evento evento in gestor.Eventos)
    {
        if (evento.CuposDisponibles > 0)
        {
            Console.WriteLine(evento);
            hayEventos = true;
        }
    }

    if (!hayEventos)
        Console.WriteLine("No hay eventos disponibles.");
}

// CONSULTAR EVENTO
static void ConsultarEvento(GestorReservas gestor)
{
    Console.Write("Ingrese el ID del evento: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Debe ingresar un número entero.");
        return;
    }

    Evento? evento = gestor.BuscarEvento(id);

    if (evento == null)
    {
        Console.WriteLine("No se encontró el evento.");
        return;
    }

    Console.WriteLine("\nInformación del evento:");
    Console.WriteLine(evento);

    Console.WriteLine(
        $"Precio final por entrada: {evento.CalcularPrecioFinal():C0}");
}

// REGISTRAR USUARIO
static void RegistrarUsuario(GestorReservas gestor)
{
    Console.Write("Nombre del usuario: ");

    string nombre = Console.ReadLine() ?? "";

    Usuario usuario = gestor.RegistrarUsuario(nombre);

    Console.WriteLine("\n¡Usuario registrado correctamente!");
    Console.WriteLine(usuario);
}

// VER USUARIOS
static void VerUsuarios(GestorReservas gestor)
{
    Console.WriteLine("\n--- USUARIOS REGISTRADOS ---");

    if (gestor.Usuarios.Count == 0)
    {
        Console.WriteLine("No hay usuarios registrados.");
        return;
    }

    foreach (Usuario usuario in gestor.Usuarios)
    {
        Console.WriteLine(usuario);
    }
}

// RESERVAR ENTRADAS
static void ReservarEntradas(GestorReservas gestor)
{
    if (gestor.Usuarios.Count == 0)
    {
        Console.WriteLine(
            "Primero debe registrar un usuario en la opción 7.");
        return;
    }

    Console.WriteLine("\n--- USUARIOS ---");

    foreach (Usuario usuario in gestor.Usuarios)
    {
        Console.WriteLine(usuario);
    }

    Console.Write("ID del usuario: ");

    if (!int.TryParse(Console.ReadLine(), out int usuarioId))
    {
        Console.WriteLine("El ID debe ser un número entero.");
        return;
    }

    Usuario? usuarioSeleccionado =
        gestor.BuscarUsuario(usuarioId);

    if (usuarioSeleccionado == null)
    {
        Console.WriteLine("No se encontró el usuario.");
        return;
    }

    Console.WriteLine($"\nUsuario seleccionado: {usuarioSeleccionado.Nombre}");

    Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");
    VerEventos(gestor);

    Console.Write("\nID del evento: ");

    if (!int.TryParse(Console.ReadLine(), out int eventoId))
    {
        Console.WriteLine("El ID debe ser un número entero.");
        return;
    }

    Console.Write("Cantidad de entradas: ");

    if (!int.TryParse(Console.ReadLine(), out int cantidad))
    {
        Console.WriteLine("La cantidad debe ser un número entero.");
        return;
    }

    Reserva reserva = gestor.CrearReserva(
        usuarioId,
        eventoId,
        cantidad);

    Console.WriteLine("\n¡Reserva realizada correctamente!");
    Console.WriteLine(reserva);
}

// VER TODAS LAS RESERVAS
static void VerReservas(GestorReservas gestor)
{
    Console.WriteLine("\n--- RESERVAS REALIZADAS ---");

    if (gestor.Reservas.Count == 0)
    {
        Console.WriteLine("Todavía no hay reservas.");
        return;
    }

    foreach (Reserva reserva in gestor.Reservas)
    {
        Console.WriteLine(reserva);
    }
}

// CONSULTAR RESERVAS DE UN USUARIO
static void ConsultarReservasUsuario(
    GestorReservas gestor)
{
    if (gestor.Usuarios.Count == 0)
    {
        Console.WriteLine("No hay usuarios registrados.");
        return;
    }

    Console.WriteLine("\n--- USUARIOS REGISTRADOS ---");

    foreach (Usuario usuario in gestor.Usuarios)
    {
        Console.WriteLine(usuario);
    }

    Console.Write("\nIngrese el ID del usuario: ");

    if (!int.TryParse(Console.ReadLine(), out int usuarioId))
    {
        Console.WriteLine("Debe ingresar un número entero.");
        return;
    }

    Usuario? usuarioSeleccionado =
        gestor.BuscarUsuario(usuarioId);

    if (usuarioSeleccionado == null)
    {
        Console.WriteLine("No se encontró el usuario.");
        return;
    }

    Console.WriteLine(
        $"\n--- RESERVAS DE {usuarioSeleccionado.Nombre} ---");

    List<Reserva> reservasUsuario =
        gestor.ObtenerReservasPorUsuario(usuarioId);

    if (reservasUsuario.Count == 0)
    {
        Console.WriteLine(
            "Este usuario todavía no tiene reservas.");
        return;
    }

    foreach (Reserva reserva in reservasUsuario)
    {
        Console.WriteLine(reserva);
    }
}

// CANCELAR RESERVA
static void CancelarReserva(GestorReservas gestor)
{
    Console.Write("Ingrese el ID de la reserva: ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("El ID debe ser un número entero.");
        return;
    }

    gestor.CancelarReserva(id);

    Console.WriteLine("La reserva se canceló correctamente.");
    Console.WriteLine("Los cupos fueron devueltos.");
}

// RESUMEN DE RESERVAS
static void VerResumen(GestorReservas gestor)
{
    Console.WriteLine("\n--- RESUMEN DE RESERVAS ---");

    Console.WriteLine(
        $"Reservas activas: {gestor.ContarReservasActivas()}");

    Console.WriteLine(
        $"Entradas reservadas: {gestor.ContarEntradasActivas()}");

    Console.WriteLine(
        $"Valor total: {gestor.CalcularValorReservasActivas():C0}");
}
