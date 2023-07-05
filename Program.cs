using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace efm15_16
{

    public abstract class IR
    {
        //1,5 POINTS
        static int[,] _Tranches = { { 0, 28000 }, { 28001, 40000 }, { 40001, 50000 }, { 50001, 60000 }, { 60001, 150000 } };
        //1.5 POINTS
        static double[] _TauxIr = { 0, 12, 24, 34, 38, 40 };
        //
        public abstract double SalaireAPayer();
        public static double getIr(double salaire)//3 POINTS
        {

            double x;
            x = salaire * 12;
            if (x >= _Tranches[0, 0] && x <= _Tranches[0, 1])
            {
                return _TauxIr[0];
            }
            else if (x >= _Tranches[1, 0] && x <= _Tranches[1, 1])
            {
                return _TauxIr[1];
            }
            else if (x >= _Tranches[2, 0] && x <= _Tranches[2, 1])
            {
                return _TauxIr[2];
            }
            else if (x >= _Tranches[3, 0] && x <= _Tranches[3, 1])
            {
                return _TauxIr[3];
            }
            else if (x >= _Tranches[4, 0] && x <= _Tranches[4, 1])
            {
                return _TauxIr[4];
            }
            else return _TauxIr[5];
        }


        interface IEmploye
        {


            public int age
            {
                get { return age; }
                set { age = value; }
            }

            public int ancienneté
            {
                get { return ancienneté; }
                set { ancienneté = value; }
            }
            public int datedenaissance
            {
                get { return datedenaissance; }
                set { datedenaissance = value; }
            }
            //1 point
            int Age();
            //1 point
            int Ancienneté();
            // 1 point
            int DateRetraite(int ageRetraite);

        }

        class Employe : IR, IComparable<Employe>, IEmploye
        {
            // 2 points
            public int _mtle = 0; //fields
            public string _nom;
            public int _dateNaissance;
            public int _dateEmbauche;
            public double _salaireBase;

            public int Mtle
            {
                get { return _mtle; }
                set { _mtle = value; }
            }
            public string Nom
            {
                get { return _nom; }
                set { _nom = value; }
            }

            public double SalaireBase
            {
                get { return _salaireBase; }
                set { _salaireBase = value; }
            }

            //3 points
            public int DateEmbauche // Accesseur
            {
                get
                {
                    return _dateEmbauche;
                }
                set
                { _dateEmbauche = value; }
            }
            public int DateNaissance//Accesseur
            {
                get
                {

                    if (DateEmbauche - _dateNaissance >= 16)
                    {
                        return _dateNaissance;
                    }
                    else
                    {
                        throw new Exception("you musst be at least 16 years old");
                    }
                }
                set
                {
                    _dateNaissance = value;
                }
            }



            //3 points
            public Employe()//constructeur par défaut(sans paramètre).
            {
                this._mtle = 0;
                this._nom = "";
                this._dateEmbauche = 0;
                this._dateNaissance = 0;
                this._salaireBase = 0;
            }
            public Employe(int matricule, string nom, int dateEmbauche, int dateNaissance, double salaireBase)//constructeur d'initialisation.
            {
                this._mtle = matricule;
                this._nom = nom;
                this._dateEmbauche = dateEmbauche;
                this._dateNaissance = dateNaissance;
                this._salaireBase = salaireBase;
            }
            // 1 point
            public override double SalaireAPayer()
            {
                return _salaireBase;
            }
            //2 points
            public int CompareTo([AllowNull] Employe other)
            {
                if (other == null) return 1;
                return _nom.CompareTo(other._nom);
            }
            public int age { get; set; }
            public int ancienneté { get; set; }
            public int datedenaissance { get; set; }
            // 5 points
            public int Age()
            {
                age = Convert.ToInt32(Console.ReadLine());
                return age;
            }

            public int Ancienneté()
            {
                ancienneté = Convert.ToInt32(Console.ReadLine());
                return ancienneté;
            }

            public int DateRetraite(int ageRetraite)
            {
                datedenaissance = Convert.ToInt32(Console.ReadLine());
                int datederetraite = ageRetraite + datedenaissance;
                return datederetraite;
            }
            //1 point
            public override string ToString()
            {
                return _mtle + " - " + _nom + " - " + _dateEmbauche + " - " + _dateNaissance + " - " + _salaireBase;
            }
            // 2 points
            public override bool Equals(object obj)
            {
                Employe e = (Employe)obj;
                if (this._mtle == e._mtle)
                    return true;
                else return false;
                //return this._mtle == e._mtle;

            }

        }
        class Formateur : Employe
        {
            //2 points
            private int _heureSup;
            private double _remunerationHSup = 70.00 ;

            //1 point
            public double RemunerationHSup
            {
                get { return _remunerationHSup; }
                set { _remunerationHSup = value; }
            }

            // 3 points
            public Formateur()//constructeur par défaut(sans paramètre).
            {
                this._mtle = 0;
                this._nom = "";
                this._dateEmbauche = 0;
                this._dateNaissance = 0;
                this._salaireBase = 0;
                this._heureSup = 0;
                this._remunerationHSup = 70.00;
            }
            public Formateur(int matricule, string nom, int dateEmbauche, int dateNaissance, double salaireBase, int HeureSup, double RemunerationHSup)//constructeur d'initialisation.
            {
                this._mtle = matricule;
                this._nom = nom;
                this._dateEmbauche = dateEmbauche;
                this._dateNaissance = dateNaissance;
                this._salaireBase = salaireBase;
                this._heureSup = HeureSup;
                this._remunerationHSup = RemunerationHSup;
            }
            // 3 points
            public override double  SalaireAPayer()
            {
                double salairenet;
                int NbrHS = _heureSup * 12;
                double TauxIr =  (getIr(_salaireBase) *1/ 100) ;
                salairenet = (_salaireBase + _heureSup * _remunerationHSup) * (1 - TauxIr );
                return salairenet;
            }
            // 1 point
            public override string ToString()
            {
                return base.ToString() + " - " +  _heureSup + " - " + _remunerationHSup;
            }
            class Agent  : Formateur 
            {
                public double _primeResponsabilite { get; set; }

                public Agent(int matricule, string nom, int dateEmbauche, int dateNaissance, double salaireBase, int HeureSup, double RemunerationHSup,double PrimeResp)
                {
                    this._mtle = matricule;
                    this._nom = nom;
                    this._dateEmbauche = dateEmbauche;
                    this._dateNaissance = dateNaissance;
                    this._salaireBase = salaireBase;
                    this._heureSup  = HeureSup;
                    this._remunerationHSup = RemunerationHSup;
                }
                //2 points
                public override double SalaireAPayer()
                {
                    double TauxIr = (getIr(_salaireBase) * 1 / 100);
                    double salaireNet = (_salaireBase + _primeResponsabilite) * (1- TauxIr);
                    return salaireNet;
                }
            }
            static void Main(string[] args)
            {
                //TEST DE LA FONCTION getIR()
                //int x =Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine(getIr(x));
                //TEST DES ACCESSEURS
                //Employe obj = new Employe();
                //Console.WriteLine("Date D'embauche: ", obj);
                //obj.DateEmbauche = Convert.ToInt32(Console.ReadLine());
                //Console.WriteLine("Date de Naissance :", obj);
                //obj.DateNaissance = Convert.ToInt32(Console.ReadLine());
                //Employe em = new Employe(1, "damian", 2023, 1999, 3000);
                //Console.WriteLine(em.ToString());
                //Employe em2 = new Employe(1, "mouad", 2022, 2001, 3000);
                //bool t = em.Equals(em2);
                //Console.WriteLine(t.ToString());
                //Employe e = new Employe();
                //int f = e.DateRetraite(60);
                //Console.WriteLine(f);
                //List<Employe> emp = new List<Employe>
                //{
                //    new Employe(1,"mouad",2023,2001,2000), new Employe (2,"pedro",2022,2002,3000),
                //    new Employe (3,"andrei",2019,2003,6000)
                //};
                //emp.Sort();
                //emp.ForEach(Employe => Console.WriteLine(Employe.Nom));
                //test de la méthode SalaireAPayer
                //Formateur f = new Formateur(1, "MOUAD", 2023, 2001, 30000, 4, 70.00);
                //Console.WriteLine(f.ToString());
                //Console.WriteLine(f.SalaireAPayer());
                //Agent a = new Agent(1,"Mouad",2023,2001,30000,4,70.00,1000);
                //Console.WriteLine(a.SalaireAPayer());
            }
        }





    }
}
