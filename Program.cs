using System;
using System.Collections.Generic;

namespace CompositePC
{
    // 
    // Componente /Clase Abstracta
    public abstract class Componente
    {
        protected string _nombre;

        public Componente(string nombre)
        {
            _nombre = nombre;
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        //
        // Metodos abstractos obligatorios del patron
        public abstract void AgregarHijo(Componente c);
        public abstract IList<Componente> ObtenerHijos();
        
        //
        // propiedad para obtener el precio 
        public abstract double ObtenerPrecio { get; }

        // metodo para imprimir la descripción de forma recursiva
        public abstract void MostrarDescripcion();
    }

    // 
    // hoja
    public class Pieza : Componente
    {
        private double _precio;

        public Pieza(string nombre, double precio) : base(nombre)
        {
            _precio = precio;
        }

        public override void AgregarHijo(Componente c)
        {
            // no hace nada / las hojas no pueden tener hijos
        }

        public override IList<Componente> ObtenerHijos()
        {
            return null;
        }

        public override double ObtenerPrecio
        {
            get { return _precio; }
        }

        public override void MostrarDescripcion()
        {
            Console.WriteLine($"    - {Nombre} : ${ObtenerPrecio}");
        }
    }

    //
    // Composite / compu
    public class ComputadoraArmada : Componente
    {
        private List<Componente> _hijos;

        public ComputadoraArmada(string nombre) : base(nombre)
        {
            _hijos = new List<Componente>();
        }

        public override void AgregarHijo(Componente c)
        {
            _hijos.Add(c);
        }

        public override IList<Componente> ObtenerHijos()
        {
            return _hijos.AsReadOnly();
        }

        // recorre y suma los precios de los hijos
        public override double ObtenerPrecio
        {
            get
            {
                double total = 0;
                foreach (var item in _hijos)
                {
                    total += item.ObtenerPrecio;
                }
                return total;
            }
        }

        // imprime el paquete y luego hace foreach para imprimir sus piezas
        public override void MostrarDescripcion()
        {
            Console.WriteLine($"\n    [ {Nombre} ]");
            Console.WriteLine($"    Descripción del producto :");
            
            foreach (var item in _hijos)
            {
                item.MostrarDescripcion();
            }
        }
    }

    // 
    //main
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bienvenido Al Armado De Computadoras ===");
            Console.WriteLine("Seleccione paquete:");
            Console.WriteLine("1. PC de Lujo");
            Console.WriteLine("2. PC Regular");
            Console.WriteLine("3. PC Economico");
            Console.Write("Opción: ");
            string opcMenu = Console.ReadLine();

            Componente miPC;

            if (opcMenu == "1")
            {
                miPC = new ComputadoraArmada("PC de Lujo");
                ArmarPCLujo(miPC);
            }
            else if (opcMenu == "2")
            {
                miPC = new ComputadoraArmada("PC Regular");
                ArmarPCRegular(miPC);
            }
            else if (opcMenu == "3")
            {
                miPC = new ComputadoraArmada("PC Economico");
                ArmarPCEconomico(miPC);
            }
            else
            {
                Console.WriteLine("Opción no valida...");
                return;
            }

            // 
            // resulatdo de arbol
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       Ticket De Compra Del Equipo      ");
            Console.WriteLine("========================================");
            
            // descripcion de comosite
            miPC.MostrarDescripcion();
            
            Console.WriteLine("\n========================================");
            Console.WriteLine($"Precio Total Del Equipo: ${miPC.ObtenerPrecio} dolares / aprox. ${miPC.ObtenerPrecio * 17.0} MXN");

            Console.ReadLine();
        }

        // 
        //menus para las piezas
        static void ElegirOpcion(Componente pc, string categoria, Pieza opcion1, Pieza opcion2)
        {
            Console.WriteLine($"\n--- Seleccione {categoria} ---");
            Console.WriteLine($"1. {opcion1.Nombre} (${opcion1.ObtenerPrecio})");
            Console.WriteLine($"2. {opcion2.Nombre} (${opcion2.ObtenerPrecio})");
            Console.Write("Elija una opción (1 o 2): ");
            
            string opc = Console.ReadLine();
            if (opc == "1") {
                pc.AgregarHijo(opcion1);
            }
            if (opc == "2") {
                pc.AgregarHijo(opcion2);
            }
            else 
            {
                Console.WriteLine("Opción no valida...");
                return;
            }
        }

        static void ArmarPCLujo(Componente pc)
        {
            ElegirOpcion(pc, "CPU", new Pieza("Intel Core i9-14900K", 650), new Pieza("Intel Core i7-14700K", 450));
            ElegirOpcion(pc, "RAM", new Pieza("64GB DDR5 Corsair Dominator", 250), new Pieza("32GB DDR5 G.Skill Trident", 150));
            ElegirOpcion(pc, "GPU", new Pieza("NVIDIA RTX 4090 24GB", 1800), new Pieza("AMD Radeon RX 7900 XTX 24GB", 1000));
            ElegirOpcion(pc, "SSD", new Pieza("4TB NVMe Gen4 Samsung 990 Pro", 350), new Pieza("2TB NVMe Gen4 WD Black", 180));
            ElegirOpcion(pc, "Motherboard", new Pieza("ASUS ROG Maximus Z790", 600), new Pieza("Gigabyte Z790 AORUS Elite", 300));
            ElegirOpcion(pc, "Case", new Pieza("Lian Li O11 Dynamic EVO", 180), new Pieza("Corsair 5000D Airflow", 160));
            ElegirOpcion(pc, "Monitor", new Pieza("OLED 4K 144Hz ASUS ROG", 1000), new Pieza("Ultrawide 1440p Alienware", 900));
            ElegirOpcion(pc, "Teclado", new Pieza("Logitech G915 TKL Custom", 200), new Pieza("Razer Huntsman V2", 180));
            ElegirOpcion(pc, "Ratón", new Pieza("Logitech G Pro X Superlight", 150), new Pieza("Razer DeathAdder V3 Pro", 140));
        }

        static void ArmarPCRegular(Componente pc)
        {
            ElegirOpcion(pc, "CPU", new Pieza("Intel Core i5-13600K", 300), new Pieza("Intel Core i5-13400F", 220));
            ElegirOpcion(pc, "RAM", new Pieza("16GB DDR5 Kingston Fury", 80), new Pieza("8GB DDR5 Crucial", 45));
            ElegirOpcion(pc, "GPU", new Pieza("NVIDIA RTX 4070 12GB", 600), new Pieza("AMD Radeon RX 7800 XT 16GB", 500));
            ElegirOpcion(pc, "SSD", new Pieza("1TB NVMe Gen4 Kingston", 70), new Pieza("500GB NVMe Gen4 WD Blue", 45));
            ElegirOpcion(pc, "Motherboard", new Pieza("MSI B760 Tomahawk", 180), new Pieza("ASUS TUF Gaming B760-PLUS", 160));
            ElegirOpcion(pc, "Case", new Pieza("NZXT H510 Flow", 90), new Pieza("Phanteks Eclipse P400A", 95));
            ElegirOpcion(pc, "Monitor", new Pieza("Monitor IPS 1440p 144Hz", 300), new Pieza("Monitor 1080p 240Hz", 250));
            ElegirOpcion(pc, "Teclado", new Pieza("HyperX Alloy Origins", 100), new Pieza("Keychron C1 Mecánico", 80));
            ElegirOpcion(pc, "Ratón", new Pieza("Logitech G305 Inalámbrico", 50), new Pieza("Razer Viper Mini", 40));
        }

        static void ArmarPCEconomico(Componente pc)
        {
            ElegirOpcion(pc, "CPU", new Pieza("Intel Core i5-12400F", 140), new Pieza("Intel Core i3-12100F", 100));
            ElegirOpcion(pc, "RAM", new Pieza("16GB DDR4 Corsair Vengeance", 45), new Pieza("8GB DDR4 Patriot Viper", 25));
            ElegirOpcion(pc, "GPU", new Pieza("NVIDIA GTX 1650 4GB", 150), new Pieza("AMD Radeon RX 6600 8GB", 200));
            ElegirOpcion(pc, "SSD", new Pieza("500GB SSD SATA Kingston", 35), new Pieza("256GB NVMe Adata", 25));
            ElegirOpcion(pc, "Motherboard", new Pieza("Gigabyte B660M DS3H", 100), new Pieza("ASUS Prime H610M", 80));
            ElegirOpcion(pc, "Case", new Pieza("Thermaltake V200", 50), new Pieza("Aerocool Cylon", 45));
            ElegirOpcion(pc, "Monitor", new Pieza("Monitor 1080p 75Hz", 100), new Pieza("Monitor 1080p 60Hz", 80));
            ElegirOpcion(pc, "Teclado", new Pieza("Redragon Kumara Mecánico", 35), new Pieza("Teclado Membrana Logitech", 15));
            ElegirOpcion(pc, "Ratón", new Pieza("Redragon Griffin", 20), new Pieza("Logitech M190", 15));
        }
    }
}

//unGAB0 MOD
