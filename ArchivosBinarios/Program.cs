using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ArchivosBinarios
{
    class Program
    {
        class ArchivoBinarioEmpleado
        {
            //Delcaración de flujos
            BinaryWriter bw = null;  //Flujo de salida-escritura de datos
            BinaryReader br = null;  //Flujo entrada-lectura de datos

            //Campos de la clase
            string nombre, direccion;
            long telefono;
            int numEmp, diasTrabajados;
            float salarioDiario;

            public void CrearArchivo(string Archivo)
            {
                //Variable locasl método
                char resp;

                try
                {
                    //Creación del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                    //Captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Número del empleado: ");
                        numEmp = int.Parse(Console.ReadLine());
                        Console.Write("Nombre del empleado: ");
                        nombre = Console.ReadLine();
                        Console.Write("Dirección del empleado: ");
                        direccion = Console.ReadLine();
                        Console.Write("Teléfono del empleado: ");
                        telefono = int.Parse(Console.ReadLine());
                        Console.Write("Días trabajados del empleado: ");
                        diasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario diario del empleado: ");
                        salarioDiario = Single.Parse(Console.ReadLine());

                        //Escribe los datos del archivo
                        bw.Write(numEmp);
                        bw.Write(nombre);
                        bw.Write(direccion);
                        bw.Write(telefono);
                        bw.Write(diasTrabajados);
                        bw.Write(salarioDiario);

                        Console.Write("\n\nDeseas almacenar otro registro (s/n)?");
                        resp = Char.Parse(Console.ReadLine());
                    } while ((resp == 's') || (resp == 'S'));
                }
                catch(IOException x)
                {
                    Console.WriteLine("\nError : " + x.Message);
                    Console.WriteLine("\nRuta : " + x.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();  //Cierra el flujo-escritura
                    Console.Write("\nPresione ENTER para terminar la escritura de datos y regresar al menú");
                    Console.ReadKey();
                }
            }

            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //Verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        //Creación del flujo para leer datos del archivo
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //Despliegue de datos en pantalla
                        Console.Clear();

                        do
                        {
                            //Lectura de registros mientras no llegue a EndOFile
                            numEmp = br.ReadInt32();
                            nombre = br.ReadString();
                            direccion = br.ReadString();
                            telefono = br.ReadInt64();
                            diasTrabajados = br.ReadInt32();
                            salarioDiario = br.ReadSingle();

                            //Muestra de los datos
                            Console.WriteLine("Número del empleado: " + numEmp);
                            Console.WriteLine("Nombre del empleado: " + nombre);
                            Console.WriteLine("Dirección del empleado: " + direccion);
                            Console.WriteLine("Teléfono del empleado: " + telefono);
                            Console.WriteLine("Días trabajados del empleado: " + diasTrabajados);
                            Console.WriteLine("Salario diario del empleado: {0:C} ", salarioDiario);
                            Console.WriteLine("Sueldo total del empleado: {0:C} ", (diasTrabajados * salarioDiario));
                            Console.WriteLine("\n");

                        } while (true);
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl archivo " + Archivo + " no existe en el disco!!");
                        Console.Write("\nPresione ENTER para continuar...");
                        Console.ReadKey();
                    }
                }
                catch(EndOfStreamException)
                {
                    Console.WriteLine("\n\nFin del listado de empleados");
                    Console.Write("\nPresione ENTER para continuar...");
                    Console.ReadKey();
                }
                finally
                {
                    if (br != null) br.Close();  //Cierra el flujo
                    Console.Write("\nPresione ENTER para terminar la lectura de datos y regresar al menú");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //Declaración de variables auxiliares 
            string Arch = null;
            int opcion;

            //Creación del objeto
            ArchivoBinarioEmpleado A1 = new ArchivoBinarioEmpleado();

            //Menú de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n***ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un archivo");
                Console.WriteLine("2.- Lectura de un archivo");
                Console.WriteLine("3.- Salida del programa");
                Console.Write("\nQué opción deseas: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //Capturar el nombre del archivo
                            Console.Write("\nAlimenta el nombre del archivo a Crear: "); Arch = Console.ReadLine();

                            //Verifica si existe el archivo 
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl archivo existe!!, deseas sobreescribirlo {s/n} ? ");
                                resp = Char.Parse(Console.ReadLine());
                            }

                            if ((resp == 's') || (resp == 'S'))
                            {
                                A1.CrearArchivo(Arch);
                            }
                        }
                        catch (Exception x)
                        {
                            Console.WriteLine("\nError: " + x.Message);
                            Console.WriteLine("\nRuta: " + x.StackTrace);
                        }

                        break;

                    case 2:
                        //Bloque de lectura
                        try
                        {
                            //Capturar nombre del archivo
                            Console.Write("\nAlimenta el nombre del archivo que desees leer: "); Arch = Console.ReadLine();
                            A1.MostrarArchivo(Arch);
                        }
                        catch (IOException x)
                        {
                            Console.WriteLine("\nError: " + x.Message);
                            Console.WriteLine("\nRuta: " + x.StackTrace);
                        }

                        break;

                    case 3:
                        Console.Write("\nPresione ENTER para salir del programa");
                        Console.ReadKey();
                        break;

                    default:
                        Console.Write("\nEsa opción no existe!!, presione ENTER para continuar...");
                        Console.ReadKey();
                        break;
                }

            } while (opcion != 3);

        }
    }
}
