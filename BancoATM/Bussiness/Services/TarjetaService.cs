using System;
using System.Collections.Generic;

public class TarjetaService : ITarjetaService
{
    private readonly ITarjetaRepository _tarjetaRepository;

    public async Task<(bool, string)> validateNip(string nip, int id, bool validador)
    {
        int validarNip = 0;
        var tarjeta = await this.GetTarjetaById(id);
        if (nip.Length != 4)
        {
            return (false, "El nip debe ser de 4 digitos");
        }
        else if (!int.TryParse(nip, out validarNip))
        {
            return (false, "El nip debe ser numerico" );
        }
        else if (tarjeta == null)
        {
            return (false, "Cliente no encontrado" );
        }
        else if (nip != tarjeta.Nip && validador)
        {
            return (false, "Nip incorrecto, intente de nuevo" );
        }
        else
        {
            return (true, "Validación exitosa" );
        }
    }
    public TarjetaService(ITarjetaRepository tarjetaRepository)
    {
        _tarjetaRepository = tarjetaRepository;
    }

    public async Task<TarjetaDTO> GetTarjetaById(int id)
    {
        var tarjetaEntity = await _tarjetaRepository.GetById(id);
        return Mapper.MapTarjetaToDTO(tarjetaEntity);
    }


    public async Task<TarjetaDTO> GetTarjetaByNip(string tarjeta)
    {
        var tarjetaEntity = await _tarjetaRepository.GetByTD(tarjeta);
        return Mapper.MapTarjetaToDTO(tarjetaEntity);
    }
    public async Task<List<TarjetaDTO>> GetAllTarjetas()
    {
        List<TarjetaDTO> list = new List<TarjetaDTO>();
        var items =  await _tarjetaRepository.GetAll();
        items.ForEach(dto =>
        {
            list.Add(Mapper.MapTarjetaToDTO(dto));
        });
        return list;
    }

    public async Task<TarjetaDTO> AddTarjeta(Tarjeta tarjeta)
    {
        return Mapper.MapTarjetaToDTO(await _tarjetaRepository.Add(tarjeta));
    }

    public async Task<TarjetaDTO> UpdateTarjeta(TarjetaDTO tarjeta)
    {
        return Mapper.MapTarjetaToDTO(await _tarjetaRepository.Update(Mapper.MapTarjetaToDTO(tarjeta)));
    }

    public async Task<int> DeleteTarjeta(int id)
    {
        var tarjeta = await _tarjetaRepository.GetById(id);
        if (tarjeta != null)
        {
            return await _tarjetaRepository.Delete(tarjeta);
        }
        return 0;
    }
}
