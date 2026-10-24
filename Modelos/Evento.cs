namespace PLANAZO.Modelos;

public abstract class Evento
{
    public int Id { get; }

    public string Nombre { get; private set; }

    public DateTime Fecha { get; private set; }

    public decimal PrecioBase { get; private set; }

    public int CapacidadMaxima { get; }

    public int CuposDisponibles { get; private set; }

    protected Evento(
        int id,
        string nombre,
        DateTime fecha,
        decimal precioBase,
        int capacidadMaxima)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException(
                "El nombre del evento es obligatorio.");
        }

        if (precioBase < 0)
        {
            throw new ArgumentException(
                "El precio no puede ser negativo.");
        }

        if (capacidadMaxima <= 0)
        {
            throw new ArgumentException(
                "La capacidad debe ser mayor que cero.");
        }

        Id = id;
        Nombre = nombre;
        Fecha = fecha;
        PrecioBase = precioBase;
        CapacidadMaxima = capacidadMaxima;
        CuposDisponibles = capacidadMaxima;
    }

    public abstract decimal CalcularPrecioFinal();

    public bool DescontarCupos(int cantidad)
    {
        if (cantidad <= 0 || cantidad > CuposDisponibles)
        {
            return false;
        }

        CuposDisponibles -= cantidad;

        return true;
    }

    public void DevolverCupos(int cantidad)
    {
        if (cantidad <= 0)
        {
            throw new ArgumentException(
                "La cantidad debe ser positiva.");
        }

        if (CuposDisponibles + cantidad > CapacidadMaxima)
        {
            throw new InvalidOperationException(
                "Se superaría la capacidad máxima.");
        }

        CuposDisponibles += cantidad;
    }

    public override string ToString()
    {
        return $"ID: {Id} | {Nombre} | " +
               $"Fecha: {Fecha:dd/MM/yyyy} | " +
               $"Precio: {CalcularPrecioFinal():C0} | " +
               $"Cupos: {CuposDisponibles}/{CapacidadMaxima}";
    }
}