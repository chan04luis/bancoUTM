using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using ConsoleTables;
using Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IClienteRepository, ClienteRepository>();
        services.AddSingleton<ITarjetaRepository, TarjetaRepository>();
        services.AddSingleton<ITarjetaService, TarjetaService>();


        services.AddSingleton<ITipoTransaccionRepository, TipoTransaccionRepository>();
        services.AddSingleton<ITipoTransaccionService, TipoTransaccionService>();
        services.AddSingleton <ICorreoElectronicoService, CorreoElectronicoService > ();


        services.AddSingleton<IATMRepository, ATMRepository>();
        services.AddSingleton<IATMService, ATMService>();


        services.AddSingleton<ITransaccionRepository, TransaccionRepository>();
        services.AddSingleton<ITransaccionService, TransaccionService>();

        services.AddSingleton<IServicioRepository, ServicioRepository>();
        services.AddSingleton<IServicioService, ServicioService>();

        services.AddDbContext<ApplicationDbContext>();
        var serviceProvider = services.BuildServiceProvider();

        var containerBuilder = new ContainerBuilder();
        containerBuilder.Populate(services);
        var container = containerBuilder.Build();
        using (var scope = container.BeginLifetimeScope())
        {
            bool valido = false;
            var tarjetaService = scope.Resolve<ITarjetaService>();
            var tipoTransaccionService = scope.Resolve<ITipoTransaccionService>();
            var emailService = scope.Resolve<ICorreoElectronicoService>();
            var transactionsService = scope.Resolve<ITransaccionService>();
            var servicioService = scope.Resolve<IServicioService>();
            var atm = scope.Resolve<IATMService>();
            while (true)
            {

                hello("Ingresa tu tarjeta para continuar o 0 para movimientos sin tarjeta");
                long result = 0;
                while (!long.TryParse(Console.ReadLine(), out result) || (result != 0 && result.ToString().Length != 16))
                {
                    hello("Error de tarjeta, ingresa uno valido o 0 para movimientos sin tarjeta");
                }
                var item = tarjetaService.GetTarjetaByNip(result.ToString());
                if (result == 0)
                {
                    hello("Opciones sin tarjeta");
                    Console.WriteLine("1.-Depositar a cuenta");
                    Console.WriteLine("2.-Pagar Servicio");
                    int selected = 0;
                    while (!int.TryParse(Console.ReadLine(), out selected) || selected < 0 || selected > 2)
                    {
                        Console.WriteLine("Ingresa un dato validio");
                    }
                    switch (selected)
                    {
                        case 1:
                            var itemOrigen = DepositarOtraCuenta(tarjetaService);
                            if (itemOrigen != null)
                            {
                                DepositarMiCuenta(item, itemOrigen, atm, tarjetaService, emailService, tipoTransaccionService.GetTipoTransaccionById(1), transactionsService);
                            }
                            break;
                        case 2:
                            PagarServicio(null, atm, tipoTransaccionService.GetTipoTransaccionById(6), transactionsService, emailService, servicioService, tarjetaService);
                            break;
                    }
                    Console.ReadKey();
                }
                else if (item!=null)
                {
                    string nombres = item.Cliente.Nombres + " " + item.Cliente.Apellidos;
                    hello(nombres+": Ingresa tu nip a 4 digitos ");
                    if (validarNip(item.NIP))
                    {
                        Console.Clear();
                        Console.WriteLine("Bienvenido "+ nombres + " a ATM StartBank");
                        Console.WriteLine("Elige una opcion");
                        var option = showTableTipoTransactions(tipoTransaccionService);
                        Console.Clear();
                        Console.WriteLine(option.Nombres);
                        int selected = 0;
                        switch (option.Id)
                        {
                            case 1:
                                Console.WriteLine("Elige una opcion");
                                Console.WriteLine("1.- A mi cuenta");
                                Console.WriteLine("2.- Cuenta de otros");
                                while(!int.TryParse(Console.ReadLine(), out selected) || selected < 1 || selected > 2)
                                {
                                    Console.WriteLine("Ingresa un valor valido");
                                }
                                switch (selected)
                                {
                                    case 1: DepositarMiCuenta(item, item, atm, tarjetaService,emailService, option, transactionsService); break;
                                    case 2:
                                        var itemOrigen = DepositarOtraCuenta(tarjetaService);
                                        if (itemOrigen!= null)
                                        {
                                            DepositarMiCuenta(item, itemOrigen, atm, tarjetaService, emailService, option, transactionsService);
                                        }
                                        break;
                                }
                                break;
                            case 2:
                                RetirarEfectivo(item,atm,tarjetaService,option,transactionsService, emailService);
                                break;
                            case 3:
                                hello(String.Format($"Saldo disponible: {item.Saldo:F2}\nGracias por su visita"));
                                break;
                            case 4:
                                hello("Ultimos 5 movimientos");
                                var last5 = transactionsService.GetAllTransacciones().Where(x => x.Id_Cuenta == item.Id).OrderByDescending(x => x.Fecha_Registro).Take(5).ToList();
                                top5(last5);
                                break;
                            case 5:
                                CambiarNip(item,option,transactionsService,emailService);
                                break;
                            case 6:
                                PagarServicio(item, atm, option, transactionsService, emailService, servicioService,tarjetaService);
                                break;
                        }
                    }
                    Console.ReadKey();
                }
                else
                {
                    
                }
            }
        }
    }
    static void PagarServicio(Tarjeta? item, IATMService atm, TipoTransaccion option, ITransaccionService transactionsService, ICorreoElectronicoService emailService,IServicioService servicioService, ITarjetaService tarjetaService)
    {
        hello("Servicios disponibles");
        var servicios = servicioService.GetAllServicios();
        var table = new ConsoleTable("Folio", "Tipo", "Importe");
        foreach (var i in servicios)
        {
            table.AddRow(i.Id, i.Nombre, i.Tarjeta.Cliente.Nombres);
        }
        table.Write();
        Console.WriteLine("Selecciona el servicio a pagar o ingresa 0 para salir");
        int id = 0;
        while(!int.TryParse(Console.ReadLine(), out id) || (id != 0 && servicios.Where(x=>x.Id==id).FirstOrDefault() == null))
        {
            Console.WriteLine("Selecciona un servicio valido");
        }
        var servicio = servicios.Where(x => x.Id == id).FirstOrDefault();
        if (id != 0)
        {
            Console.WriteLine("Ingresa la referencia");
            string referencia = Console.ReadLine();
            Console.WriteLine("Ingrese el monto a pagar");
            int payment = 0;
            var list = atm.GetAllATMs().Where(x => x.Cantidad > 0).ToList();
            // || !validarDesgaste(list, payment)
            while (!int.TryParse(Console.ReadLine(), out payment) || payment < 0)
            {
                Console.WriteLine("Ingresa un pago valido");

            }
            if (item != null)
            {
                int respuesta = 0;
                Console.WriteLine("1.-Pagar con mi tarjeta\n2.-Pagar con efectivo");
                while (!int.TryParse(Console.ReadLine(), out respuesta) || respuesta < 0 || respuesta > 2)
                {
                    Console.WriteLine("Ingresa una opción valida");
                }
                switch (respuesta)
                {
                    case 1:
                        decimal? saldo = (item.Tipo == 1) ? item.Saldo : item.Limite - item.Saldo;
                        if (saldo < payment)
                        {
                            Console.WriteLine("No cuentas con saldo para aplicar el pago");
                            return;
                        }
                        else
                        {
                            servicio.Tarjeta.Fecha_Actualizado = DateTime.Now;
                            if (servicio.Tarjeta.Tipo == 1)
                            {
                                servicio.Tarjeta.Saldo += payment;
                            }
                            else
                            {
                                servicio.Tarjeta.Saldo -= payment;
                            }
                            tarjetaService.UpdateTarjeta(servicio.Tarjeta);

                            Transaccion transaccion1 = new Transaccion();
                            transaccion1.Descripcion = $"Pago de {payment:F2} a la tarjeta {servicio.Tarjeta.tarjeta} saldo actual {servicio.Tarjeta.Saldo:F2}";
                            transaccion1.Id_Cuenta = servicio.Tarjeta.Id;
                            transaccion1.Id_Tipo = option.Id;
                            transaccion1.Edo_cuenta = item.Id;
                            transaccion1.Referencia = referencia;
                            transaccion1.Importe = payment;
                            transactionsService.AddTransaccion(transaccion1);
                            if (servicio.Tarjeta.Cliente.Email != null)
                            {
                                emailService.EnviarCorreo(servicio.Tarjeta.Cliente.Email, option.Nombres, transaccion1.Descripcion);
                            }
                            item.Fecha_Actualizado = DateTime.Now;
                            if (item.Tipo == 1)
                            {
                                item.Saldo -= payment;
                            }
                            else
                            {
                                item.Saldo += payment;
                            }
                            tarjetaService.UpdateTarjeta(item);

                            Transaccion transaccion2 = new Transaccion();
                            transaccion2.Descripcion = $"Pago de {payment:F2} al servicio {servicio.Nombre} saldo actual {item.Saldo:F2}";
                            transaccion2.Id_Cuenta = item.Id;
                            transaccion2.Id_Tipo = option.Id;
                            transaccion2.Edo_cuenta = item.Id;
                            transaccion2.Referencia = referencia;
                            transaccion2.Importe = payment;
                            transactionsService.AddTransaccion(transaccion2);
                            if (item.Cliente.Email != null)
                            {
                                emailService.EnviarCorreo(item.Cliente.Email, option.Nombres, transaccion2.Descripcion);
                            }
                        }
                        Console.WriteLine("Operacion exitosa\nGracias por su visita");
                        return;
                        break;
                }
            }
            List<ATM> updateList = contarDeposito(list.Where(x=>x.Tipo==1).ToList());
            int importe_firts = updateList.Sum(x => x.Denominacion * x.Cantidad);
            if (importe_firts < payment)
            {
                Console.WriteLine($"Has ingresado: {importe_firts:F2}\nFaltante:{(payment - importe_firts):F2}\n1.-Continuar con {importe_firts:F2}\n2.-Ingresar de nuevo\n3.-Cancelar");
                int selected = 0;
                while (!int.TryParse(Console.ReadLine(), out selected) || selected <= 0 || selected > 3)
                {
                    Console.WriteLine("Ingresa un valor valido");
                }
                switch (selected)
                {
                    case 2:
                        List<ATM> updateList2 = contarDeposito(list);
                        updateList = updateList.Zip(updateList2, (atm1, atm2) =>
                        {
                            if (atm1.Id == atm2.Id)
                            {
                                atm1.Cantidad += atm2.Cantidad;
                            }
                            return atm1;
                        }).ToList();
                        foreach (var i in updateList2)
                        {
                            if (updateList.First(x => x.Id == i.Id) == null)
                            {
                                updateList.Add(i);
                            }
                        }
                        break;
                    case 3: Console.WriteLine("Cancelacion exitosa\nGracias por su visita"); return; break;
                }
            }
            importe_firts = updateList.Sum(x => x.Denominacion * x.Cantidad);
            if (importe_firts < payment)
            {
                Console.WriteLine($"Has ingresado: {importe_firts:F2}\nFaltante:{(payment - importe_firts):F2}\n1.-Continuar con {importe_firts:F2}\n2.-Cancelar");
                int selected = 0;
                while (!int.TryParse(Console.ReadLine(), out selected) || selected <= 0 || selected > 2)
                {
                    Console.WriteLine("Ingresa un valor valido");
                }
                switch (selected)
                {
                    case 2: Console.WriteLine("Cancelacion exitosa, tome su dinero\nGracias por su visita"); return; break;
                }
            }
            if (importe_firts > payment)
            {
                Console.WriteLine("Tome su cambio: ");
                darCambio(importe_firts - payment, atm);
            }
            servicio.Tarjeta.Fecha_Actualizado = DateTime.Now;
            if (servicio.Tarjeta.Tipo == 1)
            {
                servicio.Tarjeta.Saldo += payment;
            }
            else
            {
                servicio.Tarjeta.Saldo -= payment;
            }
            tarjetaService.UpdateTarjeta(servicio.Tarjeta);

            Transaccion transaccion = new Transaccion();
            transaccion.Descripcion = $"Deposito de {payment:F2} a la tarjeta {servicio.Tarjeta.tarjeta} saldo actual {servicio.Tarjeta.Saldo:F2}";
            transaccion.Id_Cuenta = servicio.Tarjeta.Id;
            transaccion.Id_Tipo = option.Id;
            transaccion.Edo_cuenta = item?.Id;
            transaccion.Referencia = referencia;
            transaccion.Importe = payment;
            transactionsService.AddTransaccion(transaccion);
            if (servicio.Tarjeta.Cliente.Email != null)
            {
                emailService.EnviarCorreo(servicio.Tarjeta.Cliente.Email, option.Nombres, transaccion.Descripcion);
            }

            Console.WriteLine("Operación Finalizada\nGracias por su visita");
        }
        else
        {
            Console.WriteLine("Operación Cancelada\nGracias por su visita");
        }
    }
    static void CambiarNip(Tarjeta item, TipoTransaccion option, ITransaccionService transactionsService, ICorreoElectronicoService emailService)
    {
        Console.WriteLine("Ingresa los 4 digitos de tu nuevo nip o 0 para cancelar");
        bool continuar = true;
        string nip = "";
        while (continuar)
        {
            int help = 0;
            nip = Console.ReadLine();
            if (nip == "0")
            {
                continuar = false;
            }
            else if (nip.Length != 4)
            {
                Console.WriteLine("El nip debe ser de 4 digitos");
            }
            else if (!int.TryParse(nip, out help))
            {
                Console.WriteLine("El nip debe ser numerico");
            }
            else
            {
                continuar = false;
            }
        }
        if (nip != "0")
        {
            item.NIP = nip;
            item.Fecha_Actualizado = DateTime.Now;
            Transaccion transaccion = new Transaccion();
            transaccion.Descripcion = "Cambio de nip";
            transaccion.Id_Cuenta = item.Id;
            transaccion.Id_Tipo = option.Id;
            transaccion.Edo_cuenta = item.Id;
            transaccion.Referencia = "";
            transaccion.Importe = 0;
            transactionsService.AddTransaccion(transaccion);
            if (item.Cliente.Email != null)
            {
                emailService.EnviarCorreo(item.Cliente.Email, option.Nombres, transaccion.Descripcion);
            }
            Console.WriteLine("Operación Finalizada\nGracias por su visita");
        }
    }

    static void top5(List<Transaccion> list)
    {
        bool continuar = true;
        while (continuar)
        {
            var table = new ConsoleTable("Folio", "Tipo", "Importe", "Fecha");
            foreach (var i in list)
            {
                table.AddRow(i.Id, i.TipoTransaccion.Nombres, i.Importe, i.Fecha_Registro);
            }
            table.Write();
            Console.WriteLine("Elige una opción");
            Console.WriteLine("1.- Ordenar por importe");
            Console.WriteLine("2.- Ordenar por fecha");
            Console.WriteLine("0.- Salir");
            int selected = 0;
            while(!int.TryParse(Console.ReadLine(), out selected) || selected < 0 || selected > 2)
            {
                Console.WriteLine("Ingresa una opcion valida");
            }
            switch (selected)
            {
                case 1:
                    /*Quicksorter<Transaccion> sorter = new Quicksorter<Transaccion>();
                    sorter.Sort(list);*/
                    break;
                case 2: break;
                case 0: continuar = false; break;
            }
        }
        

    }

    private static void RetirarEfectivo(Tarjeta item, IATMService atm, ITarjetaService tarjetaService, TipoTransaccion option, ITransaccionService transactionsService, ICorreoElectronicoService emailService)
    {
        var list = atm.GetAllATMs().Where(x => x.Tipo == 1&& x.Cantidad > 0).OrderByDescending(x => x.Denominacion).ToList();
        var nodisponible = atm.GetAllATMs().Where(x => x.Cantidad == 0).ToList();
        var aldia = transactionsService.GetAllTransacciones().Where(x => x.Id_Cuenta == item.Id && x.Id_Tipo == 1 && x.TipoTransaccion.Edo_Cuenta == 1 && x.Fecha_Registro.Date == DateTime.Now.Date).ToList();
        if (list.Count == 0)
        {
            hello("Cajero no tiene dinero");
        }
        else if (item.Saldo<50)
        {
            hello("No tienes saldo disponible para retirar");
        }
        else if(aldia.Count==5)
        {
            hello("Has alcanzado tu limite de transacciones diarias");
        }
        else if (aldia.Sum(x=>x.Importe) >= 9000)
        {
            hello("Has alcanzado tu limite de retiro diario");
        }
        else
        {
            if (nodisponible.Count > 0)
            {
                hello("El cajero no cuenta con las denominaciones siguientes");
                var table = new ConsoleTable("Importe");
                foreach (var i in nodisponible)
                {
                    table.AddRow(i.Denominacion);
                }
                table.Write();
                Console.WriteLine("Preciona una tecla para continuar...");
                Console.ReadKey();
            }
            hello(string.Format($"Saldo disponible: {item.Saldo:F2}"));
            Console.WriteLine("Ingresa el monto a retirar");
            int payment = 0;
            while (!int.TryParse(Console.ReadLine(), out payment) || !validarDesgaste(list, payment))
            {
                Console.WriteLine("Ingresa un valor valido");
            }
            darCambio(payment, atm);
            item.Fecha_Actualizado = DateTime.Now;
            if (item.Tipo == 1)
            {
                item.Saldo -= payment;
            }
            else
            {
                item.Saldo += payment;
            }
            tarjetaService.UpdateTarjeta(item);

            Transaccion transaccion = new Transaccion();
            transaccion.Descripcion = $"Retiro de {payment:F2} a la tarjeta {item.tarjeta} saldo actual {item.Saldo:F2}";
            transaccion.Id_Cuenta = item.Id;
            transaccion.Id_Tipo = option.Id;
            transaccion.Edo_cuenta = item.Id;
            transaccion.Referencia = "";
            transaccion.Importe = payment;
            transactionsService.AddTransaccion(transaccion);
            if (item.Cliente.Email != null)
            {
                emailService.EnviarCorreo(item.Cliente.Email, option.Nombres, transaccion.Descripcion);
            }

            Console.WriteLine("Operación Finalizada\nGracias por su visita");
        }
        
    }

    static Tarjeta? DepositarOtraCuenta(ITarjetaService itarjeta)
    {
        int contador = 0;
        hello("Ingresa tu tarjeta para continuar o 0 para cancelar");
        long result = 0;
        while (!long.TryParse(Console.ReadLine(), out result) || (result != 0 && result.ToString().Length != 16) || itarjeta.GetTarjetaByNip(result.ToString())==null)
        {
            contador++;
            if (contador == 3)
            {
                hello("Limite de intentos superado, operación cancelada.");
                return null;
            }
            hello("Error de tarjeta, ingresa uno valido o 0 para movimientos sin tarjeta");
        }
        var item = itarjeta.GetTarjetaByNip(result.ToString());
        if (result == 0)
        {
            hello("Operación cancelada");
            Console.ReadKey();
            return null;
        }else
        {
            return item;
        }
    }
    static void DepositarMiCuenta(Tarjeta origen, Tarjeta tarjeta, IATMService atm, ITarjetaService itarjeta, ICorreoElectronicoService emailService, TipoTransaccion option, ITransaccionService transactionsService)
    {
        hello("Billetes permitidos, Ingrese el monto a depositar");
        var list = atm.GetAllATMs().Where(x=>x.Tipo==1).OrderByDescending(x => x.Denominacion).ToList();
        var table = new ConsoleTable("Importe");
        foreach (var item in list)
        {
            table.AddRow(item.Denominacion);
        }
        table.Write();
        int payment = 0;
        while(!int.TryParse(Console.ReadLine(), out payment) || !validarDesgaste(list,payment))
        {
            Console.WriteLine("Ingresa un valor valido");
        }
        if (origen != null && tarjeta?.Id!=origen?.Id)
        {
            int respuesta = 0;
            Console.WriteLine("1.-Depositar con mi tarjeta\n2.-Depositar con efectivo");
            while (!int.TryParse(Console.ReadLine(), out respuesta) || respuesta < 0 || respuesta > 2)
            {
                Console.WriteLine("Ingresa una opción valida");
            }
            switch (respuesta)
            {
                case 1:
                    decimal? saldo = (origen.Tipo == 1) ? origen.Saldo : origen.Limite - origen.Saldo;
                    if (saldo < payment)
                    {
                        Console.WriteLine("No cuentas con saldo para aplicar el pago");
                        return;
                    }
                    else
                    {
                        origen.Fecha_Actualizado = DateTime.Now;
                        if (tarjeta.Tipo == 1)
                        {
                            tarjeta.Saldo += payment;
                        }
                        else
                        {
                            tarjeta.Saldo -= payment;
                        }
                        itarjeta.UpdateTarjeta(tarjeta);

                        Transaccion transaccion1 = new Transaccion();
                        transaccion1.Descripcion = $"Pago de {payment:F2} a la tarjeta {tarjeta.tarjeta} saldo actual {tarjeta.Saldo:F2}";
                        transaccion1.Id_Cuenta = tarjeta.Id;
                        transaccion1.Id_Tipo = option.Id;
                        transaccion1.Edo_cuenta = origen.Id;
                        transaccion1.Referencia = "";
                        transaccion1.Importe = payment;
                        transactionsService.AddTransaccion(transaccion1);
                        if (tarjeta.Cliente.Email != null)
                        {
                            emailService.EnviarCorreo(tarjeta.Cliente.Email, option.Nombres, transaccion1.Descripcion);
                        }
                        origen.Fecha_Actualizado = DateTime.Now;
                        if (origen.Tipo == 1)
                        {
                            origen.Saldo -= payment;
                        }
                        else
                        {
                            origen.Saldo += payment;
                        }
                        itarjeta.UpdateTarjeta(origen);

                        Transaccion transaccion2 = new Transaccion();
                        transaccion2.Descripcion = $"Pago de {payment:F2} a la tarjeta {tarjeta.tarjeta}";
                        transaccion2.Id_Cuenta = origen.Id;
                        transaccion2.Id_Tipo = option.Id;
                        transaccion2.Edo_cuenta = origen.Id;
                        transaccion2.Referencia = "";
                        transaccion2.Importe = payment;
                        transactionsService.AddTransaccion(transaccion2);
                        if (origen.Cliente.Email != null)
                        {
                            emailService.EnviarCorreo(origen.Cliente.Email, option.Nombres, transaccion2.Descripcion);
                        }
                    }
                    Console.WriteLine("Operacion exitosa\nGracias por su visita");
                    return;
                    break;
            }
        }
        List<ATM> updateList = contarDeposito(list);
        int importe_firts = updateList.Sum(x => x.Denominacion * x.Cantidad);
        if (importe_firts < payment) {
            Console.WriteLine($"Has ingresado: {importe_firts:F2}\nFaltante:{(payment-importe_firts):F2}\n1.-Continuar con {importe_firts:F2}\n2.-Ingresar de nuevo\n3.-Cancelar");
            int selected = 0;
            while(!int.TryParse(Console.ReadLine(), out selected) || selected <= 0 || selected > 3)
            {
                Console.WriteLine("Ingresa un valor valido");
            }
            switch(selected) {
                case 2:
                    List<ATM> updateList2 = contarDeposito(list);
                    updateList = updateList.Zip(updateList2, (atm1, atm2) =>
                    {
                        if (atm1.Id == atm2.Id)
                        {
                            atm1.Cantidad += atm2.Cantidad;
                        }
                        return atm1;
                    }).ToList();
                    foreach (var i in updateList2)
                    {
                        if (updateList.First(x => x.Id == i.Id)==null)
                        {
                            updateList.Add(i);
                        }
                    }
                    break;
                case 3: Console.WriteLine("Cancelacion exitosa, tome su dinero\nGracias por su visita"); return; break;
            }
        }

        importe_firts = updateList.Sum(x => x.Denominacion * x.Cantidad);
        if(importe_firts > payment)
        {
            Console.WriteLine("Tome su cambio: ");
            darCambio(importe_firts - payment, atm);
        }
        list = list.Zip(updateList, (atm1, atm2) =>
        {
            if (atm1.Id == atm2.Id)
            {
                atm1.Cantidad += atm2.Cantidad;
            }
            return atm1;
        }).ToList();
        foreach (var item in list)
        {
            item.Fecha_Actualizado = DateTime.Now;
            atm.UpdateATM(item);
        }
        Transaccion transaccion = new Transaccion();
        tarjeta.Fecha_Actualizado = DateTime.Now;
        if (tarjeta.Tipo == 1)
        {
            tarjeta.Saldo += payment;
            transaccion.Descripcion = $"Deposito de {payment:F2} a la tarjeta {tarjeta.tarjeta} saldo actual {tarjeta.Saldo:F2}";
        }
        else
        {
            tarjeta.Saldo -= payment;
            transaccion.Descripcion = $"Pago de {payment:F2} a la tarjeta {tarjeta.tarjeta} saldo actual {tarjeta.Saldo:F2} Credito disponible {tarjeta.Limite-tarjeta.Saldo:F2}";
        }
        itarjeta.UpdateTarjeta(tarjeta);

        transaccion.Id_Cuenta = tarjeta.Id;
        transaccion.Id_Tipo = option.Id;
        transaccion.Edo_cuenta = origen?.Id;
        transaccion.Referencia = "";
        transaccion.Importe = payment;
        transactionsService.AddTransaccion(transaccion);
        if (tarjeta.Cliente.Email != null)
        {
            emailService.EnviarCorreo(tarjeta.Cliente.Email, option.Nombres, transaccion.Descripcion);
        }
        if (tarjeta?.Id == origen?.Id)
        {
            if (tarjeta.Tipo == 1)
            {
                Console.WriteLine($"Saldo disponible {tarjeta.Saldo:F2}");
            }
            else
            {
                Console.WriteLine($"Crédito disponible {tarjeta.Limite - tarjeta.Saldo:F2}");
            }
        }

        Console.WriteLine("Operación Finalizada\nGracias por su visita");
    }
    static void darCambio(int cambio, IATMService atm)
    {
        var listado = atm.GetAllATMs().OrderByDescending(x=>x.Denominacion);
        var table = new ConsoleTable("Billete", "Cantidad");
        foreach (var item in listado)
        {
            var cantidad = (int)(cambio / item.Denominacion);
            cambio %= item.Denominacion;
            if (cantidad > 0)
            {

                item.Cantidad -= cantidad;
                item.Fecha_Actualizado = DateTime.Now;
                atm.UpdateATM(item);
                table.AddRow(item.Denominacion, cantidad);
            }
        }
        table.Write();
    }
    static List<ATM> contarDeposito(List<ATM> list)
    {
        List<ATM> updateList = new List<ATM>();
        foreach (var item in list)
        {
            Console.WriteLine($"Ingresa Cuantos billetes de: {item.Denominacion:F2}");
            int cantidad = 0;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad < 0)
            {
                Console.WriteLine("Ingresa una cantidad valida");
            }
            updateList.Add(new ATM() { Cantidad = cantidad, Id = item.Id, Denominacion=item.Denominacion });
        }
        return updateList;
    }
    static bool validarDesgaste(List<ATM> atm, int importe)
    {
        double residual = 0.00;
        foreach (var item in atm)
        {
            importe %= item.Denominacion;
        }
        return (importe == 0);
    }
    static TipoTransaccion showTableTipoTransactions(ITipoTransaccionService tipoTransaccionService)
    {
        var transactions = tipoTransaccionService.GetAllTipoTransacciones();
        var table = new ConsoleTable("ID", "Nombre");
        foreach (var item in transactions)
        {
            table.AddRow(item.Id,item.Nombres);
        }
        table.Write();
        int selected = 0;
        while(!int.TryParse(Console.ReadLine(), out selected) || transactions.Where(x=> x.Id==selected).FirstOrDefault() == null)
        {
            Console.WriteLine("Ingresa una opción valida");
        }
        return transactions.Where(x => x.Id == selected).FirstOrDefault();
    }
    static void hello(string mensaje)
    {
        Console.Clear();
        Console.WriteLine("Bienvenido a ATM StartBank");
        Console.WriteLine(mensaje);
    }

    static bool validarNip(string nip)
    {
        int contador = 0;

        string newnip = Console.ReadLine();
        int limite = 4;
        while (contador< limite)
        {
            contador++;
            if (contador == limite)
            {
                return false;
            }
            else
            {
                if(newnip.Length != 4)
                {
                    Console.WriteLine("Te quedan {0} intentos", limite - contador);
                    Console.WriteLine("El nip debe ser de 4 digitos");
                }
                else if(newnip != nip)
                {
                    Console.WriteLine("Te quedan {0} intentos", limite - contador);
                    Console.WriteLine("Nip incorrecto");
                }
                else
                {
                    return true;
                }
            }

            newnip = Console.ReadLine();
        }
        return true;
    }
}