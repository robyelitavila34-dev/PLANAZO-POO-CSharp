namespace PLANAZO.Modelos;

public class Usuario
{
    public int Id { get; }

    public string Nombre { get; }

    public Usuario(int id, string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException(
                "El nombre es obligatorio.");
        }

        Id = id;
        Nombre = nombre;
    }

    public override string ToString()
    {
        return $"Usuario #{Id}: {Nombre}";
    }
}
