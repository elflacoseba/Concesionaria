using Concesionaria.API.Controllers;
using Concesionaria.Application.DTOs;
using Concesionaria.Application.Interfaces;
using Concesionaria.Application.Services;
using Concesionaria.Domain.Entities;
using Concesionaria.Infrastructure.Repositories;
using ConcesionariaAPITests.Utilidades;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ConcesionariaAPITests;

[TestClass]
public class ConsultasContactoPruebas : BasePruebas
{
    [TestMethod]
    public async Task Get_Retorna404_CuandoConsutaContactoConIdNoExiste()
    {
        //Preparación
        var nombreBD = new Guid().ToString();
        var context = ConstruirContext(nombreBD);
        //var mapper = ConfigurarMapper();
        IGenericRepository<ConsultaContacto> repository = new GenericRepository<ConsultaContacto>(context);
        IValidator<ConsultaContactoCreacionDTO> validatorConsultaCreacionDTO = null;
        IValidator<ConsultaContactoActualizacionDTO> validatorConsultaActualizacionDTO = null;

        var consultaContactoService = new ConsultaContactoService(repository, validatorConsultaCreacionDTO, validatorConsultaActualizacionDTO);
        var controller = new ConsultasContactoController(consultaContactoService);

        //Prueba
        var respuesta = await controller.Get(1);

        //Verificación
        var resultado = respuesta.Result as StatusCodeResult;
        
        Assert.AreEqual(expected: 404, actual: resultado!.StatusCode, "Se esperaba un estado 404 Not Found cuando la consulta de contacto no existe.");
    }

    [TestMethod]
    public async Task Get_RetornaConsultaContacto_CuandoConsutaContactoConIdExiste()
    {
        //Preparación

        var nombreBD = new Guid().ToString();
        var context = ConstruirContext(nombreBD);
      //  var mapper = ConfigurarMapper();
        var context2 = ConstruirContext(nombreBD);
        IGenericRepository<ConsultaContacto> repository = new GenericRepository<ConsultaContacto>(context2);
        IValidator<ConsultaContactoCreacionDTO> validatorConsultaCreacionDTO = null;
        IValidator<ConsultaContactoActualizacionDTO> validatorConsultaActualizacionDTO = null;
        var consultaContactoService = new ConsultaContactoService(repository, validatorConsultaCreacionDTO, validatorConsultaActualizacionDTO);

        context.ConsultasContacto.Add(new ConsultaContacto
        {
            Nombre = "Nombre1",
            Email = "email@server.com",
            Telefono = "123456789",
            Mensaje = "Mensaje de prueba"
        });

        context.ConsultasContacto.Add(new ConsultaContacto
        {
            Nombre = "Nombre2",
            Email = "email@server.com",
            Telefono = "123456789",
            Mensaje = "Mensaje de prueba"
        });

        context.ConsultasContacto.Add(new ConsultaContacto
        {
            Nombre = "Nombre3",
            Email = "email@server.com",
            Telefono = "123456789",
            Mensaje = "Mensaje de prueba"
        });

        await context.SaveChangesAsync();   

        var controller = new ConsultasContactoController(consultaContactoService);

        //Prueba
        var respuesta = await controller.Get(1);

        //Verificación
        var resultado = respuesta.Value;

        Assert.AreEqual(expected: 1, actual: resultado!.Id,"Se espera obtener una consulta contacto con el Id = 1.");
    }
}
