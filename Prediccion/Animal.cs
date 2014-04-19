using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prediccion
{
    public interface Animal
    {
        int Id { get; set; }
        string Nombre { get; set; }
        TipoAnimal Tipo { get; }
        void Prediccion();
    }

    public enum TipoAnimal
    {
        Dragon = 1,
        Mantis = 2,
        Papagayo = 3,
        Canguro = 4,
        Pulpo = 5
    }

    public class Dragon : Animal
    {


        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {

            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get
            {
                return TipoAnimal.Dragon;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void Prediccion()
        {
            throw new NotImplementedException();
        }

        private Dragon()
        {

        }

        public static Dragon Create()
        {
            return new Dragon();
        }
    }

    public class Mantis : Animal
    {


        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {

            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get
            {
                return TipoAnimal.Mantis;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void Prediccion()
        {
            throw new NotImplementedException();
        }

        private Mantis()
        {

        }

        public static Mantis Create()
        {
            return new Mantis();
        }
    }

    public class Papagayo : Animal
    {


        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {

            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get
            {
                return TipoAnimal.Papagayo;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void Prediccion()
        {
            throw new NotImplementedException();
        }

        private Papagayo()
        {

        }

        public static Papagayo Create()
        {
            return new Papagayo();
        }
    }

    public class Canguro : Animal
    {

        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {

            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get
            {
                return TipoAnimal.Canguro;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void Prediccion()
        {
            throw new NotImplementedException();
        }

        private Canguro()
        {

        }

        public static Canguro Create()
        {
            return new Canguro();
        }
    }

    public class Pulpo : Animal
    {

        public int Id
        {
            get;
            set;
        }

        public string Nombre
        {

            get;
            set;
        }

        public TipoAnimal Tipo
        {
            get
            {
                return TipoAnimal.Pulpo;
            }
            private set
            {
                throw new NotImplementedException();
            }
        }

        public void Prediccion()
        {
            throw new NotImplementedException();
        }

        private Pulpo()
        {

        }

        public static Pulpo Create()
        {
            return new Pulpo();
        }
    }

}
