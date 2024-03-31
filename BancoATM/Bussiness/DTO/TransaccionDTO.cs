public class TransaccionDTO
{
    public int Id { get; set; }
    public int? Edo_cuenta { get; set; }
    public int Id_Tipo { get; set; }
    public TipoTransaccion TipoTransaccion { get; set; }
    public int CompareTo(Transaccion other)
    {
        // Comparación por nombre
        return this.Importe.CompareTo(other.Importe);
    }

    public int Estatus { get; set; } = 1;

    public string Descripcion { get; set; }

    public decimal Importe { get; set; }
    public string Referencia { get; set; }
    public int Id_Cuenta { get; set; }
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}
