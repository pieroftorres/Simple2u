﻿using Microsoft.Extensions.Configuration;
using Simple2u.Enums;
using System;

namespace Simple2u.Config
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            string diretorioJson = string.Format(@$"{AppDomain.CurrentDomain.BaseDirectory}..\\..\\..\\AppSettings");
            _config = new ConfigurationBuilder()
                .AddJsonFile($"{diretorioJson}/appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{diretorioJson}/appsettings.local.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public string Url(string tipo) => _config.GetSection($"Url:{tipo}").Value;
        public Browser Browser => Enum.Parse<Browser>(_config.GetSection("Browser").Value);
        public string NomeSquad => _config.GetSection("ConfigBrowserStack:NomeSquad").Value;
        public string NomeProjeto => _config.GetSection("ConfigBrowserStack:NomeProjeto").Value;
        public string UserName => _config.GetSection("ConfigBrowserStack:UserName").Value;
        public string AccessKey => _config.GetSection("ConfigBrowserStack:AccessKey").Value;
        public string OS => _config.GetSection("ConfigBrowserStack:OS").Value;
        public string OSVersion => _config.GetSection("ConfigBrowserStack:OSVersion").Value;
        public string ClientId => _config.GetSection("ConfigHttpRequest:ClientId").Value;
        public string ClientSecret => _config.GetSection("ConfigHttpRequest:ClientSecret").Value;
        public string Scopes => _config.GetSection("ConfigHttpRequest:Scopes").Value;
        public string HostUrl => _config.GetSection("ConfigHttpRequest:HostUrl").Value;
        public string UsuarioEmail => _config.GetSection("ConfigEmail:Usuario").Value;
        public string SenhaEmail => _config.GetSection("ConfigEmail:Senha").Value;
        public string UsuarioEsim => _config.GetSection("ConfigEsim:Usuario").Value;
        public string SenhaEsim => _config.GetSection("ConfigEsim:Senha").Value;
    }
}