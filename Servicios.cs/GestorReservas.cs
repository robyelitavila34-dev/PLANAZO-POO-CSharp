
using PLANAZO.Modelos;

namespace PLANAZO.Servicios;

public class GestorReservas
{
    private readonly List<Evento> eventos = new();
    private readonly List<Reserva> reservas = new();
    private readonly List<Usuario> usuarios = new();

    private int siguienteReservaId = 1;
    private int siguienteUsuarioId = 1;

    public IReadOnlyList<Evento> Eventos =>
        eventos.AsReadOnly();

    public IReadOnlyList<Reserva> Reservas =>
        reservas.AsReadOnly();

    public IReadOnlyList<Usuario> Usuarios =>
        usuarios.AsReadOnly();

    public void RegistrarEvento(Evento evento)
    {
        if (evento == null)
            throw new ArgumentNullException(nameof(evento));

        if (eventos.Any(e => e.Id == evento.Id))
            throw new InvalidOperationException(
                "Ya existe un evento con ese identificador.");

        eventos.Add(evento);
    }

    public Evento? BuscarEvento(int id)
    {
        return eventos.FirstOrDefault(e => e.Id == id);
    }

    public Usuario RegistrarUsuario(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException(
                "El nombre del usuario es obligatorio.");

        Usuario usuario = new Usuario(
            siguienteUsuarioId,
            nombre);

        usuarios.Add(usuario);
        siguienteUsuarioId++;

        return usuario;
    }

    public Usuario? BuscarUsuario(int id)
    {
        return usuarios.FirstOrDefault(u => u.Id == id);
    }

    public Reserva CrearReserva(
        int usuarioId,
        int eventoId,
        int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        Usuario? usuario = BuscarUsuario(usuarioId);

        if (usuario == null)
            throw new InvalidOperationException(
                "El usuario no existe. Regístrelo primero.");

        Evento? evento = BuscarEvento(eventoId);

        if (evento == null)
            throw new InvalidOperationException(
                "El evento no existe.");

        if (cantidad > evento.CuposDisponibles)
            throw new InvalidOperationException(
                "No hay suficientes cupos disponibles.");

        Reserva reserva = new Reserva(
            siguienteReservaId,
            usuario,
            evento,
            cantidad);

        if (!evento.DescontarCupos(cantidad))
            throw new InvalidOperationException(
                "No fue posible descontar los cupos.");

        reservas.Add(reserva);
        siguienteReservaId++;

        return reserva;
    }

    public List<Reserva> ObtenerReservasPorUsuario(
        int usuarioId)
    {
        if (BuscarUsuario(usuarioId) == null)
            throw new InvalidOperationException(
                "El usuario no existe.");

        return reservas
            .Where(r => r.Usuario.Id == usuarioId)
            .ToList();
    }

    public void CancelarReserva(int reservaId)
    {
        Reserva? reserva = reservas.FirstOrDefault(
            r => r.Id == reservaId);

        if (reserva == null)
            throw new InvalidOperationException(
                "La reserva no existe.");

        reserva.Cancelar();
    }

    public int ContarReservasActivas()
    {
        return reservas.Count(r => r.EstaActiva);
    }

    public int ContarEntradasActivas()
    {
        return reservas
            .Where(r => r.EstaActiva)
            .Sum(r => r.CantidadEntradas);
    }

    public decimal CalcularValorReservasActivas()
    {
        return reservas
            .Where(r => r.EstaActiva)
            .Sum(r => r.ValorTotal);
    }
}
