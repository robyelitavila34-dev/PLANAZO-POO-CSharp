namespace PLANAZO.Modelos;

public class Reserva
{
    public int Id { get; }

    public Usuario Usuario { get; }

    public Evento EventoReservado { get; }

    public int CantidadEntradas { get; }

    public decimal ValorTotal { get; }

    public bool EstaActiva { get; private set; }

    public string NombrePersona => Usuario.Nombre;

    public Reserva(
        int id,
        Usuario usuario,
        Evento evento,
        int cantidadEntradas)
    {
        if (usuario == null)
            throw new ArgumentNullException(nameof(usuario));

        if (evento == null)
            throw new ArgumentNullException(nameof(evento));

        if (cantidadEntradas <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.");

        Id = id;
        Usuario = usuario;
        EventoReservado = evento;
        CantidadEntradas = cantidadEntradas;

        ValorTotal =
            evento.CalcularPrecioFinal() * cantidadEntradas;

        EstaActiva = true;
    }

    public void Cancelar()
    {
        if (!EstaActiva)
            throw new InvalidOperationException(
                "La reserva ya está cancelada.");

        EventoReservado.DevolverCupos(CantidadEntradas);
        EstaActiva = false;
    }

    public override string ToString()
    {
        return $"Reserva #{Id} | Usuario: {Usuario.Nombre} " +
               $"(ID: {Usuario.Id}) | Evento: {EventoReservado.Nombre} " +
               $"| Entradas: {CantidadEntradas} " +
               $"| Total: {ValorTotal:C0} " +
               $"| Estado: {(EstaActiva ? "Activa" : "Cancelada")}";
    }
}