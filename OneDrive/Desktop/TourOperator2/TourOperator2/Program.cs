using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TourOperator2
{
    
    public interface ICD
    {
        void insert(IComparable key, Object attribute);
        Object find(IComparable key);
        Object remove(IComparable key);
    }

    public interface IContainer
    {
        bool IsEmpty();
        void MakeEmpty();
        int Size();
    }

    public class TourOperator : ICD, IContainer
    {
        private String nextClientCode;
        private Dictionary<IComparable, Object> clientDictionary;

        public TourOperator(String initialClientCode)
        {
            this.nextClientCode = initialClientCode;
        }
        public void add(String nome, String dest) 
        {
            string codCliente = this.nextClientCode;
            this.nextClientCode = this.IncrementClientCode(codCliente);
            this.clientDictionary[codCliente] = new Client(nome, dest);
        }
        void ICD.insert(IComparable key, Object attribute)
        {

        }

        Object ICD.find(IComparable key)
        {
            if (this.clientDictionary.ContainsKey(key))
            {
                return this.clientDictionary[key];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        Object ICD.remove(IComparable key)
        {
            if (this.clientDictionary.ContainsKey(key))
            {
                object attribute = this.clientDictionary[key];
                this.clientDictionary.Remove(key);
                return attribute;
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }

        bool IContainer.IsEmpty()
        {
            return this.clientDictionary.Count == 0;
        }

        void IContainer.MakeEmpty()
        {
            this.clientDictionary.Clear();
        }

        int IContainer.Size()
        {
            return this.clientDictionary.Count;
        }

        //public override string ToString()
        //{
        //    return base.ToString();
        //}


        public static void main(String[] args) 
        {

        }

        private class Client 
        {
            String name;
            String dest;

            Client(String _name, String _dest)
            {
                name = _name;
                dest = _dest;
            }
        }

        private class Coppia : IComparable 
        {
            String code;
            Client client;

            public Coppia(string _code, Client _client)
            {
                code = _code;
                client = _client;
            }

             int IComparable.CompareTo(Object obj)
            {
                Coppia tmpC = (Coppia)obj;
                return code.CompareTo(tmpC.code);
            }
        }

        private string IncrementClientCode(string codiceCliente)
        {
            Regex regex = new Regex(@"^[A-Z][0-9]{3}$");
            Match match = regex.Match(codiceCliente);

            if (match.Success)
            {
                char lettera = match.Groups[1].Value[0];
                string numeroStringa = match.Groups[2].Value;
                int numero = int.Parse(numeroStringa);

                if (numero == 999)
                {
                    if (lettera == 'Z')
                    {
                        lettera = 'A';
                        numero = 0;
                    }
                    else
                    {
                        lettera++;
                        numero = 0;
                    }
                }
                else
                {
                    numero++;
                }

                return $"{lettera}{numero:000}";
            }
            else
            {
                throw new ArgumentException("Codice cliente non valido!");
            }
        }
    }
}


    