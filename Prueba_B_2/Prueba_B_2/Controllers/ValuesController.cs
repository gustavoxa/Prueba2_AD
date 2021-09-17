using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Prueba_B_2.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public HttpResponseMessage Get(string usuario, string password)
        {
            var response = new HttpResponseMessage();
            var value = new NameValueCollection();
            int valor;
            valor = usuarios(usuario, password);
            if (valor == 2)
            {
                value["usuario"] = usuario;
                value["password"] = password;
                value["date"] = DateTime.Now.ToString("dd-MM-yyyy");
                value["time"]= DateTime.Now.ToString("hh:mm:ss tt");
                var Coki = new CookieHeaderValue("session", value);
                response.Headers.AddCookies(new CookieHeaderValue[] { Coki });
            }
            else
            {
                response.Headers.Clear();
                response = null;

            }
            return response;
        }

        private int usuarios(string nombre, string password)
        {
            int resultado=0;
            int contador = 1;
            String line;
            string[] words;
            string valor1 = " ";
            string valor2 = " ";
            StreamReader sr = new StreamReader(@"C:\Users\Usuario\Desktop\Prueba_B_2\Usuarios.txt");
            //Read the first line of text
            line = sr.ReadLine();
            char[] delimiterChars = { ' ', ',', '.', '-', '\t' };
            while (line != null)
            {
                //write the line to console window
                 words = line.Split(delimiterChars);
                foreach (var word in words)
                {
                    if (contador == 1)
                    {
                        valor1 = word;
                        contador = contador + 1;

                    }
                    else
                    {
                        valor2 = word;
                        contador = contador - 1;

                    }
                }

                if (valor1 != "" && valor2 != " ")
                {

                    if (nombre == valor1 && valor2 == password)
                    {
                        resultado = 2;
                        break;
                    }

                }
                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            return resultado;
        }

        

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        // POST api/values
        public string Get(int parametro,string nombre)
        {
            string salida = "";
            CookieHeaderValue Coki = Request.Headers.GetCookies("session").FirstOrDefault();
            if (Coki != null)
            {
                CookieState cookie_State = Coki["session"];
                if (parametro == 1)
                {
                    string tipopokemon = tipo(nombre);
                    salida = tipopokemon;
                }
                if (parametro == 2)
                {
                    string tipopoke = tipo(nombre);
                    string debil = debilidad(tipopoke);
                    salida = debil;
                }
            }
            else 
            {
                salida = "No valido";
            }
            return salida;
        }
        private string tipo(string nombre)
        {
            string resultado = "";
            int contador = 1;
            String line;
            string[] words;
            string valor1 = " ";
            string valor2 = " ";
            string valor3;
            StreamReader sr = new StreamReader(@"C:\Users\Usuario\Desktop\Prueba_B_2\Nombres.txt");
            //Read the first line of text
            line = sr.ReadLine();
            char[] delimiterChars = { ' ', ',', '.', '-', '\t' };
            while (line != null)
            {
                //write the line to console window
                words = line.Split(delimiterChars);
                foreach (var word in words)
                {
                    valor3 = word;

                    if (contador == 1)
                    {
                        valor1 = valor3;
                        contador = contador + 1;
                    }
                    else
                    {
                        valor2 = word;

                        contador = contador - 1;

                    }
                }

                if (valor1 != "" && valor2 != " ")
                {


                    if (nombre == valor1)
                    {
                        resultado = valor2;

                        break;
                    }

                }

                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            return resultado;
        }

        static string debilidad(string debil)
        {
            string resultado = "";
            int contador = 1;
            String line;
            string[] words;
            string valor1 = " ";
            string valor2 = " ";
            string valor3;
            StreamReader sr = new StreamReader(@"C:\Users\Usuario\Desktop\Prueba_B_2\Debilidades.txt");
            //Read the first line of text
            line = sr.ReadLine();
            char[] delimiterChars = { ' ', ',', '.', '-', '\t' };
            while (line != null)
            {
                //write the line to console window
                words = line.Split(delimiterChars);
                foreach (var word in words)
                {
                    valor3 = word;
                    //Console.WriteLine(valor3);
                    if (contador == 1)
                    {
                        valor1 = valor3;
                        contador = contador + 1;
                    }
                    else
                    {
                        valor2 = word;
                        contador = contador - 1;

                    }
                }

                if (valor1 != "" && valor2 != " ")
                {


                    if (debil == valor1)
                    {
                        resultado = valor2;

                        break;
                    }

                }

                //Read the next line
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            return resultado;

        }


    }
}
