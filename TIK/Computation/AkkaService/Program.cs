﻿using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using CommandLine;
using Microsoft.AspNetCore.Hosting;

namespace TIK.Computation.AkkaService
{
    public class Program
    {

        public static void Main(string[] args) 
        {
            
            var host = new WebHostBuilder()
                      .UseKestrel(options =>
                      {
                          options.Listen(IPAddress.Loopback, 5301);
                      })
                      .UseContentRoot(Directory.GetCurrentDirectory())
                      .UseStartup<Startup>()
                      .Build();

             host.RunAsync();

         
        }

    }

}