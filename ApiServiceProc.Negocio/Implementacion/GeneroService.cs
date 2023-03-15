﻿using ApiServiceProc.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServiceProc.Entidades;
using System.Data;
using System.Data.Common;
using Microsoft.Win32.SafeHandles;

namespace ApiServiceProc.Negocio.Implementacion
{
    public class GeneroService : IGeneroService
    {
        private readonly IDbConnection _connection;

        public GeneroService(IDbConnection dbConnection)
        {
            this._connection = dbConnection;
        }

        public async Task<List<Genero>> Obtener()
        {
            string consulta = "Select * From Genero";
            List<Genero> lista = new List<Genero>();

            using (var conn = _connection)
            {
               conn.Open();
                using var cmd = conn.CreateCommand();
                cmd.CommandText = consulta;

                using var reader = await ((DbCommand)cmd).ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    Genero genero = new Genero()
                    {
                        Id = reader.GetInt32(0),
                        Descripcion = reader.GetString(1)
                    };

                    lista.Add(genero);
                }
            }

            return lista;
        }

        public async Task<List<Genero>> Guardar(Genero genero)
        {
            string consulta = "Insert into Genero (descripcion) values (@descripcion)";

            using (var cn = _connection)
            {
                cn.Open();

                using var comando = cn.CreateCommand();
                comando.CommandText = consulta;
                var descripcionParametro = comando.CreateParameter();

                descripcionParametro.ParameterName = "@descripcion";
                descripcionParametro.Value = genero.Descripcion;
                comando.Parameters.Add(descripcionParametro);

                comando.ExecuteNonQuery(); 
            }
            return await Obtener();

        }

        public Task<List<Genero>> Actualizar(Genero genero)
        {
            throw new NotImplementedException();
        }

        public Task<List<Genero>> Borrar(int id)
        {
            throw new NotImplementedException();
        }
    }
}