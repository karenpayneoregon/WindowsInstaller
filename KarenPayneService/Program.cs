﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using KarenPayneService.Classes;

namespace KarenPayneService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // ReSharper disable once InconsistentNaming
            // ReSharper disable once JoinDeclarationAndInitializer
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DatabaseService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
