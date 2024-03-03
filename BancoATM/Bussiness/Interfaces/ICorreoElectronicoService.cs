using System;

public interface ICorreoElectronicoService
{
    void EnviarCorreo(string destinatario, string asunto, string cuerpo);
}
